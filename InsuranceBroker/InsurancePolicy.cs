namespace InsuranceBroker
{
    public class InsurancePolicy
    {
        public Entity InsuredEntity { get; set; }
        // Can be a Vehicle or a Person due to polymorphism
        public int DurationMonths
        { get; set; } //Duration in months
        public decimal MaxInsuredValue { get; set; } // Maximum insured value
        // Method to display policy details
        public void DisplayPolicyDetails()
        {
            Console.WriteLine($"Policy Details: Duration {DurationMonths} months, Max Value {MaxInsuredValue:C}");
            InsuredEntity.DisplayInfo();
        }
    }
}
