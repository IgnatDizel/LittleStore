using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LittleStore.Models
{
    public class User
    {

        public int UserId { get; set; }

        [Required(ErrorMessage = "Введите ФИО")]
        [Display(Name = "ФИО")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите Логин")]
        [Display(Name = "Логин")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите Пароль")]
        [Display(Name = "Пароль")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string Password { get; set; }

        public DateTime? DateReg { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [Display(Name = "Номер телефона")]
        [MaxLength(50, ErrorMessage = "Превышена максимальная длина записи")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите Статус")]
        [Display(Name = "Статус")]
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }

    }
}