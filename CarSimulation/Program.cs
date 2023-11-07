using CarSimulation;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static CarSimulation.Car;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Welcome to Auto Driving Car Simulation");
        Console.WriteLine("Please enther the width and height of the simulation field in x y format");

        // Read input from the user
        string input = Console.ReadLine();

        var carField = new CarField();

        try
        {
            int width = int.Parse(input.Split(" ")[0]);
            int height = int.Parse(input.Split(" ")[1]);

            carField.FieldDimension = (width - 1, height - 1);

            Console.WriteLine($"You have created a field {width} * {height} ");

            Console.WriteLine("Please choose from the following options");
            Console.WriteLine("[1] Add a car to field");
            Console.WriteLine("[2] Run simulation");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Please enter the name of the car : ");
                    var carName = Console.ReadLine();

                    int carX;
                    int carY;
                    string carDir;

                    while (true)
                    {
                      Console.WriteLine("Please enter initial position of car A in x y Direction format ");

                      var carPosition = Console.ReadLine();

                      carX = int.Parse(carPosition.Split(" ")[0]);
                      carY = int.Parse(carPosition.Split(" ")[1]);
                      carDir = carPosition.Split(" ")[2];

                      if (carDir == "N" || carDir == "S" || carDir == "W" || carDir == "E ")
                      {
                            break;
                      }
                    }

                    Console.WriteLine("Please enter the commands for car A");

                    var commands = Console.ReadLine();

                    // initialize car
                    var newCar = new Car();
                    newCar.CarName = carName;
                    newCar.CarPosition = (carX, carY, (Direction)Enum.Parse(typeof(Direction), carDir));
                    newCar.Commands = commands;

                    carField.AddCar(newCar);

                    Console.WriteLine("Your current list of cars are : ");

                    foreach ( var car in carField.CarList)
                    {
                        Console.WriteLine($" - {car.CarName}, {(car.CarPosition.x, car.CarPosition.y)} { Enum.GetName(typeof(Direction), car.CarPosition.Item3)}, {car.Commands} ");
                    }



                    break;

                default:
                    Console.WriteLine("Please enter correct choice");
                    break;
            }




        }
        catch (Exception ex)
        {
            Console.WriteLine($" Error : {ex.Message} , Failed to parse input as integer, make sure you give correct input ");
        }
       
    }
}


