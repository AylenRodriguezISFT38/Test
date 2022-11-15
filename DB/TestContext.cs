using Microsoft.EntityFrameworkCore;
using System;

namespace DB
{
    public class TestContext:DbContext
    {
        public TestContext(DbContextOptions<TestContext>options) : base(options) { 

        }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Stock> Stock{ get; set; }
        public DbSet<TipoProducto> TipoProducto{ get; set; }
    
    }
}
