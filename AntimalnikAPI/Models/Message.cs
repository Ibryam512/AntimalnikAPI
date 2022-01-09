using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AntimalnikAPI.Models
{
    public class Message
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Reciever { get; set; }
        public string Text { get; set; }
    }
}
