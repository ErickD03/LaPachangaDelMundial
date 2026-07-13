namespace LaPachangaDelMundial.Models
{
    public class Pronostico
    {
        public string IdUsuario { get; set; }
        public string IdPartido { get; set; }
        public int GolesLocal { get; set; }
        public int GolesVisitante { get; set; }
        public int PuntosObtenidos { get; set; }

        public Pronostico() { }
    }
}