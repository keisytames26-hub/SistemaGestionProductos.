using System;
using System.Collections.Generic;
using SistemaGestionProductos.Datos;
using SistemaGestionProductos.Modelos;

namespace SistemaGestionProductos.Logica
{
    public class CategoriaLogica
    {
        private CategoriaDatos datos = new CategoriaDatos();

        public List<Categoria> Listar()
        {
            return datos.Listar();
        }

        
        public void Agregar(Categoria categoria)
        {
            if (categoria == null)
                throw new Exception("La categoría no es válida.");

            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new Exception("Debe ingresar el nombre de la categoría.");

            if (categoria.Nombre.Length > 100)
                throw new Exception("El nombre no puede superar los 100 caracteres.");

            if (string.IsNullOrWhiteSpace(categoria.Descripcion))
                throw new Exception("Debe ingresar una descripción.");

            if (categoria.Descripcion.Length > 200)
                throw new Exception("La descripción no puede superar los 200 caracteres.");

            datos.Agregar(categoria);
        }

        public void Modificar(Categoria categoria)
        {
            if (categoria == null)
                throw new Exception("La categoría no es válida.");

            if (categoria.IdCategoria <= 0)
                throw new Exception("Seleccione una categoría válida.");

            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new Exception("Debe ingresar el nombre de la categoría.");

            if (categoria.Nombre.Length > 100)
                throw new Exception("El nombre no puede superar los 100 caracteres.");

            if (string.IsNullOrWhiteSpace(categoria.Descripcion))
                throw new Exception("Debe ingresar una descripción.");

            if (categoria.Descripcion.Length > 200)
                throw new Exception("La descripción no puede superar los 200 caracteres.");

            datos.Modificar(categoria);
        }

        
        public void Eliminar(int idCategoria)
        {
            if (idCategoria <= 0)
                throw new Exception("Seleccione una categoría válida.");

            datos.Eliminar(idCategoria);
        }
    }
}