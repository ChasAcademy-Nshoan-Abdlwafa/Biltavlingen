﻿using System;
using System.Reflection;
using static Biltavlingen.Car;

namespace Biltavlingen
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the race! Press any key to start!");
            Console.ReadKey();
            Console.WriteLine("\nThe race has begun! Press any key to receive an update on the current status of the race!");

            Car Car1 = new Car
            {
                car_name = "Car #1",
                car_speed = 120,
                car_currentDistance = 0
            };

            Car Car2 = new Car
            {
                car_name = "Car #2",
                car_speed = 120,
                car_currentDistance = 0
            };

            var carRace1 = RaceStart(Car1);
            var carRace2 = RaceStart(Car2);
            var carRaceStatus = RaceStatus(new List<Car> { Car1, Car2 });

            var carRaces = new List<Task> { carRace1, carRace2, carRaceStatus };

            int placement = 0;

            while (carRaces.Count > 0) // while-loop that checks placements, once both cars has received placements the race is considered finished
            {
                Task finishedTask = await Task.WhenAny(carRaces);
                if (finishedTask == carRace1)
                {
                    placement += 1;
                    PrintPlacement(Car1, placement);
                }
                else if (finishedTask == carRace2)
                {
                    placement += 1;
                    PrintPlacement(Car2, placement);
                }
                else if (placement == 2)
                {
                    Console.WriteLine("\nThe race has finished!");
                }

                await finishedTask;
                carRaces.Remove(finishedTask);
            }
        }

        public async static Task<Car> RaceStart(Car car)
        {
            int timeCalc = 30;
            while (true)
            {
                await Wait();
                car.car_currentDistance += (((car.car_speed * 1000) * timeCalc / 3600m));
                Event(car);

                if (car.car_delay > 0)
                {
                    await Wait(car.car_delay);
                    car.car_delay = 0;
                }

                if (car.car_currentDistance >= 10000m)
                {
                    return car;
                }
            }
        }

        public async static Task RaceStatus(List<Car> cars) // checks the current status of the car
        {
            while (true)
            {
                DateTime start = DateTime.Now;

                bool gotKey = false;

                while ((DateTime.Now - start).TotalSeconds < 2)
                {
                    if (Console.KeyAvailable)
                    {
                        gotKey = true;
                        break;
                    }
                }

                if (gotKey)
                {
                    Console.WriteLine("\nStatus check: ");
                    Console.ReadKey();
                    cars.ForEach(car =>
                    {
                        string currentDistance = car.car_currentDistance.ToString("0");
                        Console.WriteLine($"{car.car_name} has reached {currentDistance} meters so far and is currently driving at the speed of {car.car_speed} km/h!");
                    });
                    gotKey = false;
                }

                await Task.Delay(10);

                var remainingDistance = cars.Select(car => car.car_currentDistance).Sum();

                if (remainingDistance > 20000m)
                {
                    return;
                }
            }
        }

        public async static Task Wait(int delay = 30)
        {
            await Task.Delay(TimeSpan.FromSeconds(delay));
        }

        public static void Event(Car car) // random events that affects the cars
        {
            Random random = new Random();
            int eventChance = random.Next(1, 50);

            if (eventChance == 1)
            {
                Console.WriteLine($"\n{car.car_name} has no gas left! Refueling will take 30 seconds.");
                car.car_delay += 30;
            }
            else if (eventChance <= 2)
            {
                Console.WriteLine($"\nOne of {car.car_name}'s tires has flattened! Replacing the tire will take 20 seconds.");
                car.car_delay += 20;
            }
            else if ( eventChance <= 5)
            {
                Console.WriteLine($"\nA bird has hit the windshield of {car.car_name}, how unfortunate! Cleaning the windshield will take 10 seconds.");
                car.car_delay += 10;
            }
            else if ( eventChance <= 10)
            {
                car.car_speed = car.car_speed - 1;
                Console.WriteLine($"\nThere is something wrong with {car.car_name}'s engine! Its speed has slowed down to {car.car_speed} km/h.");
            }
            else
            {
                car.car_delay = 0;
            }
        }

        public static void PrintPlacement(Car car, int placement) // prints out the placements that the cars finished with
        {
            if (placement == 1)
            {
                Console.WriteLine($"\n{car.car_name} has won the race with the placement of {placement}!");
            }
            else
            {
                Console.WriteLine($"\n{car.car_name} has finished the race with the placement of {placement}.");
            }
        }
    }
}
