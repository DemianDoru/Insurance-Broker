namespace InsuranceBroker
{
    public class Vehicle : Entity
    {
        public string Make { get; set; }
        public string Model { get; set; }
        // Implementing the abstract method
        public override void DisplayInfo()
        {
            Console.WriteLine($"Vehicle ID: {ID}, Make: {Make}, Model: {Model}");
        }
    }
}
