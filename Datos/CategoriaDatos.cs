using System.Collections.Generic;
using System.Data.SqlClient;
using SistemaGestionProductos.Modelos;

namespace SistemaGestionProductos.Datos
{
    public class CategoriaDatos
    {
        
        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();

            using (SqlConnection conexion = ConexionBD.AbrirConexion())
            {
                string consulta = "SELECT IdCategoria, Nombre, Descripcion FROM Categorias";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Categoria categoria = new Categoria();

                        categoria.IdCategoria = (int)lector["IdCategoria"];
                        categoria.Nombre = lector["Nombre"].ToString();
                        categoria.Descripcion = lector["Descripcion"].ToString();

                        lista.Add(categoria);
                    }
                }
            }

            return lista;
        }

        
        public void Agregar(Categoria categoria)
        {
            using (SqlConnection conexion = ConexionBD.AbrirConexion())
            {
                string consulta = "INSERT INTO Categorias (Nombre, Descripcion) " +
                                  "VALUES (@Nombre, @Descripcion)";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    comando.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

                    comando.ExecuteNonQuery();
                }
            }
        }

       
        public void Modificar(Categoria categoria)
        {
            using (SqlConnection conexion = ConexionBD.AbrirConexion())
            {
                string consulta = "UPDATE Categorias SET Nombre = @Nombre, " +
                                  "Descripcion = @Descripcion " +
                                  "WHERE IdCategoria = @IdCategoria";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdCategoria", categoria.IdCategoria);
                    comando.Parameters.AddWithValue("@Nombre", categoria.Nombre);
                    comando.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

                    comando.ExecuteNonQuery();
                }
            }
        }

        
        public void Eliminar(int idCategoria)
        {
            using (SqlConnection conexion = ConexionBD.AbrirConexion())
            {
                string consulta = "DELETE FROM Categorias WHERE IdCategoria = @IdCategoria";

                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                {
                    comando.Parameters.AddWithValue("@IdCategoria", idCategoria);

                    comando.ExecuteNonQuery();
                }
            }
        }
    }
}