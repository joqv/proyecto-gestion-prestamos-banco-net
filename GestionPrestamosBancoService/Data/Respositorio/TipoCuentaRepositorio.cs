using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using Microsoft.Data.SqlClient;

namespace GestionPrestamosBancoService.Data.Respositorio
{
    public class TipoCuentaRepositorio : ITipoCuenta
    {
        private readonly IConfiguration config;
        private readonly string cadenaConexion;

        public TipoCuentaRepositorio(IConfiguration configuration)
        {
            config = configuration;
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<TipoCuenta> ListarTipoCuenta()
        {
            var listado = new List<TipoCuenta>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("ListarTiposCuenta", conexion))
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
        private TipoCuenta ConvertirReaderEnObjeto(SqlDataReader reader)
        {
            return new TipoCuenta()
            {
                IdTipoCuenta= reader.GetInt32(reader.GetOrdinal("IdTipoCuenta")),
                NombreTipo = reader.GetString(reader.GetOrdinal("NombreTipo")),
                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion"))
            };
        }
    }
}
