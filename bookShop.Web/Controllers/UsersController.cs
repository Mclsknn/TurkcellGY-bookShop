using bookShop.Business.Abstract;
using bookShop.Dtos.Requests;
using bookShop.Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;

namespace bookShop.Web.Controllers
{
    [AllowAnonymous]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AddUserRequest user)
        {
            if (ModelState.IsValid)
            {
                
                    bool success = await _userService.AddAsync(user);
                

                return RedirectToAction(nameof(Login));
            }

            return View();
        }

        [HttpGet]
        public IActionResult PanelRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PanelRegister(AddUserRequest user) 
        {
            if (ModelState.IsValid)
            {

                bool success = await _userService.AddAsync(user);


                return RedirectToAction(nameof(PanelLogin));
            }

            return View();
        }
        public IActionResult PanelLogin()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user2 = await _userService.ValidateUser(user.UserName, user.Password);
                if (user2 != null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user2.UserName),
                        new Claim(ClaimTypes.Email, user2.UserMail),
                        new Claim(ClaimTypes.Role, user2.Role),
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimsPrincipal);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                }
                ModelState.AddModelError("login", "kullanıcı adı veya şifre hatalı");

            }
            return View();
        }

        [HttpPost]
        public IActionResult Contact()  //Mail sınıfından m diye bir değişken tanımlarız
        {

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Burası aynı kalacak
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("mustafa.clsknn@gmail.com", "Mmfbgs1175..");
          
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            MailMessage msj1 = new MailMessage();
            msj1.From = new MailAddress("mustafa.clsknn@gmail.com", "Mustafa");
            msj1.To.Add("mustafa.clsknn@outlook.com.tr"); //Buraua iletişim sayfasında gelecek mail adresi gelecktir.
            msj1.Subject = "Mail'inize Cevap";
            msj1.Body = "Size En kısa zamanda Döneceğiz. Teşekkür Ederiz Bizi tercih ettiğiniz için";
            client.Send(msj1);
            return View();

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
