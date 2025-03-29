using System.ComponentModel.DataAnnotations.Schema;

namespace apptienda.Models
{
    [Table("t_order")]
    public class Orden
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        public string? UserID { get; set; }

        public Decimal Total { get; set; }

        public DateTime Fecha { get; set; }

        public string? Status { get; set; }
    }
}