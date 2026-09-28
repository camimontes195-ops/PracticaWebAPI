using System;

namespace Ejercicio1ObjetosPerdidos.Models
{
    public class ObjetoPerdido
    {
        // Atributos privados
        private int id;
        private string descripcion;
        private string categoria;
        private string lugarEncontrado;
        private DateTime fechaEncontrado;
        private bool reclamado;
        private string nombrePersonaQueRetiro;

        // Propiedades públicas
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

        public string LugarEncontrado
        {
            get { return lugarEncontrado; }
            set { lugarEncontrado = value; }
        }

        public DateTime FechaEncontrado
        {
            get { return fechaEncontrado; }
            set { fechaEncontrado = value; }
        }

        public bool Reclamado
        {
            get { return reclamado; }
            set { reclamado = value; }
        }

        public string NombrePersonaQueRetiro
        {
            get { return nombrePersonaQueRetiro; }
            set { nombrePersonaQueRetiro = value; }
        }
    }
}