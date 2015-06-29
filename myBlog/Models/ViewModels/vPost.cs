using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myBlog.Models.ViewModels
{
    public class vPost
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Classify { get; set; }
        public int Browse { get; set; } //浏览量
        public DateTime DateOfIssue { get; set; }
        public int ReplyCount { get; set; }//评论数 
        public vPost() { 
              
        }
        public vPost(Post post) {
            DB db = new DB ();
            this.ID = post.ID;
            this.Title = post.Title;
            this.Content = post.Content;
            this.Author= post.Author;
            this.Classify = post.Classify;
            this.Browse = post.Browse;
            this.DateOfIssue = post.DateOfIssue;
            this.ReplyCount = db.PostReply.Where(p => p.ID == post.ID).ToList().Count;
        }
    }
}