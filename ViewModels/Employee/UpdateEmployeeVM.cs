using eBusiness.Models;

namespace eBusiness.ViewModels
{
    public class UpdateEmployeeVM
    {
        public string Name { get; set; }
        public string? Surname { get; set; }
        public double Salary { get; set; }
        public IFormFile? Image { get; set; }
        public int PositionId { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? ImgUrl { get; set; }
    }
}
