namespace Ejercicio5.Models
{
    public class ReservaActividad
    {
       
        private int id;
        private int actividadId;
        private int participanteId;
        private DateTime fechaReserva;

      
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public int ActividadId
        {
            get { return actividadId; }
            set { actividadId = value; }
        }

        public int ParticipanteId
        {
            get { return participanteId; }
            set { participanteId = value; }
        }

        public DateTime FechaReserva
        {
            get { return fechaReserva; }
            set { fechaReserva = value; }
        }
    }
}