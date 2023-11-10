using System.Text.RegularExpressions;
using CarSimulation;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static CarSimulation.Car;

public class Program
{

    public static void Main()
    {
        var startOverflag = true;
        CarField carField = new CarField();
        var commands = string.Empty;
        int width = 0;
        int height = 0;

        try
        {
            while (true)
            {
                if (startOverflag)
                {
                    while (true)
                    {
                        Console.WriteLine("Welcome to Auto Driving Car Simulation");
                        Console.WriteLine("Please enter the width and height of the simulation field in x y format");
                        string input = Console.ReadLine();

                        if (input.Split(" ").Length < 2)
                        {
                            Console.WriteLine("Number of input is incorrect. Please re-enter");
                            continue;
                        }

                        var input1 = input.Split(" ")[0];
                        var input2 = input.Split(" ")[1];

                        if ( (int.TryParse(input1, out width)) && (int.TryParse(input2, out height)) )
                        {
                            if (width <= 1 || height <= 1)
                            {
                                Console.WriteLine("Invalid input. Please re-enter dimension to be greater than 1");
                                continue;
                            }

                            carField.FieldDimension = (width - 1, height - 1);
                            Console.WriteLine($"You have created a field of {width} X {height} ");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input type. Please re-enter the dimension as number");
                        }
                    }

                }

                // Read input from the user
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
                            Console.WriteLine($"Please enter initial position of car {carName} in x y Direction format ");

                            var carPosition = Console.ReadLine();

                            // make sure car position is within the created car field
                            if (carPosition.Split(" ").Length < 3)
                            {
                                Console.WriteLine($"Re-enter as the length of parameters is wrong");
                                continue;
                            }

                            carX = int.Parse(carPosition.Split(" ")[0]);
                            carY = int.Parse(carPosition.Split(" ")[1]);
                            carDir = carPosition.Split(" ")[2];

                            // make sure car position is within the created car field
                            if (! ((carX >= 0) && (carX <= width - 1) && (carY >= 0) && (carY <= height - 1) ))
                            {
                                Console.WriteLine($"Re-enter car position as it is not within Car Field dimension {width} X {height} ");
                                continue;
                            }

                            // make sure input car direction is valid
                            if (carDir == "N" || carDir == "S" || carDir == "W" || carDir == "E" )
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Re-enter car position as car direction is not one of 'E' 'N' 'S' 'W' ");
                            }
                        }

                        Console.WriteLine($"Please enter the commands for car {carName}");

                        while (true)
                        {
                            commands = Console.ReadLine();

                            // Define a regular expression pattern that matches only 'F', 'R', and 'L' characters
                            string pattern = "^[FRL]+$";

                            if (Regex.IsMatch(commands, pattern))
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine($"Command is wrong.Please re-enter command using 'F', 'R', and 'L' chraracter only ");
                            }
                        }

                        // initialize car
                        var newCar = new Car(carName, (carX, carY, (Direction)Enum.Parse(typeof(Direction), carDir)), commands);

                        //newCar.CarName = carName;
                        //newCar.CarPosition = (carX, carY, (Direction)Enum.Parse(typeof(Direction), carDir));
                        //newCar.Commands = commands;

                        carField.AddCar(newCar);

                        PrintCarListInField(carField);

                        Console.WriteLine();

                        startOverflag = false;
                        break;

                    case "2":
                        // check if have car
                        if (carField.CarList.Count() == 0 )
                        {
                            Console.WriteLine("No cars in car field for simulation. Please add in some cars.");
                            startOverflag = false;
                            break;
                        }

                        PrintCarListInField(carField);
                        Console.WriteLine("Running Simulation :");

                        var returnedCarList = carField.RunSimulation();

                        Console.WriteLine("After simulation, the result is :");

                        foreach (var car in returnedCarList)
                        {
                            if (car.IsCrahsed)
                            {
                                Console.WriteLine($" - {car.CarName}, collides with {car.CrashedWithCar} at {(car.CarPosition.x, car.CarPosition.y)} at step {car.CrashedStep}");
                            }
                            else
                            {
                                Console.WriteLine($" - {car.CarName}, {(car.CarPosition.x, car.CarPosition.y)} {Enum.GetName(typeof(Direction), car.CarPosition.Item3)}");
                            }
                            
                        }

                        while (true)
                        {
                            Console.WriteLine("Please choose from the following options");
                            Console.WriteLine("[1] Start over");
                            Console.WriteLine("[2] Exit");

                            var result = Console.ReadLine();

                            if (result == "1")
                            {
                                startOverflag = true;
                                // reset car field
                                carField = new CarField();
                                break;
                            }
                            else if
                             (result == "2")
                            {
                                Console.WriteLine("Thank you for running the simulation. Goodbye! ");
                                Environment.Exit(0);
                            }
                            else
                            {
                                Console.WriteLine("Invalid input. Please enter 1 or 2.");
                            }
                        }

                        break;

                    default:
                        Console.WriteLine("Please enter 1 or 2");
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Program Exit due to this Error : {ex.Message}");
        }
    }

    public static void PrintCarListInField( CarField field ) 
    {
        Console.WriteLine("Your current list of cars are : ");

        foreach (var car in field.CarList)
        {
            Console.WriteLine($" - {car.CarName}, {(car.CarPosition.x, car.CarPosition.y)} {Enum.GetName(typeof(Direction), car.CarPosition.Item3)}, {car.Commands} ");
        }
    }
}


