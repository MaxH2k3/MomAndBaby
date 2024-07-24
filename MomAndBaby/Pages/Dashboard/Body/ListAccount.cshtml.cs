using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
using MomAndBaby.Service.MessageConstant;
using MomAndBaby.Service.Worker;
using MomAndBaby.Utilities.Constants;
using System.Text;

namespace MomAndBaby.Pages.Dashboard.Body
{
    public class ListAccountModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly NotificationWorker _notificationWorker;

        public ListAccountModel(IUserService userService, NotificationWorker notificationWorker)
        {
            _userService = userService;
            _notificationWorker = notificationWorker;
        }


       
        public IEnumerable<User?> Users { get; set; }
       
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int? SelectedRole { get; set; }

        [BindProperty]
        public string? EmailStaff { get; set; }

        public async Task OnGet(int? pageIndex, int? role)
        {
            int pageSize = 15;
            CurrentPage = pageIndex ?? 1;
            SelectedRole = role;

            IEnumerable<User?> list;
            if (role == null)
            {
                list = await _userService.GetUsersExceptAdmin();
            }
            else
            {
                list = await _userService.GetUserByRoleId(role);
            }

            int count = list.Count();
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Users = list.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToList();
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.AccountList;
           
        }

        public async Task<IActionResult> OnPostUpdateStatus(string userEmail, int i, int? role)
        {
            var user = await _userService.GetUserByEmail(userEmail);
            if (user != null && user.RoleId != (int)RoleType.Admin)
            {
                await _userService.UpdateStatus(user);
                _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Account, NotificationType.Modified);
            }
            return RedirectToPage(new { pageIndex = i, role = role });
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var users = await _userService.GetUsersExceptAdmin();
            var csv = new StringBuilder();
            csv.AppendLine("Id,UserName,FullName,Email, PhoneNumber,Address,Role,CreatedAt,UpdatedAt,Status");
            foreach (var user in users)
            {
                csv.AppendLine($"{user.Id},{user.Username},{user.FullName},{user.Email},{user.PhoneNumber},{user.Address},{user.Role.Name},{user.CreatedAt},{user.UpdatedAt},{user.Status}");
            }

            // Return the CSV file
            var byteArray = Encoding.ASCII.GetBytes(csv.ToString());
            var stream = new MemoryStream(byteArray);

            return File(stream, "text/csv", "UserList.csv");
        }

        public async Task<IActionResult> OnPostApply(int? role)
        {
            SelectedRole = role;
            return RedirectToPage(new { pageIndex = 1, role = SelectedRole });
        }

        public async Task<IActionResult> OnPostAddStaff()
        {
            var result = await _userService.AddStaff(EmailStaff);
            Users = await _userService.GetUsersExceptAdmin();
            if (result == null)
            {
                TempData["MessageRegister"] = MessageAuthen.ExistedEmail;
                
                return Page();
            }
            else
            {
                TempData["MessageRegister"] = MessageAuthen.AddStaffTrue;
                
                return Page();
            }
        }



        

        

    }
}
