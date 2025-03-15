using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.Protocols;
using System.Net;

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

            [Display(Name = "记住我?")]
            public bool RememberMe { get; set; }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // 进行 LDAP 验证
                if (AuthenticateUser(Input.UserName, Input.Password))
                {
                    // 验证通过后可以进行登录操作
                    return RedirectToPage("/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "用户名或密码错误。");
                }
            }

            return Page();
        }

        private bool AuthenticateUser(string username, string password)
        {
            try
            {
                // 替换为你的 LDAP 服务器地址
                string ldapServer = "ldap://192.168.3.2:389";
                // 替换为你的域名
                string domain = "PUW.com";
                string userDn = $"{username}@{domain}";

                using (LdapConnection connection = new LdapConnection(new LdapDirectoryIdentifier(ldapServer)))
                {
                    NetworkCredential credential = new NetworkCredential(userDn, password);
                    connection.Credential = credential;
                    connection.AuthType = AuthType.Basic;
                    connection.Bind();
                    return true;
                }
            }
            catch (LdapException)
            {
                return false;
            }
        }
    }
}