using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using sharedclipboard.Models;
using sharedclipboard.services;

namespace sharedclipboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly SharedBoardService _sharedBoardSrv;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string COOKIE_NAME_USER = "MyCookieAuthenticationScheme";

        private readonly IList<string> _urlList;

        public HomeController(IHttpContextAccessor httpContextAccessor, IHostingEnvironment hostingEnv, IMemoryCache memoryCache)
        {
            _sharedBoardSrv = new SharedBoardService(memoryCache, hostingEnv);

            _httpContextAccessor = httpContextAccessor;

            //var context = _httpContextAccessor.HttpContext;
            
            
            /*
            if(_httpContextAccessor.HttpContext.Items.ContainsKey(COOKIE_NAME_USER)){
                _urlList = (List<string>)_httpContextAccessor.HttpContext.Items[COOKIE_NAME_USER];
            }else{
                _urlList = new List<string>();
            }
            //Response.Cookies.Append(COOKIE_NAME_USER, "");


            string cookieValue;
            if(HttpContext.Request.Cookies.TryGetValue(COOKIE_NAME_USER, out cookieValue)){
                var some = cookieValue;
            }
            */

        }


        public async Task<IActionResult> Index()
        {
            //var tenantId = (string)_httpContextAccessor.HttpContext.Items["tenant"];
            
            //UpdateCookieValues(_urlList);



            /*
            var cookieValue = Response.Cookies[COOKIE_NAME_USER];

            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append(COOKIE_NAME_USER, "", cookie);
            */

            //read cookie from IHttpContextAccessor  
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[COOKIE_NAME_USER];
            //read cookie from Request object  
            string cookieValueFromReq = Request.Cookies[COOKIE_NAME_USER];
            //set the key value in Cookie  
            //Set("kay", "Hello from cookie", 10);  

            foreach (var item in Request.Cookies)
            {
                var asdasd = item.Value;
            }


            string cookieValue = Request.Cookies[COOKIE_NAME_USER];
            if(cookieValue == null ){
                CookieOptions option = new CookieOptions();  
                option.Expires = DateTime.Now.AddMinutes(5);
                option.HttpOnly = true;
                Response.Cookies.Append(COOKIE_NAME_USER, "", option);
            }

            ViewBag.Url =  cookieValue;

            /*
            //string cookieValueFromReq = Request.Cookies[COOKIE_NAME_USER]; 

            if(!HttpContext.Request.Cookies.ContainsKey(COOKIE_NAME_USER)){
                var claims = new List<Claim>
                {
                    new Claim("name", "admin")
                };
                var userIdentity = new ClaimsIdentity(claims, "login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(COOKIE_NAME_USER,principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                });
            }
            */

            

            return View();
        }

        [HttpGet]
        public IActionResult SharedBoard(string id)
        {
            var boardResult = _sharedBoardSrv.LoadBoard(id);
            if(boardResult == null){
                Response.Redirect("Error");
            }
            return View(boardResult);
        }

        [HttpPost]
        public IActionResult Create()
        {
            var url = _sharedBoardSrv.CreateBoard();

            /*
            CookieOptions cookie = new CookieOptions();
            cookie.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append(COOKIE_NAME_USER, url, cookie);
            */

            //HttpContext.Request.Cookies.Item.Add(COOKIE_NAME_USER,url);
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[COOKIE_NAME_USER];
            //read cookie from Request object  
            string cookieValueFromReq = Request.Cookies[COOKIE_NAME_USER];

            CookieOptions option = new CookieOptions();  
            option.Expires = DateTime.Now.AddMinutes(5);  
            option.HttpOnly = true;
            Response.Cookies.Append(COOKIE_NAME_USER, url, option);

            //TODO: change this
            //_urlList.Add(url);
            //UpdateCookieValues(_urlList);


            return Json(url);
        }

        [HttpPost]
        public IActionResult Add(FileInputModel uploadBoard)
        {
            string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies[COOKIE_NAME_USER];
            //read cookie from Request object  
            string cookieValueFromReq = Request.Cookies[COOKIE_NAME_USER];


            var uploadResult = _sharedBoardSrv.AddItemsToBoard(uploadBoard);

            return Json(uploadResult);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        private void UpdateCookieValues(IList<string> list)
        {
            if(_httpContextAccessor.HttpContext.Items.ContainsKey(COOKIE_NAME_USER)){
                _httpContextAccessor.HttpContext.Items[COOKIE_NAME_USER] = list;
            }else{
                _httpContextAccessor.HttpContext.Items.Add(COOKIE_NAME_USER, list);
            }
        }


        
    }
}
