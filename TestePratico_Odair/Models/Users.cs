using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using static TestePratico_Odair.Class.UserHelper;

namespace TestePratico_Odair.Models
{
    public class Users
    {

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Este Campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "É permitido no máximo 255 caracteres")]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Este Campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "É permitido no máximo 255 caracteres")]
        [Display(Name = "SobreNome")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Este Campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "É permitido no máximo 255 caracteres")]
        [Index("User_UserName_Index)", IsUnique = true)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este Campo é obrigatório")]
        [Display(Name = "Celular")]
        [DataType(DataType.PhoneNumber)]
        public string CellPhone { get; set; }

        [Required(ErrorMessage = "Este Campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "É permitido no máximo 255 caracteres")]
        [Display(Name = "RG")]
        [DataType(DataType.PhoneNumber)]
        public string RG { get; set; }

        [Required(ErrorMessage = "Este Campo é obrigatório")]
        [CustomValidationCPF(ErrorMessage = "CPF inválido")]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

    }
}