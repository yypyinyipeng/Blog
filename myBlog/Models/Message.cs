using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myBlog.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime DateOfIssue { get; set; }
    }
}