using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using Microsoft.Data.SqlClient;

namespace GestionPrestamosBancoService.Data.Respositorio
{
    public class ClienteRepositorio : ICliente
    {
        private readonly IConfiguration config;
        private readonly string cadenaConexion;

        public ClienteRepositorio(IConfiguration configuration)
        {
            config = configuration;
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<Cliente> Listar()
        {
            var listado = new List<Cliente>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("ListarClientes", conexion))
                {
                    comando.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var reader = comando.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listado.Add(ConvertirReaderEnObjeto(reader));
                        }
                    }
                }

            }
            return listado;
        }

        // Metodos privados
        private Cliente ConvertirReaderEnObjeto(SqlDataReader reader)
        {
            return new Cliente()
            {
                IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                Apellido = reader.GetString(reader.GetOrdinal("Apellido")),
                NumeroDocumento = reader.GetString(reader.GetOrdinal("NumeroDocumento")),
                Telefono = reader.GetString(reader.GetOrdinal("Telefono")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
            };
        }
    }
}
