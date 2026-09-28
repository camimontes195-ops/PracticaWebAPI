namespace Ejercicio2.Models
{
    public class Medicion
    {
      
        private int id;
        private int estacionId;
        private double temperatura;
        private double humedad;
        private double velocidadViento;
        private DateTime fechaHora;

       
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int EstacionId
        {
            get { return estacionId; }
            set { estacionId = value; }
        }

        public double Temperatura
        {
            get { return temperatura; }
            set { temperatura = value; }
        }

        public double Humedad
        {
            get { return humedad; }
            set { humedad = value; }
        }

        public double VelocidadViento
        {
            get { return velocidadViento; }
            set { velocidadViento = value; }
        }

        public DateTime FechaHora
        {
            get { return fechaHora; }
            set { fechaHora = value; }
        }
    }
}