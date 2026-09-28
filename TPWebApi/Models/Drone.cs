namespace Ejercicio4.Models
{
    public class Drone
    {
      
        private int id;
        private string codigo;
        private string modelo;
        private double bateria;
        private string estado;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public string Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }

        public double Bateria
        {
            get { return bateria; }
            set { bateria = value; }
        }

        public string Estado
        {
            get { return estado; }
            set { estado = value; }
        }
    }
}