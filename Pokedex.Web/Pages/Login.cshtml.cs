using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pokedex.Client.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
