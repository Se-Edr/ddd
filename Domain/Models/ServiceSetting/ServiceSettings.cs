using Domain.Events.Service;
using Shared;
using System.Text.Json.Serialization;

namespace Domain.Models.ServiceSetting
{
    public class ServiceSettings:MainEntity
    {
        public int Id { get; internal set; }
        private ServiceSettings(){}
        private ServiceSettings(int minutes,int price, Shift morn, Shift wholeDay )
        {
            Id = 1;
            BasePricePerWindow = price;
            BaseWindowInMinutes = minutes;
            Morning = morn;
            WholeDay = wholeDay;
        }
        public int BaseWindowInMinutes {get; internal set; }
        public int BasePricePerWindow { get; internal set; }

        public Shift Morning { get; internal set; }
        public Shift WholeDay { get; internal set; }

        public static ServiceSettings SetServiceSettings(int minutes,int price,Shift morn, Shift wholeDay)
        {
            ServiceSettings settings = new(minutes,price, morn,wholeDay);
            return settings;
        }

        public ServiceSettings EditServiceSettings(int minutes, int price, Shift morn, Shift wholeDay)
        {
            BasePricePerWindow = price;
            BaseWindowInMinutes = minutes;
            Morning = morn;
            WholeDay = wholeDay;
            AddDomainEvent(new SettingsUpdatedDomainEvent(BasePricePerWindow));
            return this;
        }


    }

    public struct Shift
    {
        public TimeOnly startTime { get;  }
        public TimeOnly finishTime { get;}

        
        public Shift(TimeOnly startTime, TimeOnly finishTime)
        {
            Result res=CheckDates(startTime, finishTime);
            if (!res.IsSucces)
            {
                throw new ValidationException(res.Error.Description);
            }
            this.startTime = startTime;
            this.finishTime = finishTime;
        }

        private Result CheckDates(TimeOnly start, TimeOnly finish)
        {
            if (start >= finish)
            {
                return Result.Failure(ApplicationError.Failure("EndTime must be after StartTime"));
            }
            return Result.Success();
        }

    }

    
}
