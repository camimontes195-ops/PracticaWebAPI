namespace Ejercicio6.Models
{
    public class ObjetoEspacial
    {
       
        private int id;
        private string nombre;
        private string tipo;
        private double distancia;
        private int nivelRiesgo;
        private DateTime fechaDescubrimiento;
        private bool activo;
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

        public double Distancia
        {
            get { return distancia; }
            set { distancia = value; }
        }

        public int NivelRiesgo
        {
            get { return nivelRiesgo; }
            set { nivelRiesgo = value; }
        }

        public DateTime FechaDescubrimiento
        {
            get { return fechaDescubrimiento; }
            set { fechaDescubrimiento = value; }
        }

        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }
    }
}