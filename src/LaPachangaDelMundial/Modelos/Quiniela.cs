using System.Collections.Generic;

namespace LaPachangaDelMundial.Models
{
    public enum TipoQuiniela
    {
        Publica,
        Privada
    }

    public class Quiniela
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public TipoQuiniela Tipo { get; set; }
        public string IdCreador { get; set; }
        public List<string> IdsIntegrantes { get; set; }
        public List<string> Notificaciones { get; set; }

        public Quiniela()
        {
            IdsIntegrantes = new List<string>();
            Notificaciones = new List<string>();
        }
    }
}