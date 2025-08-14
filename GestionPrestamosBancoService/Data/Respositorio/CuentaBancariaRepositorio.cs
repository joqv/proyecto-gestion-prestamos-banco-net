using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using GestionPrestamosBancoService.Models.Dto;
using Microsoft.Data.SqlClient;

namespace GestionPrestamosBancoService.Data.Respositorio
{
    public class CuentaBancariaRepositorio : ICuentaBancaria
    {
        private readonly IConfiguration config;
        private readonly string cadenaConexion;

        public CuentaBancariaRepositorio(IConfiguration configuration)
        {
            config = configuration;
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<CuentaBancaria> Listar()
        {
            var listado = new List<CuentaBancaria>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("ListarCuentasBancarias", conexion))
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

        public CuentaBancaria CrearCuentaBancaria(SolicitudCrearCuentaBancariaDto solicitud)
        {
            CuentaBancaria nuevaCuentaBancaria= null;

            int idCuenta = 0;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("CrearCuentaBancaria", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idCliente", solicitud.IdCliente);
                    cmd.Parameters.AddWithValue("@idTipoCuenta", solicitud.IdTipoCuenta);
                    cmd.Parameters.AddWithValue("@idMoneda", solicitud.IdMoneda);
                    cmd.Parameters.AddWithValue("@saldoInicial", solicitud.SaldoInicial);

                    idCuenta = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            nuevaCuentaBancaria = ObtenerCuentaBancariaPorID(idCuenta);

            return nuevaCuentaBancaria;
        }

        public CuentaBancaria ObtenerCuentaBancariaPorID(int id)
        {
            CuentaBancaria cuenta = null;

            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("ObtenerCuentaBancariaPorID", conexion))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader != null && reader.HasRows)
                        {
                            reader.Read();
                            cuenta = ConvertirReaderEnObjeto(reader);
                        }
                    }
                }
            }

            return cuenta;
        }


        // Metodos privados
        private CuentaBancaria ConvertirReaderEnObjeto(SqlDataReader reader)
        {
            return new CuentaBancaria()
            {
                IdCuenta = reader.GetInt32(reader.GetOrdinal("IdCuenta")),
                IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                IdTipoCuenta = reader.GetInt32(reader.GetOrdinal("IdTipoCuenta")),
                IdMoneda = reader.GetInt32(reader.GetOrdinal("IdMoneda")),
                NumeroCuenta = reader.GetString(reader.GetOrdinal("NumeroCuenta")),
                Saldo = reader.GetDecimal(reader.GetOrdinal("Saldo")),
                FechaApertura = reader.GetDateTime(reader.GetOrdinal("FechaApertura"))
            };
        }
    }
}
