using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaGestionProductos.Modelos;

namespace SistemaGestionProductos.Datos
{
    public class ProductoDatos
    {
        
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            using (SqlConnection conexion = ConexionBD.AbrirConexion())
            {
             
            {
                string consulta = @"
            SELECT
                P.IdProducto,
                P.Nombre,
                P.Precio,
                P.Stock,
                P.FechaRegistro,
                P.IdCategoria,
                C.Nombre AS NombreCategoria
            FROM Productos P
            INNER JOIN Categorias C
                ON P.IdCategoria = C.IdCategoria";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Producto producto = new Producto();

                        producto.IdProducto = (int)lector["IdProducto"];
                        producto.Nombre = lector["Nombre"].ToString();
                        producto.Precio = (decimal)lector["Precio"];
                        producto.Stock = (int)lector["Stock"];
                        producto.FechaRegistro = (DateTime)lector["FechaRegistro"];
                        producto.IdCategoria = (int)lector["IdCategoria"];
                        producto.NombreCategoria = lector["NombreCategoria"].ToString();

                        lista.Add(producto);
                    }
                }
            }

            return lista;
        }
    }


        public int Agregar(Producto producto)
        {
            using (SqlConnection conexion = ConexionBD.AbrirConexion())
            {
                string consulta = @"
            INSERT INTO Productos
            (Nombre, Precio, Stock, FechaRegistro, IdCategoria)
            VALUES
            (@Nombre, @Precio, @Stock, @FechaRegistro, @IdCategoria);

            SELECT SCOPE_IDENTITY();";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", producto.Stock);
                    comando.Parameters.AddWithValue("@FechaRegistro", producto.FechaRegistro);
                    comando.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);

                    
                    return Convert.ToInt32(comando.ExecuteScalar());
                }
            }
        }


        public void Modificar(Producto producto)
        {
            using (SqlConnection conexion = ConexionBD.AbrirConexion())
            {
                string consulta = @"
                    UPDATE Productos
                    SET Nombre = @Nombre,
                        Precio = @Precio,
                        Stock = @Stock,
                        IdCategoria = @IdCategoria
                    WHERE IdProducto = @IdProducto";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdProducto", producto.IdProducto);
                    comando.Parameters.AddWithValue("@Nombre", producto.Nombre);
                    comando.Parameters.AddWithValue("@Precio", producto.Precio);
                    comando.Parameters.AddWithValue("@Stock", producto.Stock);
                    comando.Parameters.AddWithValue("@IdCategoria", producto.IdCategoria);

                    comando.ExecuteNonQuery();
                }
            }
        }

       
        public bool Eliminar(int idProducto)
        {
            
            using (SqlConnection conexion = ConexionBD.AbrirConexion())
            {
                string consulta = @"
            DELETE FROM Productos
            WHERE IdProducto = @IdProducto";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdProducto", idProducto);

                    int filasAfectadas = comando.ExecuteNonQuery();

                    return filasAfectadas > 0;
                }
            }
        }
    }
}
