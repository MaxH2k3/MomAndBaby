using FluentEmail.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Constants;
using MomAndBaby.BusinessObject.Entity;
using MomAndBaby.BusinessObject.Models.UserDto;
using MomAndBaby.Service;
using MomAndBaby.Utilities.Helper;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using PayPal.Api;
using MomAndBaby.Service.OrderService;
using MomAndBaby.Service.Extension;

namespace MomAndBaby.Pages.Main.Body
{
    public class MyAccountModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public MyAccountModel(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string? UserName { get; set; }

        [BindProperty]
        public string? FullName { get; set; }
        [BindProperty]
        public string? PhoneNumber { get; set; }
        [BindProperty]
        public string? Address { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        [PasswordMatch("NewPassword", ErrorMessage = "The passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        public IEnumerable<MomAndBaby.BusinessObject.Models.OrderResponseModel> Orders { get; set; } = new List<MomAndBaby.BusinessObject.Models.OrderResponseModel>();

        public async Task OnGet()
        {
            if(!User.Identity!.IsAuthenticated)
            {
                Redirect("/login");
            }
            
            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(UserClaimType.Email));

            if (email != null)
            {
                var user = await _userService.GetUserByEmail(email.Value);
                if (user != null)
                {
                    Email = user.Email;
                    UserName = user.Username;
                    FullName = user.FullName;
                    Address = user.Address;
                    PhoneNumber = user.PhoneNumber;
                    Orders = await _orderService.GetAllOrder(Guid.Parse(User.GetUserIdFromToken()));
                }
            }
        }

        public async Task<IActionResult> OnPostSaveAccount()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var email = HttpContext.User.Claims.FirstOrDefault(c => c.Type == UserClaimType.Email).Value;
            var updateUser = new UpdateUserDto(UserName, FullName, NewPassword, PhoneNumber, Address);
            var userUpdatedEntity = await _userService.UpdateUser(email, updateUser);
            UserName = userUpdatedEntity.Username;
            FullName = userUpdatedEntity.FullName;
            Address = userUpdatedEntity.Address;
            PhoneNumber = userUpdatedEntity.PhoneNumber;
            TempData["MyAccount"] = "Update Account Detail Successfully";

            return Page();
        }
        public async Task<IActionResult> OnPostUpdateShippingAddressAsync(int orderId, string shippingAddress)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Process the update logic here
            var result = await _orderService.UpdateOrderAddress(shippingAddress, orderId);

            if (result)
            {
                TempData["SuccessMessage"] = "Shipping address updated successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Error updating shipping address.";
            }

            return RedirectToPage(); // Reload the page
        }



    }
}
