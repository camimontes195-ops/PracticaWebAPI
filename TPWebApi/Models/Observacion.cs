namespace Ejercicio6.Models
{
    public class Observacion
    {
        
        private int id;
        private int objetoEspacialId;
        private DateTime fecha;
        private double distanciaMedida;
        private double velocidad;
        private string comentario;

        
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int ObjetoEspacialId
        {
            get { return objetoEspacialId; }
            set { objetoEspacialId = value; }
        }

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public double DistanciaMedida
        {
            get { return distanciaMedida; }
            set { distanciaMedida = value; }
        }

        public double Velocidad
        {
            get { return velocidad; }
            set { velocidad = value; }
        }

        public string Comentario
        {
            get { return comentario; }
            set { comentario = value; }
        }
    }
}