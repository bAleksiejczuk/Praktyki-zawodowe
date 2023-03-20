namespace CarList
{
    public class Car
    {
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Power { get; set; }

        public Car(string color, string brand, string model, int power)
        {
            Color = color;
            Brand = brand;
            Model = model;
            Power = power;
        }

        public string ToString()
        {
            
            return $"Marka: {Brand} Model: {Model} Moc: {Power} Kolor: {Color}";
        }
     
    }
}
