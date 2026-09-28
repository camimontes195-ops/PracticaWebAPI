namespace Ejercicio4.Models
{
    public class Mision
    {
    
        private int id;
        private int droneId;
        private string descripcion;
        private double distanciaKm;
        private DateTime fecha;
        private bool completada;

      
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int DroneId
        {
            get { return droneId; }
            set { droneId = value; }
        }

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        public double DistanciaKm
        {
            get { return distanciaKm; }
            set { distanciaKm = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public bool Completada
        {
            get { return completada; }
            set { completada = value; }
        }
    }
}