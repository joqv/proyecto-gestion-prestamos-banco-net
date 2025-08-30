using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GestionPrestamosBancoService.Data.Respositorio
{
    public class UsuarioRepository:IUsuario
    {
        private readonly IConfiguration config;
        private readonly string cadenaConexion;

        public UsuarioRepository(IConfiguration configuration)
        {
            config = configuration;
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public Usuario Login(string username, string password)
        {
            Usuario usuario = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("LoginUsuario", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            reader.Read();
                            usuario = ConvertirReaderEnObjeto(reader);
                        }
                    }
                }
            }

            return usuario;
        }

        public Usuario ObtenerPorUsername(string username)
        {
            Usuario usuario = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("ObtenerUsuarioPorUsername", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            reader.Read();
                            usuario = ConvertirReaderEnObjeto(reader);
                        }
                    }
                }
            }

            return usuario;
        }
        private Usuario ConvertirReaderEnObjeto(SqlDataReader reader)
        {
            return new Usuario()
            {
                IdUsuario = reader.GetInt32(reader.GetOrdinal("IdUsuario")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente"))
            };
        }
    }
}
