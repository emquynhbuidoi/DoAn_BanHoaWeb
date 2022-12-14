//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace alo_alo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class AdminUser
    {
        [Required(ErrorMessage = "Please enter ID ..")]
        public int ID { get; set; }

        public string NameUser { get; set; }

        public string RoleUser { get; set; }

        [Required(ErrorMessage = "Please enter Password ..")]
        [DataType(DataType.Password)]
        public string PasswordUser { get; set; }


        [NotMapped]
        [Compare("PasswordUser", ErrorMessage = "faill Compare password !!")]
        [DataType(DataType.Password)]
        public string ConfirmPass { get; set; }
        [NotMapped]
        public string ErrorLogin { get; set; }
    }
}
