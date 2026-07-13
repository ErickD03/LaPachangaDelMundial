namespace LaPachangaDelMundial.Models
{
    public class Seleccion
    {
        public string Codigo { get; set; }      // Que sería como BRA, ARG, FRA
        public string Nombre { get; set; }      // Completo: Costa Rica
        public string Grupo { get; set; }       // A, B, C, D, E
        public string RutaBandera { get; set; } // Ruta del png bandera

        public Seleccion() { }

        public Seleccion(string codigo, string nombre, string grupo, string rutaBandera)
        {
            Codigo = codigo;
            Nombre = nombre;
            Grupo = grupo;
            RutaBandera = rutaBandera;
        }
    }
}