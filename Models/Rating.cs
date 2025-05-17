using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace apptienda.Models
{
    [Table("t_rating")]
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? UserName { get; set; }
        public Producto? Product { get; set; }
        public int RatingValue { get; set; }
        public DateTime FechaRating { get; set; }

    }
}