using eBusiness.Models.Base;

namespace eBusiness.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; }
        public string? Surname{ get; set; }
        public double Salary { get; set; }
        public string ImageUrl{ get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public string? TwitterUrl { get; set; }
        public string? InstagramUrl { get; set; }
        public string? FacebookUrl { get; set; }
    }
}
