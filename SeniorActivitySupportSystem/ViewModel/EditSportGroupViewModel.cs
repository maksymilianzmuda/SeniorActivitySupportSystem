using SeniorActivitySupportSystem.Data.Enum;
using SeniorActivitySupportSystem.Models;

namespace SeniorActivitySupportSystem.ViewModel
{
    public class EditSportGroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public string? URL { get; set; }
        public SportGroupCategory SportGroupCategory { get; set; }
    }
}
