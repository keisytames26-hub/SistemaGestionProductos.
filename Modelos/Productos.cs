
using System;

namespace SistemaGestionProductos.Modelos
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdCategoria { get; set; }
        public string NombreCategoria { get; set; }

        
        public int CantidadSeleccionada { get; set; }

        
        public decimal SubtotalSeleccionado
        {
            get
            {
                return Precio * CantidadSeleccionada;
            }
        }

        
        public decimal TotalInventario
        {
            get
            {
                return Precio * Stock;
            }
        }
    }
}
