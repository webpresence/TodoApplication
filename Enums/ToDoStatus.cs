using Ardalis.SmartEnum;
namespace ToDoApplication.Enums
{
    public class ToDoStatus : SmartEnum<ToDoStatus, string>
    {
        public static readonly ToDoStatus Draft = new(nameof(Draft), "DRAFT");
        public static readonly ToDoStatus Active = new(nameof(Active), "ACTIVE");
        public static readonly ToDoStatus Done = new(nameof(Done), "DONE");

        private ToDoStatus(string name, string value) : base(name, value)
        {
        }
    }
}
