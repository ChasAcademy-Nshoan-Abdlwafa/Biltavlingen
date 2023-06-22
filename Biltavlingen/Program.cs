using System;
using System.Threading.Tasks;
using static Biltavlingen.Car;

namespace Biltavlingen
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the race! Press any key to start!");
            Console.ReadKey();
            Console.WriteLine("The race has begun! Press any key to receive an update on the current race status!");

            Car Car1 = new Car();
            {
                car_name = "Car #1",
                car_speed = 120,
                car_currentDistance = 0
            };

            Car Car2 = new Car();
            {
                car_name = "Car #2",
                car_speed = 120,
                car_currentDistance = 0
            };

            var carRace1 = RaceStart(Car1);
            var carRace2 = RaceStart(Car2);
            var carRaceStatus = RaceStatus(new List<Task> { Car1, Car2 });

            var carRaces = new List<Task> { new carRace1, carRace2, carRaceStatus };
        }
    }
}
