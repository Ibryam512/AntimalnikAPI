using AntimalnikAPI.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.Models
{
    public class Post
    {
        public string Id { get; set; }
        public PostType PostType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime DeleteDate { get; set; }
        public string Image { get; set; }
        public DateTime AddDate { get; set; }
        public ApplicationUser User { get; set; }
}
}
