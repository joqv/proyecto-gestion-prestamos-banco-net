namespace GestionPrestamosBancoService.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public int IdCliente { get; set; }
    }
}
