using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using SistemaGestionProductos.Modelos;

namespace Tarea_ProgramacionII_SistemaGestionProductos.Almacenamiento
{
    public class ProductoSeleccionadoArchivo
    {
        private readonly string rutaArchivo =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "ProductosSeleccionados.json"
            );

        public void Guardar(List<Producto> productos)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string json = serializer.Serialize(productos);

            File.WriteAllText(rutaArchivo, json);
        }

        public List<Producto> Cargar()
        {
            if (!File.Exists(rutaArchivo))
                return new List<Producto>();

            string json = File.ReadAllText(rutaArchivo);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Deserialize<List<Producto>>(json)
                   ?? new List<Producto>();
        }
    }
}