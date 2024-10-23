using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Src.Models
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string NombreRole { get; set; } = string.Empty;
    }
}