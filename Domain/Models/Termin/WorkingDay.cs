

using Domain.Models.Appointment;
using Domain.Models.ServiceSetting;

namespace Domain.Models.Termin
{
    public class WorkingDay
    {
        private WorkingDay()
        {
            
        }
        private WorkingDay(DateOnly day,Shift shift,string sh)
        {
            DayId = Guid.NewGuid();
            Date = day;
            StartTime = shift.startTime;
            EndTime = shift.finishTime;
            ShiftString = sh;

        }
        public Guid DayId { get; internal set; }
        public DateOnly Date { get; internal set; }

        public TimeOnly StartTime { get; internal set;}
        public TimeOnly EndTime { get; internal set;}
        public string ShiftString { get;internal set;}

        public IList<Meeting> meetings { get; internal set; } = [];


        public static WorkingDay CreateDay(DateTime date, Shift shift,string sh)
        {
            DateOnly day = DateOnly.FromDateTime(date);
            return new WorkingDay(day,shift,sh);
        }
        public bool EditDay(Shift shift,string sh)
        {
            StartTime = shift.startTime;
            EndTime = shift.finishTime;
            ShiftString = sh;
            return true;
        }

        public void AddMeetingToDay(Meeting meeting)
        {
            meetings.Add(meeting);
        }

    }
}
