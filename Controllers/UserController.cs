using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BlogApp.Data.Abstract;
using BlogApp.Data.Concrete.EfCore;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Controllers{

    public class UsersController : Controller{

        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository){
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login(){
            if(User.Identity!.IsAuthenticated){
                return RedirectToAction("Index","Posts");
            }
            return View();
        }
        public async Task<IActionResult>Logout(){
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Register(){
             return View();
         }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model){
            if(ModelState.IsValid){
                var user = await _userRepository.Users.FirstOrDefaultAsync(x=>x.UserName == model.Username || x.Email == model.Email);
                if(user == null){

                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/users");
                    Directory.CreateDirectory(uploadsFolder);
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create)){
                        await model.ImageFile.CopyToAsync(stream);
                    }

                    _userRepository.CreateUser(new Entity.User {
                        UserName = model.Username,
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        Image = fileName,
                    });
                    
                    return RedirectToAction("Login");
                }else{
                    ModelState.AddModelError("","Username ya da Email kullanımda.");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model){
            if(ModelState.IsValid){
                var isUser = await _userRepository.Users.FirstOrDefaultAsync(x=>x.Email == model.Email && x.Password == model.Password);
                if(isUser != null){
                    var userClaims = new List<Claim>();

                    userClaims.Add(new Claim(ClaimTypes.NameIdentifier, isUser.UserId.ToString()));
                    userClaims.Add(new Claim(ClaimTypes.Name, isUser.UserName ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.GivenName, isUser.Name ?? ""));
                    userClaims.Add(new Claim(ClaimTypes.UserData, isUser.Image ?? ""));

                    if(isUser.Email == "info@ahmetkaya.com"){
                        userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
                    }

                    var claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authPoperties = new AuthenticationProperties{IsPersistent = true};

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),authPoperties
                    );

                    return RedirectToAction("Index","Posts");
                }else{
                    ModelState.AddModelError("","Kullanıcı adı veya şifre hatalı");
                }
            }
            return View(model);
        }

        public IActionResult Profile(string username){
            if(string.IsNullOrEmpty(username)){
                return NotFound();
            }
            var user = _userRepository.Users.Include(x=>x.Posts).Include(x=>x.Comments).ThenInclude(x=>x.Post).FirstOrDefault(x=>x.UserName == username);

            if(user == null){
                return NotFound();
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            var user = _userRepository.Users.FirstOrDefault(x => x.UserName == username);

            if (user == null)
            {
                return NotFound();
            }

            var model = new EditProfileViewModel
            {
                UserId = user.UserId,
                Name = user.Name,
                UserName = user.UserName,
                Email = user.Email,
                Image = user.Image
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {   
                var editUser = _userRepository.Users.FirstOrDefault(x => x.UserId == model.UserId);

                if(model.Password != editUser.Password)
                {
                    ModelState.AddModelError("", "Incorrect Password!!");
                    return View(model);
                }

                if (editUser != null)
                {
                    editUser.Name = model.Name;
                    editUser.UserName = model.UserName;
                    editUser.Email = model.Email;

                    if (model.ImageFile != null)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/users");
                        Directory.CreateDirectory(uploadsFolder);
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.ImageFile.CopyToAsync(stream);
                        }

                        editUser.Image = fileName;
                    }

                    if(model.Password != null && model.NewPassword != null && model.Password != model.NewPassword)
                    {
                        editUser.Password = model.NewPassword;
                    }
                    else if(model.Password == model.NewPassword){
                        ModelState.AddModelError("", "New password cannot be same as old password.");
                        return View(model);
                    }

                    _userRepository.EditUser(editUser);

                    var claimsIdentity = (ClaimsIdentity)User.Identity;
                    var usernameClaim = claimsIdentity.FindFirst(ClaimTypes.Name);
                    if (usernameClaim != null)
                    {
                        claimsIdentity.RemoveClaim(usernameClaim);
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, editUser.UserName));
                    }

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties { IsPersistent = true };
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Profile", new { username = editUser.UserName });
                }

                ModelState.AddModelError("", "User couldn't found.");
            }

            return View(model);
        }
    }       
}