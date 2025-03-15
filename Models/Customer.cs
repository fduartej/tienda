using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace apptienda.Models
{
    public class Customer
    {
        public string? UserName { get; set; }
        [Required(ErrorMessage = "DNI es obligatorios.")]
        public string? DNI { get; set; }
        [Required(ErrorMessage = "Fecha nacimiento es obligatorios.")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Direccion es obligatorios.")]
        public string? Address { get; set; }

    }
}