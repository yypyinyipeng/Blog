using myBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myBlog.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (this.HttpContext.Session["User"] == null)
                return Redirect("/Admin/Login");
            else
                return View();
        }

        [HttpGet]
        public ActionResult Register()//注册
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(string username , string password , string rpassword)//注册
        {
            DB db = new DB();
            User user = new User();
            user.Name = username;
            user.Password = password;
            db.Users.Add(user);
            int row = db.SaveChanges();
            if (row > 0)
            {
                return Redirect("/Admin/Login");
            }
            else
            {
                return Redirect("/Admin/Error");
            }
        }

        [HttpGet]
        public ActionResult Login()//登陆
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username , string password)//登陆
        {
            DB db = new DB();
            User user = new User();
            //user =
            //    (from U in db.Users
            //     where username == U.Name && password == U.Password
            //     select U).FirstOrDefault();
            user = (db.Users.Where(u => u.Password == password && u.Name == username)).FirstOrDefault();
            if (user != null)
            {
                this.HttpContext.Session["User"] = username;
                return Redirect("/Admin/Index");
            }
            else
            {
                return Redirect("/Admin/Error");
            }
        }

        [HttpGet]
        public ActionResult PublicPosts()//发布日志
        {
            if (this.HttpContext.Session["User"] == null)
                return Redirect("/Admin/Login");
            else
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult PublicPosts(string title, string content, string classify)  //发布日志
        {
                DB db = new DB();
                Post posts = new Post();
                posts.Title = title;
                posts.Content = content;
                posts.Classify = classify;
                posts.DateOfIssue = DateTime.Now;
                posts.Author = "尹逸朋";
                db.Posts.Add(posts);
                int row = db.SaveChanges();
                if (row > 0)
                {
                    return Redirect("/Admin/PublicPosts");
                }
                else
                {
                    return Redirect("/Admin/Error");
                }
        }

        [HttpGet]
        public ActionResult ManagePost()   //管理日志
        {
            if (this.HttpContext.Session["User"] == null)
                return Redirect("/Admin/Login");
            else
            {
                DB db = new DB();
                List<Post> posts = new List<Post>();
                posts =
                    (from p in db.Posts
                     select p).ToList();
                int counts = posts.Count();
                ViewData["counts"] = counts;
                ViewBag.posts = posts;
                return View();
            }
        }
        [HttpGet]
        public ActionResult RevPost(int id)//修改日志
        {
                DB db = new DB();
                Post posts = new Post();
                posts =
                    (from p in db.Posts
                     where p.ID == id
                     select p).FirstOrDefault();
                return Redirect("/Admin/ManagePost");
        }
        [HttpGet]
        public ActionResult DelPost(int id)//删除日志
        {
            DB db = new DB();
            Post posts = new Post();
            posts =
                (from p in db.Posts
                where p.ID == id
                select p).FirstOrDefault();
            db.Posts.Remove(posts);
            db.SaveChanges();
            return Redirect("/Admin/ManagePost");
        }
        [HttpGet]
        public ActionResult Message()
        {
            DB db = new DB();
            List<Message> messages = new List<Message>();
            messages =
                (from m in db.messages
                select m).ToList();
            ViewBag.messages = messages;
            return View();
        }
        public ActionResult OutLogin()//退出登陆
        {
            this.HttpContext.Session["User"] = null;
            return Redirect("/Admin/Login");
        }
        public ActionResult Error()//  错误页面404
        {
            if (this.HttpContext.Session["User"] == null)
                return Redirect("/Admin/Login");
            else
                return View();
        }

	}
}