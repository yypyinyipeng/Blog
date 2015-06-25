using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myBlog.Models
{
    public class PostReply
    {
        public int ID { get; set; }
        [ForeignKey("Post")]
        public int PostID{ get; set; }
        public virtual Post Post { get; set; }
        public string ReplyContent { get; set; }
        public string ReplyAuthor { get; set; }
        public DateTime DateOfIssue { get; set; }
    }
}