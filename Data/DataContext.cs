using LicoreriaBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LicoreriaBackend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Bodega> Bodegas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Detalle_compra> DetallesCompras { get; set; }
        public DbSet<Detalle_venta> DetalleVentas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Compra - Usuario (Uno a muchos)
            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Compras)  // Asegúrate de que Usuario tenga ICollection<Compra> Compras
                .HasForeignKey(c => c.Idusuario);


            // Configuración de Compra - Proveedor (Uno a muchos)
            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Proveedor)
                .WithMany(p => p.Compras)  // Asegúrate de que Proveedor tenga ICollection<Compra> Compras
                .HasForeignKey(c => c.Id_proveedor);


            // Configuración de Producto - Categoria (Uno a muchos)
            modelBuilder.Entity<Producto>()
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Productos)  // Categoria debe tener ICollection<Producto> Productos
                .HasForeignKey(p => p.CodigoCategoria);


            // Configuración de Producto - Bodega (Uno a uno o uno a muchos)
            modelBuilder.Entity<Bodega>()
                .HasMany(b => b.Productos)
                .WithOne(p => p.Bodegas)
                .HasForeignKey(p => p.Id_bodega);


            // Configuración de Compra - Detalle_compra (Uno a muchos)
            modelBuilder.Entity<Detalle_compra>()
                .HasOne(dc => dc.Compra)
                .WithMany(c => c.DetallesCompras)
                .HasForeignKey(dc => dc.Id_compra);


            // Configuración de Detalle_venta - Venta (uno a muchos)
            modelBuilder.Entity<Detalle_venta>()
                .HasOne(dv => dv.Venta)
                .WithMany(v => v.DetallesVentas)  // Venta debe tener ICollection<Detalle_venta> DetallesVentas
                .HasForeignKey(dv => dv.Id_venta);


            // Configuración de Detalle_venta - Producto (uno a muchos)
            modelBuilder.Entity<Detalle_venta>()
                .HasOne(dv => dv.Producto)
                .WithMany(p => p.DetallesVentas)  // Producto debe tener ICollection<Detalle_venta> DetallesVentas
                .HasForeignKey(dv => dv.Id_producto);


        }


    }
}
