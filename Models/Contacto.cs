using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace apptienda.Models
{
    public class Contacto
    {
        [Required(ErrorMessage = "Nombres son obligatorios.")]
        public string? Nombres { get; set; }
        [NotNull]
        public string? Email { get; set; }
        [NotNull]
        public string? Mensaje { get; set; }
    }
}
