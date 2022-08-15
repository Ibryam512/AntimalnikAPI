using AntimalnikAPI.DAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AntimalnikAPI.DAL.Models
{
    public class Post : BaseEntity
    {
        public PostType PostType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime DeleteDate { get; set; }
        public string Image { get; set; }
        public string CreatorId { get; set; }
        public virtual ApplicationUser Creator { get; set; }
    }
}
