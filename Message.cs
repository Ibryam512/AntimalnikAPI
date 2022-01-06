using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.Models
{
    public class Message
    {
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Reciever { get; set; }
        public string Text { get; set; }
    }
}
