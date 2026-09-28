namespace Ejercicio3.Models
{
    public class Desafio
    {
      
        private int id;
        private string titulo;
        private string dificultad;
        private int puntajeMaximo;
        private bool activo;

       
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Titulo
        {
            get { return titulo; }
            set { titulo = value; }
        }

        public string Dificultad
        {
            get { return dificultad; }
            set { dificultad = value; }
        }

        public int PuntajeMaximo
        {
            get { return puntajeMaximo; }
            set { puntajeMaximo = value; }
        }

        public bool Activo
        {
            get { return activo; }
            set { activo = value; }
        }
    }
}