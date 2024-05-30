namespace InsuranceBroker
{
    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Implementing the abstract method
        public override void DisplayInfo()
        {
            Console.WriteLine($"Person ID: {ID}, Name: {FirstName} {LastName}");
        }
    }
}
