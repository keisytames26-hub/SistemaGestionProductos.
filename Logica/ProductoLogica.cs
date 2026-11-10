using SistemaGestionProductos.Datos;
using SistemaGestionProductos.Modelos;
using System;
using System.Collections.Generic;

namespace SistemaGestionProductos.Logica
{
    public class ProductoLogica
    {
        private ProductoDatos datos = new ProductoDatos();

        public List<Producto> Listar()
        {
            return datos.Listar();
        }

        public int Agregar(Producto producto)
        {
            ValidarProducto(producto);

            producto.FechaRegistro = DateTime.Now;

            return datos.Agregar(producto);
        }

        public void Modificar(Producto producto)
        {
            if (producto == null)
                throw new Exception("No se recibió información del producto.");

            if (producto.IdProducto <= 0)
                throw new Exception("Seleccione un producto válido.");

            ValidarProducto(producto);

            datos.Modificar(producto);
        }

        public void Eliminar(int idProducto)
        {
           
            if (idProducto <= 0)
                throw new Exception("Seleccione un producto válido.");

            bool eliminado = datos.Eliminar(idProducto);

            if (!eliminado)
                throw new Exception("No se encontró el producto en la base de datos.");
        }
        

        public void ValidarCantidadSeleccionada(int cantidad, int stock)
        {
            if (cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor que cero.");

            if (cantidad > stock)
                throw new Exception("La cantidad seleccionada supera el stock disponible.");
        }

        private void ValidarProducto(Producto producto)
        {
            if (producto == null)
                throw new Exception("No se recibió información del producto.");

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new Exception("El nombre del producto es obligatorio.");

            if (producto.Nombre.Length > 100)
                throw new Exception("El nombre no puede superar los 100 caracteres.");

            if (producto.Precio <= 0)
                throw new Exception("El precio debe ser mayor que cero.");

            if (producto.Stock < 0)
                throw new Exception("El stock no puede ser negativo.");

            if (producto.IdCategoria <= 0)
                throw new Exception("Debe seleccionar una categoría.");
        }

    }



}