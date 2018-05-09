using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelWebApp.ViewModels
{
    public class AccountAddRoleViewModel
    {
        [Required]
        [MaxLength(50)]
        public string RoleName { get; set; }
    }
}
