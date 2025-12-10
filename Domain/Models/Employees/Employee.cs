
namespace Domain.Models.Employees
{
    public class Employee :MainEntity
    {
        public Guid Id { get; internal set;}
        private Employee() { }

        private Employee(double hourlybase, string name)
        {
            Id=Guid.NewGuid();
            HourlyBase=hourlybase;
            EmployeeName=name;
        }

        public double HourlyBase { get; internal set;}

        public string EmployeeName { get; internal set;}

        public static Employee CreateEmployee(double hourlybase,string name)
        {
            Employee employee = new Employee(hourlybase,name);
            return employee;
        }

        public Employee EditEmployee(double hourlybase, string name)
        {
            HourlyBase = hourlybase;
            EmployeeName = name;
            return this;
        }
    }
}
