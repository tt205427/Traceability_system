using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Traceability_system.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public required InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "用户名")]
            public required string UserName { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "密码")]
            public required string Password { get; set; }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // 这里进行 AD 域验证
                // 验证通过后可以进行登录操作
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}