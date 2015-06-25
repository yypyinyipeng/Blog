using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace myBlog.Models
{
    public class DB : DbContext
    {
        public DB()
            : base("myBlog")
        { 
        
        }
        public DbSet<User> Users { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostReply> PostReply { set; get; }
        public DbSet<Message> messages { set; get; }
    }
}