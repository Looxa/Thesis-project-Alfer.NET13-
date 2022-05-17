using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharer.ClassLibrary.Entities
{
    internal class AccountInfo
    {
        public string Id { get; set; }
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; } = null!;
    }
}
