using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.ViewModels
{
    public class MessageInputViewModel
    {
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public string Text { get; set; }
    }
}
