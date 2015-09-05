using myBlog.Models;
using myBlog.Models.DataModels;
using myBlog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace myBlog.Controllers
{
    public class HomeController : Controller
    {
        #region Index G
        [HttpGet]
        public ActionResult Index()
        {
            DB db = new DB();
            List<Post> posts = new List<Post>();
            List<vPost> vposts = new List<vPost>();
            posts =
                (from p in db.Posts
                 orderby p.ID descending
                 select p).ToList();
            foreach (Post post in posts)
            {
                vPost vpost = new vPost(post);
                vposts.Add(vpost);
            }
            ViewBag.posts = vposts;
            return View();
        }
        #endregion

        #region Index P
        [HttpPost]
        public ActionResult Index(int page)
        {
            DB db = new DB();
            int index = page * 10;
            List<Post> posts = new List<Post>();
            List<vPost> vposts = new List<vPost>();
            posts =
                (from p in db.Posts
                 orderby p.ID descending
                 select p).Skip(index).Take(10).ToList();
            foreach (Post item in posts)
            {
                vposts.Add(new vPost(item));
            }
            AjaxModel ajaxModel = new AjaxModel();
            try
            {
                if (vposts.Count() == 0)
                {
                    ajaxModel.Data = "";
                    ajaxModel.Statu = "end";
                    ajaxModel.Msg = "没有更多了";
                }
                else
                {
                    ajaxModel.Data = vposts;
                    ajaxModel.Statu = "OK";
                    ajaxModel.Msg = "加载成功";
                }
            }
            catch
            {
                ajaxModel.Statu = "err";
                ajaxModel.Msg = "获取失败";

            }
            return Json(ajaxModel);
        }
        #endregion

        public ActionResult About()   //关于我
        {
            return View();
        }

        public ActionResult Slive() //慢生活
        {
            DB db = new DB();
            List<Post> posts = new List<Post>();
            List<vPost> vPosts = new List<vPost>();
            posts =
                (from p in db.Posts
                 orderby p.ID descending
                 select p).ToList();

            foreach (Post post in posts)
            {
                vPost vpost = new vPost(post);
                vPosts.Add(vpost);
            }
            ViewBag.posts = vPosts;

            List<Post> postList = new List<Post>();
            postList =
                (from l in db.Posts
                 orderby l.Browse descending
                 select l).ToList();
            ViewBag.postList = postList;
                
            return View();
        }
        [HttpGet]
        public ActionResult PostMore(int id)//日志详细内容
        {
            DB db = new DB();
            List<Post> posts = new List<Post>();
            List<vPost> vposts = new List<vPost>();
            posts =
                (from p in db.Posts
                 select p).ToList();
            foreach (Post post in posts)
            {
                vPost vpost = new vPost(post);
                vposts.Add(vpost);
            }
            ViewBag.posts = vposts;

            List<Post> postList = new List<Post>();
            postList =
                (from l in db.Posts
                 orderby l.Browse descending
                 select l).ToList();
            ViewBag.postList = postList;

            Post post2 = new Post();
            post2 =
                 (from p in db.Posts
                  where p.ID == id
                  select p).FirstOrDefault();
            post2.Browse++;
            db.SaveChanges();
            ViewData["id"] = post2.ID;
            ViewData["title"] = post2.Title;
            ViewData["content"] = post2.Content;

            List<PostReply> postreply = new List<PostReply>();
            postreply =
                (from p in db.PostReply
                 where p.PostID == id
                 select p).ToList();
            ViewBag.postreply = postreply;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]  //允许特殊字符提交/
        public ActionResult PostMore_reply(int id, string content) //日志回复
        {
            DB db = new DB();
            PostReply postreply = new PostReply();
            postreply.PostID = id;
            postreply.ReplyContent = content;
            postreply.DateOfIssue = DateTime.Now;

            db.PostReply.Add(postreply);
            int row = db.SaveChanges();
            if (row > 0)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return Redirect("/Home/Error");
            }
        }
        public ActionResult Share()  //资源分享
        {
            return View();
        }

        public ActionResult Message()  //留言
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}