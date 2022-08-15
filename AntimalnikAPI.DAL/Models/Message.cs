using System;

namespace AntimalnikAPI.DAL.Models
{
    public class Message : BaseEntity
    {
        public string SenderId { get; set; }
        public virtual ApplicationUser Sender { get; set; }
        public string RecieverId { get; set; }
        public virtual ApplicationUser Reciever { get; set; }
        public string Text { get; set; }
    }
}
