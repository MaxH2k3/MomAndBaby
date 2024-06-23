using Microsoft.AspNetCore.Mvc.RazorPages;
using MomAndBaby.BusinessObject.Entity;

namespace MomAndBaby.Pages.Authen
{
    public class TestModel : PageModel
    {
        private readonly MomAndBabyContext _context;

        public TestModel()
        {
            _context = new MomAndBabyContext();
        }

        public void OnGet()
        {
            Console.WriteLine("TestModel OnGet");
            var users = _context.Users.ToList();
            foreach (var item in users)
            {
                Console.WriteLine("item: " + item);
            }
        }
    }
}
