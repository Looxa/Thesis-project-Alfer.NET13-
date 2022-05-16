using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharer.ClassLibrary.Entities
{
    internal class AccountInfo
    {
        public string AccountId { get; set; }
        public string Login { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }

        public int userId { get; set; }
    }
}
