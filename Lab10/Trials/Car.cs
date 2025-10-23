namespace Trials
{
    public class Car : IInit, System.ICloneable
    {
        public string Brand { get; set; }
        public int Year { get; set; }
        public double Price { get; set; }

        // Конструкторы
        public Car() { }
        public Car(string brand, int year, double price)
        {
            Brand = brand;
            Year = year;
            Price = price;
        }

        // Реализация IInit
        public void Init()
        {
            Console.Write("Введите марку автомобиля: ");
            Brand = Console.ReadLine();
            Console.Write("Введите год выпуска: ");
            Year = int.Parse(Console.ReadLine());
            Console.Write("Введите цену: ");
            Price = double.Parse(Console.ReadLine());
        }

        public void RandomInit()
        {
            string[] brands = { "Toyota", "BMW", "Audi", "Mercedes", "Honda" };
            Random rnd = new Random();
            Brand = brands[rnd.Next(brands.Length)];
            Year = rnd.Next(1990, 2024);
            Price = rnd.Next(500000, 5000000);
        }

        public void Show()
        {
            Console.WriteLine($"[Автомобиль]: {Brand}, {Year} год, цена: {Price} руб.");
        }

        // Реализация ICloneable
        public object Clone()
        {
            return new Car(Brand, Year, Price); // глубокое копирование
        }

        public Car ShallowCopy()
        {
            return (Car)MemberwiseClone(); // поверхностное копирование
        }
    }
}