using SeniorActivitySupportSystem.Data.Enum;
using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.ViewModel
{
    public class CreateSportEventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public string AppUserId { get; set; }
        public IFormFile Image { get; set; }
        public EventCategory SportEventCategory { get; set; }
    }
}
