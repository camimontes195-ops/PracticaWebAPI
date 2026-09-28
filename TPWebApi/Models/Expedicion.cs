namespace Ejercicio7.Models
{
    public class Expedicion
    {
       
        private int id;
        private string nombre;
        private string destino;
        private DateTime fechaInicio;
        private DateTime fechaFin;
        private int capacidad;
        private string estado;

       
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

        public string Destino
        {
            get { return destino; }
            set { destino = value; }
        }

        public DateTime FechaInicio
        {
            get { return fechaInicio; }
            set { fechaInicio = value; }
        }

        public DateTime FechaFin
        {
            get { return fechaFin; }
            set { fechaFin = value; }
        }

        public int Capacidad
        {
            get { return capacidad; }
            set { capacidad = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}