using Domain.Models.ServiceSetting;
using Domain.Repositories;


namespace Domain.Models.Operation
{
    public class Procedure:MainEntity
    {
        private Procedure()
        {
            
        }

        private Procedure(string name,int windows,int price)
        {
            ProcedureId = Guid.NewGuid();
            ProcedureName = name;
            TakeWindows = windows;
            Price = price;
        }
        public Guid ProcedureId { get; internal set; }
        public string ProcedureName { get; internal set;}
        public bool fixedPrice { get; internal set; } = false;

        public int TakeWindows { get; internal set; }

        public int Price { get; internal set; }

        internal static Procedure CreateProcedureWithPrice(string name, int windows, int price,bool fixedPrice)
        {
            Procedure someProc = new(name,windows,price);
            return someProc;
        }

        public void UpdateProcedure(string name, int windows,int basePrice, int? price)
        {
            if(string.IsNullOrWhiteSpace(name) || windows <=0)
            {
                throw new Exception("error");
            }

            if (price.HasValue && price.Value < 0)
            {
                throw new Exception("price cannot be negative");
            }
            ProcedureName = name;
            TakeWindows = windows;
            Price = price ?? windows*basePrice;
        }

        public void RecalculatePrice(int basePrice)
        {
            Price = TakeWindows * basePrice;
        }

    }
    public interface IProcedureFactory
    {
        Task<Procedure> CreateProcedure(string name, int windows, int? price = null);
       
    }

    public class ProcedureFactory(IServiceSettingRepository _serviceRepo) : IProcedureFactory
    {
        public async Task<Procedure> CreateProcedure(string name, int windows, int? price = null)
        {
            bool @fixed= true;
            if (!price.HasValue)
            {
                ServiceSettings settings =await  _serviceRepo.GetSettings();
                price = windows * settings.BasePricePerWindow;
                @fixed = false;
            }
            return Procedure.CreateProcedureWithPrice(name, windows, price.Value,@fixed);
        }
    }


}
