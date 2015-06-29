using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myBlog.Models
{
    public class Post
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string Classify { get; set; }
        public int Browse { get; set; } //浏览量
        public DateTime DateOfIssue { get; set; }

    }
}