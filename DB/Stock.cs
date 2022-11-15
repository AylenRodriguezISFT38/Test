using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DB
{
    public class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int IdProducto{ get; set; }
        public float Cantidad { get; set; }
        public DateTime Created_At { get; set; }
        public bool Deleted { get; set; }
        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; }

    }
}
