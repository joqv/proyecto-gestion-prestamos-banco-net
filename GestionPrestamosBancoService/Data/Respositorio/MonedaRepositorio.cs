using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using Microsoft.Data.SqlClient;

namespace GestionPrestamosBancoService.Data.Respositorio
{
    public class MonedaRepositorio : IMoneda
    {
        private readonly IConfiguration config;
        private readonly string cadenaConexion;

        public MonedaRepositorio(IConfiguration configuration)
        {
            config = configuration;
            cadenaConexion = config["ConnectionStrings:DB"];
        }

        public List<Moneda> ListarMonedas()
        {
            var listado = new List<Moneda>();
            using (var conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                using (var comando = new SqlCommand("ListarMonedas", conexion))
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
        private Moneda ConvertirReaderEnObjeto(SqlDataReader reader)
        {
            return new Moneda()
            {
                IdMoneda = reader.GetInt32(reader.GetOrdinal("IdMoneda")),
                Codigo = reader.GetString(reader.GetOrdinal("Codigo")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                Simbolo = reader.GetString(reader.GetOrdinal("Simbolo"))
            };
        }
    }
}
