namespace Ejercicio5.Models
{
    public class Actividad
    {
       
        private int id;
        private string nombre;
        private string tipo;
        private DateTime horario;
        private int duracionMinutos;
        private int capacidad;
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

        public string Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        public DateTime Horario
        {
            get { return horario; }
            set { horario = value; }
        }

        public int DuracionMinutos
        {
            get { return duracionMinutos; }
            set { duracionMinutos = value; }
        }

        public int Capacidad
        {
            get { return capacidad; }
            set { capacidad = value; }
        }

        public bool Activa
        {
            get { return activa; }
            set { activa = value; }
        }
    }
}