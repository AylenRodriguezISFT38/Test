using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DB
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public float Precio { get; set; }
        public int IdTipoProducto { get; set; }
        public DateTime Created_At { get; set; }
        public bool Deleted { get; set; }

        [ForeignKey("IdTipoProducto")]
        public virtual TipoProducto TipoProducto { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
