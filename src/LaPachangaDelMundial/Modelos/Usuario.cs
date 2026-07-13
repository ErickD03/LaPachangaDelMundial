using System.Collections.Generic;

namespace LaPachangaDelMundial.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string CodigoPaisPreferido { get; set; }
        public int Puntos { get; set; }
        public List<string> Insignias { get; set; }
        public List<string> IdsQuinielas { get; set; }

        public Usuario()
        {
            Insignias = new List<string>();
            IdsQuinielas = new List<string>();
        }
    }
}