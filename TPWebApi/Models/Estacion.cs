namespace Ejercicio2.Models
{
    public class Estacion
    {
  
        private int id;
        private string nombre;
        private string localidad;
        private bool activa;

        
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

        public string Localidad
        {
            get { return localidad; }
            set { localidad = value; }
        }

        public bool Activa
        {
            get { return activa; }
            set { activa = value; }
        }
    }
}