namespace Trials
{
    public class Car : IInit, System.ICloneable
    {
        private string brand;
        private int year;
        public double price;

        public string Brand { 
            get {
                return brand;
            }
            set {
                if (value is not null)
                {
                    brand = value;
                }
            } 
        }

        public int Year
        {
            get
            {
                return year;
            }
            set
            {
                if (3000 > value && value > 1900)
                {
                    year = value;
                }
            }
        }
        public double Price { get
            {
                return price;
            }
            set
            {
                if (value > 0)
                {
                    price = value;
                }
            }
        }

        // Конструкторы
        public Car() {
            brand = "Игрушечная машинка";
            year = 2025;
            price = 85.9;
        }

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
            try
            {
                Brand = Console.ReadLine();
                Console.Write("Введите год выпуска: ");
                Year = int.Parse(Console.ReadLine());
                Console.Write("Введите цену: ");
                Price = double.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Ошибка ввода. Убедитесь, что вы вводите значения корректно!");
            }
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

        // Для IClonable
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