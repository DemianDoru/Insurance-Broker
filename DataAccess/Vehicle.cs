using Dapper.Contrib.Extensions;

namespace DataAccess
{
    [Table("Vehicles")]
    public class Vehicle
    {
        [Key]
        public int ID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
}
