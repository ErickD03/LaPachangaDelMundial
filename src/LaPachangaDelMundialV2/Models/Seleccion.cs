namespace LaPachangaDelMundialV2.Models
{
    public class Seleccion
    {
        /// <summary>
        /// como BRA, ARG, FRA
        /// </summary>
        public string Codigo { get; set; }
        /// <summary>
        /// completo: Costa Rica
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// A, B, C, D
        /// </summary>
        public string Grupo { get; set; }
        /// <summary>
        /// png bandera
        /// </summary>
        public string RutaBandera { get; set; }

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