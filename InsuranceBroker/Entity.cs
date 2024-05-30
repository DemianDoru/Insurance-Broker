namespace InsuranceBroker
{
    public abstract class Entity
    {
        public string ID { get; set; }
        public abstract void DisplayInfo();
        // Abstract method for polymorphism
    }
}
