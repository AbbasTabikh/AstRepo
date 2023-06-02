using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Data.Models
{
    public class ContactRequest
    {
        public Guid Id { get; set; }
        public string UserFullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? IPAddress { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
