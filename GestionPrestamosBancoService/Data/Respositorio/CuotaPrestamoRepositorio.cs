using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using GestionPrestamosBancoService.Models.Dto;
using Microsoft.Data.SqlClient;

namespace GestionPrestamosBancoService.Data.Respositorio
{
    public class CuotaPrestamoRepositorio : ICuotaPrestamo
    {
        private readonly IConfiguration config;
        private readonly string cadenaConexion;

        public CuotaPrestamoRepositorio(IConfiguration configuration)
        {
            config = configuration;
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<CuotaPrestamo> ListarCuotas()
        {
            var listado = new List<CuotaPrestamo>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("ListarCuotasPrestamo", conexion))
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

        public CuotaPrestamo ObtenerCuotaPorID(int id)
        {
            CuotaPrestamo cuotaPrestamo = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("ObtenerCuotaPorId", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            reader.Read();
                            cuotaPrestamo = ConvertirReaderEnObjeto(reader);
                        }
                    }
                }
            }

            return cuotaPrestamo;
        }

        public CuotaPrestamo PagarCuotaPrestamo(SolicitudPagoCuotaPrestamoDto solicitud)
        {
            CuotaPrestamo cuotaPagada = null;

            int idCuota = 0;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("PagarCuotaPrestamo", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCuota", solicitud.IdCuota);
                    cmd.Parameters.AddWithValue("@idCuentaOrigen", solicitud.IdCuentaOrigen);
                    cmd.Parameters.AddWithValue("@montoAPagar", solicitud.MontoAPagar);

                    idCuota = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            cuotaPagada = ObtenerCuotaPorID(idCuota);

            return cuotaPagada;
        }

        // Metodos privados
        private CuotaPrestamo ConvertirReaderEnObjeto(SqlDataReader reader)
        {
            return new CuotaPrestamo()
            {
                IdCuota = reader.GetInt32(reader.GetOrdinal("IdCuota")),
                IdPrestamo = reader.GetInt32(reader.GetOrdinal("IdPrestamo")),
                NumeroCuota = reader.GetInt32(reader.GetOrdinal("NumeroCuota")),
                FechaVencimiento = reader.GetDateTime(reader.GetOrdinal("FechaVencimiento")),
                MontoTotalCuota = reader.GetDecimal(reader.GetOrdinal("MontoTotalCuota")),
                MontoCapital = reader.GetDecimal(reader.GetOrdinal("MontoCapital")),
                MontoInteres = reader.GetDecimal(reader.GetOrdinal("MontoInteres")),
                SaldoPendiente = reader.GetDecimal(reader.GetOrdinal("SaldoPendiente")),
                MontoPagado = reader.GetDecimal(reader.GetOrdinal("MontoPagado")),
                EstadoCuota = reader.GetString(reader.GetOrdinal("EstadoCuota"))
            };
        }
    }
}
