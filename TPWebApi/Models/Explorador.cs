namespace Ejercicio7.Models
{
    public class Explorador
    {
      
        private int id;
        private string nombre;
        private string especialidad;
        private int experienciaAnios;
        private bool disponible;

       
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Especialidad
        {
            get { return especialidad; }
            set { especialidad = value; }
        }

        public int ExperienciaAnios
        {
            get { return experienciaAnios; }
            set { experienciaAnios = value; }
        }

        public bool Disponible
        {
            get { return disponible; }
            set { disponible = value; }
        }
    }
}