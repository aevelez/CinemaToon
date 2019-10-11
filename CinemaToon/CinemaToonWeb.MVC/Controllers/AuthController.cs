using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CinemaToon.Web.MVC.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {      

        public async Task<IActionResult> Logout()
        {         

            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return await Task.FromResult(View());
        }
    }
}
