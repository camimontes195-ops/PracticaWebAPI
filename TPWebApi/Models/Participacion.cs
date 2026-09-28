namespace Ejercicio7.Models
{
    public class Participacion
    {
        
        private int id;
        private int expedicionId;
        private int exploradorId;
        private string rol;

        
        
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int ExpedicionId
        {
            get { return expedicionId; }
            set { expedicionId = value; }
        }

        public int ExploradorId
        {
            get { return exploradorId; }
            set { exploradorId = value; }
        }

        public string Rol
        {
            get { return rol; }
            set { rol = value; }
        }
    }
}