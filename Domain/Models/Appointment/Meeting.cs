using Domain.Models.Operation;
using Domain.Models.Termin;

namespace Domain.Models.Appointment
{
    public class Meeting
    {
        private Meeting()
        {
        }
        public Meeting(
             WorkingDay day,
            List<Procedure> proceds,
            string description,
            int? price,
            string spz,
            Guid? carId
            )
        {
            MeetId = Guid.NewGuid();
            ForCarSPZ = spz;
            ForCarId = carId;
            Description = description;
            StartingWorkingDayDay = day;
            Procedures=proceds;
            CalculatedPrice = price;
        }

        public Guid MeetId { get; private set;}
        public string ForCarSPZ { get; private set;}
        public Guid? ForCarId { get; private set;}
        public string Description { get; private set;}
        public WorkingDay? StartingWorkingDayDay { get; private set; }

        public DateOnly? StartingDate { get; private set;}
        public bool ActiveMeeting { get; private set; } = false;
        public DateOnly? FinishDate { get; private set; }

        public IList<Procedure> Procedures { get; private set; } = [];
        public bool ConfirmedByUser { get; private set; } = false;
        public bool ConfirmedByAdmin { get; private set; } = false;


        public int? CalculatedPrice { get; private set; }


        public static Meeting CreateMeet(
            WorkingDay? day,
            List<Procedure>? proceds,
            string description,
            int? price,
            string spz,
            Guid? carId)
        {

            Meeting meeting = new Meeting(day,proceds,description,price.Value,spz,carId);
            return meeting;

        }

        public bool ConfirmByUser()
        {
            ConfirmedByUser = true;
            return true;
        }

        public bool ConfirmByAdmin()
        {
            ConfirmedByAdmin = true;
            return true;
        }

        public bool StartRepair()
        {
            ActiveMeeting = true;
            return true;
        }
        public bool FinishCarRepair()
        {
            ActiveMeeting = false;
            return true;
        }

    }
}
