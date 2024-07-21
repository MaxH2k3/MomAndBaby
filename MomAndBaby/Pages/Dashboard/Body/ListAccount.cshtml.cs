using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Enums;
using MomAndBaby.Service;
using MomAndBaby.Service.Extension;
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
        public async Task OnGet(int? pageIndex)
        {
            int pageSize = 4;
            CurrentPage = pageIndex ?? 1;

            var list  = await _userService.GetAllUsers();
            int count = list.Count();
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Users = list.Skip((CurrentPage - 1) * pageSize).Take(pageSize).ToList();
            ViewData[VariableConstant.CurrentMenu] = (int)Menu.AccountList;
           
        }

        public async Task<IActionResult> OnPostUpdateStatus(string userEmail, int i)
        {
            var user = await _userService.GetUserByEmail(userEmail);
            if (user != null)
            {
                await _userService.UpdateStatus(user);
                _notificationWorker.DoWork(Guid.Parse(User.GetUserIdFromToken()), TableName.Account, NotificationType.Modified);
            }
            await OnGet(i);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var users = await _userService.GetAllUsers();
            var csv = new StringBuilder();
            csv.AppendLine("Id,UserName,FullName,Email, PhoneNumber,Address,RoleId,CreatedAt,UpdatedAt,Status");
            foreach (var user in users)
            {
                csv.AppendLine($"{user.Id},{user.Username},{user.FullName},{user.Email},{user.PhoneNumber},{user.Address},{user.RoleId},{user.CreatedAt},{user.UpdatedAt},{user.Status}");
            }

            // Return the CSV file
            var byteArray = Encoding.ASCII.GetBytes(csv.ToString());
            var stream = new MemoryStream(byteArray);

            return File(stream, "text/csv", "UserList.csv");
        }

        

    }
}
