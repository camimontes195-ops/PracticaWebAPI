namespace Ejercicio5.Models
{
    public class Participante
    {
      
        private int id;
        private string nombre;
        private string email;
        private int edad;

    
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

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public int Edad
        {
            get { return edad; }
            set { edad = value; }
        }
    }
}