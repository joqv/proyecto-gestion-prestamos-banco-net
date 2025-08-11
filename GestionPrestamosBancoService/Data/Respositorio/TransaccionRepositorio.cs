using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using Microsoft.Data.SqlClient;

namespace GestionPrestamosBancoService.Data.Respositorio
{
    public class TransaccionRepositorio : ITransaccion
    {
        private readonly IConfiguration config;
        private readonly string cadenaConexion;

        public TransaccionRepositorio(IConfiguration configuration)
        {
            config = configuration;
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<Transaccion> Listar()
        {
            var listado = new List<Transaccion>();
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
        private Transaccion ConvertirReaderEnObjeto(SqlDataReader reader)
        {
            return new Transaccion()
            {
                IdTransaccion = reader.GetInt32(reader.GetOrdinal("IdTransaccion")),
                IdCuentaOrigen = reader.GetInt32(reader.GetOrdinal("IdCuentaOrigen")),
                IdCuentaDestino = reader.GetInt32(reader.GetOrdinal("IdCuentaDestino")),
                TipoTransaccion= reader.GetString(reader.GetOrdinal("TipoTransaccion")),
                Monto = reader.GetDecimal(reader.GetOrdinal("Monto")),
                IdMoneda = reader.GetInt32(reader.GetOrdinal("IdMoneda")),
                FechaHoraTransaccion = reader.GetDateTime(reader.GetOrdinal("FechaHoraTransaccion")),
                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
            };
        }
    }
}
