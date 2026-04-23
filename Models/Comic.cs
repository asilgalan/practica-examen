using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticandoExamenViernes.Models
{

    [Table("Comics")]
    public class Comic
    {


        [Key]

        [Column("Id")]
      
        public int Id { get; set; }
        [Column("Titulo")]
        public string Titulo { get; set; }
        [Column("Imagen")]

        public string Imagen { get; set; }
        [Column("Descripcion")]
        public string Descripcion { get; set; }
    }
}
