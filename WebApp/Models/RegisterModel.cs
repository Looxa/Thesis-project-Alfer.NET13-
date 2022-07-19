using FileSharer.ClassLibrary.Entities;
using System.ComponentModel.DataAnnotations;

namespace FileSharer.Web.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указано Имя")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Не указана фамилия")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
        //public string Avatar { get; set; }  // url? DB? 

        public int RoleId = 3;
    }
}
