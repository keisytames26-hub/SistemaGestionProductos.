using System.Data.SqlClient;

namespace SistemaGestionProductos.Datos
{
    public class ConexionBD
    {

        private static readonly string CadenaConexion =
    @"Data Source=LAPTOP-10PJN0TF\SQLEXPRESS;Initial Catalog=SistemaGestionProductos;Integrated Security=True;Encrypt=True;TrustServerCertificate=True;";


        public static SqlConnection AbrirConexion()
        {
            SqlConnection conexion = new SqlConnection(CadenaConexion);

            conexion.Open();

            return conexion;
        }
    }
}