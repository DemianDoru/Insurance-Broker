using Dapper.Contrib.Extensions;
namespace DataAccess
{
    [Table("InsurancePolicies")]
    public class InsurancePolicy
    {
        [Key]
        public int PolicyID { get; set; }
        public int? PersonID { get; set; } // Nullable int for foreign key to Persons table
        public int? VehicleID { get; set; } // Nullable int for foreign key to Vehicles table
        public int DurationMonths { get; set; }
        public decimal MaxInsuredValue { get; set; }
    }
}