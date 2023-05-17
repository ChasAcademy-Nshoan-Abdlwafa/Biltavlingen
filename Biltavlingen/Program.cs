using Car;
using System;
using System.Threading.Tasks;

namespace Biltavlingen
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            await StartRace();
        }

        public static async Task StartRace()
        {
            Console.WriteLine("Welcome! Press any key to start the race.");
            Console.ReadLine();

            Car Car1 = new Car
            {
                car_id = 1,
                car_name = "Car #1",
                car_speed = 100,
                car_distance = 0
            };

            Car Car2 = new Car
            {
                car_id = 2,
                car_name = "Car #2",
                car_speed = 100,
                car_distance = 0
            };

            var carRace1 = CarRace(Car1);
            var carRace2 = CarRace(Car2);
            var carRaceStatus = CarStatus(new List<Car> { Car1, Car2 });

            var carRaces = new List<Task> { carRace1, carRace2, carRaceStatus };
        }
    }
}
