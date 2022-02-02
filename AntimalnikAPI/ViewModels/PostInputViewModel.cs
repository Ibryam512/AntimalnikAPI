using AntimalnikAPI.Enums;
using AntimalnikAPI.Models;
using System.Collections.Generic;

namespace AntimalnikAPI.ViewModels
{
    public class PostInputViewModel
    {
        public PostType PostType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string UserName { get; set; }
    }
}