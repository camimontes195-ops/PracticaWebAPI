namespace Ejercicio3.Models
{
    public class Participante
    {
      
        private int id;
        private string nombre;
        private string email;
        private string nivel;

       
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

        public string Nivel
        {
            get { return nivel; }
            set { nivel = value; }
        }
    }
}