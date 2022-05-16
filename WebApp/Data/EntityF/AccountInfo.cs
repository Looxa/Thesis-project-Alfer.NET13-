﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSharer.Web.Data.EntityF
{
    public class AccountInfo
    {
        public string accountId { get; set; }
        public string login { get; set; } = null!;
        public string passwordHash { get; set; } = null!;
        public DateTime RegistrationDate { get; set; }
        public int userId { get; set; }
    }
}