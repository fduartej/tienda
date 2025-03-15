using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apptienda.Models
{
    public class Customer
    {
        public string UserName { get; set; }
        [Required(ErrorMessage = "DNI es obligatorios.")]
        public string DNI { get; set; }
        [Required(ErrorMessage = "Fecha nacimiento es obligatorios.")]
        public date BirthDate { get; set; }
        [Required(ErrorMessage = "Direccion es obligatorios.")]
        public string Address { get; set; }

    }
}