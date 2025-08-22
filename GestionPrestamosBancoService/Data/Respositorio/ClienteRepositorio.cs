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
        public Cliente ObtenerPorID(int id)
        {
            Cliente cliente = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("ObtenerClientePorID", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            reader.Read();
                            cliente = ConvertirReaderEnObjeto(reader);
                        }
                    }
                }
            }

            return cliente;
        }

        public Cliente RegistrarCliente(Cliente cliente)
        {
            Cliente nuevoCliente = null;

            int nuevoID = 0;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("RegistrarCliente", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", cliente.Nombre);
                    cmd.Parameters.AddWithValue("@apellido", cliente.Apellido);
                    cmd.Parameters.AddWithValue("@numeroDocumento", cliente.NumeroDocumento);
                    cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);
                    cmd.Parameters.AddWithValue("@email", cliente.Email);

                    nuevoID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            nuevoCliente = ObtenerPorID(nuevoID);

            return nuevoCliente;
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
