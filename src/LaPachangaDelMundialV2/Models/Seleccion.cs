namespace LaPachangaDelMundialV2.Models
{
    /// <summary>
    /// representa una selección
    /// </summary>
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
        /// <summary>
        /// inicializa una selección vacía
        /// </summary>
        public Seleccion() { }
        /// <summary>
        /// inicializa una selección con sus datos
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="nombre"></param>
        /// <param name="grupo"></param>
        /// <param name="rutaBandera"></param>
        public Seleccion(string codigo, string nombre, string grupo, string rutaBandera)
        {
            Codigo = codigo;
            Nombre = nombre;
            Grupo = grupo;
            RutaBandera = rutaBandera;
        }
    }
}