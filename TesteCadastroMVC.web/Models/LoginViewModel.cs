using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace TesteCadastroMVC.web.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Usuário:")]
        [Required]
        public string Usuario { get; set; }
        [Display(Name = "Senha:")]
        [DataType(DataType.Password)]
        [Required]
        public string Senha { get; set; }
        [Display(Name = "Lembrar Me")]
        public bool LembrarMe { get; set; }
    }

    public class ViewModels
    {
        public LoginViewModel Login { get; set; }
        public CadastrarViewModel User { get; set; }
    }
}