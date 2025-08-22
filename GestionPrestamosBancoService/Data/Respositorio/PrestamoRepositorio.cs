using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using GestionPrestamosBancoService.Models.Dto;
using Microsoft.Data.SqlClient;

namespace GestionPrestamosBancoService.Data.Respositorio
{
    public class PrestamoRepositorio : IPrestamo
    {
        private readonly IConfiguration config;
        private readonly string cadenaConexion;

        public PrestamoRepositorio(IConfiguration configuration)
        {
            config = configuration;
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public Prestamo AgregarPrestamo(SolicitudPrestamoDto solicitud)
        {
            Prestamo nuevoPrestamo = null;

            int nuevoID = 0;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("AgregarPrestamo", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCliente", solicitud.IdCliente);
                    cmd.Parameters.AddWithValue("@idMoneda", solicitud.IdMoneda);
                    cmd.Parameters.AddWithValue("@montoPrincipal", solicitud.MontoPrincipal);
                    cmd.Parameters.AddWithValue("@tasaInteres", solicitud.TasaInteres);
                    cmd.Parameters.AddWithValue("@plazoMeses", solicitud.PlazoMeses);

                    nuevoID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            nuevoPrestamo = ObtenerPrestamoPorId(nuevoID);

            return nuevoPrestamo;
        }

        public List<Prestamo> Listar()
        {
            var listado = new List<Prestamo>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("ListarPrestamos", conexion))
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

        public Prestamo ObtenerPrestamoPorId(int id)
        {
            Prestamo prestamo = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("ObtenerPrestamoPorId", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            reader.Read();
                            prestamo = ConvertirReaderEnObjeto(reader);
                        }
                    }
                }
            }

            return prestamo;
        }

        public List<Prestamo> ObtenerPrestamosPorCliente(int id)
        {
            List<Prestamo> listado = new List<Prestamo>();

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("ObtenerPrestamosPorCliente", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCliente", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listado.Add(ConvertirReaderEnObjeto(reader));
                            }
                        }
                    }
                }
            }

            return listado;
        }

        // Metodos privados
        private Prestamo ConvertirReaderEnObjeto(SqlDataReader reader)
        {
            return new Prestamo()
            {
                IdPrestamo = reader.GetInt32(reader.GetOrdinal("IdPrestamo")),
                IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                IdMoneda= reader.GetInt32(reader.GetOrdinal("IdMoneda")),
                MontoPrincipal = reader.GetDecimal(reader.GetOrdinal("MontoPrincipal")),
                TasaInteres = reader.GetDecimal(reader.GetOrdinal("TasaInteres")),
                PlazoMeses = reader.GetInt32(reader.GetOrdinal("PlazoMeses")),
                FechaInicio = reader.GetDateTime(reader.GetOrdinal("FechaInicio")),
                FechaFinEstimada= reader.GetDateTime(reader.GetOrdinal("FechaFinEstimada")),
                SaldoPendiente = reader.GetDecimal(reader.GetOrdinal("SaldoPendiente")),
                EstadoPrestamo = reader.GetString(reader.GetOrdinal("EstadoPrestamo")),
            };
    }
    }
}
