using System;
using System.Linq;
using static CarSimulation.Car;

namespace CarSimulation
{
	public class CarField
	{
		public (int width, int height) FieldDimension { get; set; }

		public List<Car> CarList { get; set; } = new List<Car>();

		public void AddCar ( Car newCar)
		{
            if ( (newCar.CarPosition.x <= FieldDimension.width - 1) && (newCar.CarPosition.y <= FieldDimension.height - 1) )
            {
                CarList.Add(newCar);
            }
            else
            {
                Console.WriteLine($"Failed to add car to field as it exceeds car field dimension {FieldDimension.width} X {FieldDimension.height}");
            }
        }

        public Dictionary<string, Dictionary<string, string>> directionMapping = new Dictionary<string, Dictionary<string, string>>
        {
            {
                "N", new Dictionary<string, string>
                {
                    { "L", "W" },
                    { "R", "E" }
                }
            },
            {
                "S", new Dictionary<string, string>
                {
                    { "L", "E" },
                    { "R", "W" }
                }
            },
            {
                "W", new Dictionary<string, string>
                {
                    { "L", "S" },
                    { "R", "N" }
                }
            },
            {
                "E", new Dictionary<string, string>
                {
                    { "L", "N" },
                    { "R", "S" }
                }
            }
        };

        public Dictionary<string, Tuple<int, int>> moveMapping = new Dictionary<string, Tuple<int, int>>
        {
            {  "N", new Tuple<int,int>(0,1) },
            {  "S", new Tuple<int,int>(0,-1) },
            {  "W", new Tuple<int,int>(-1,0) },
            {  "E", new Tuple<int,int>(1,0) }
        };

        public List<Car> RunSimulation()
		{
            bool successFlag = true;

            // select the longest command out of all the car commands
            var longestCommand = CarList.OrderByDescending(car => car.Commands.Length)
                        .Select(car => car.Commands)
                        .FirstOrDefault();

            //Console.WriteLine(longestCommand);

            int steps = 0;

            // based on longest command to perform logic
            for (int i = 0; i < longestCommand.Length; i++)
            {
                //List<(string, int, int)> CarCompareList = new List<(string, int, int)>();
                // initliaze car list for comparision
                List<Car> CarCompareList = new List<Car> { };

                foreach (var car in CarList)
                {
                    MoveCar(car);
                    CarCompareList.Add(car);
                }

                steps += 1;
                // after get all the Car New Position, compare and see whether do they collide with each other
                var groupedCars = CarCompareList.GroupBy(car => new { car.CarPosition.x, car.CarPosition.y })
                                     .Where(group => group.Count() > 1)
                                     .ToList();

                if (groupedCars.Count() > 0)
                {
                    //Console.WriteLine("There are cars that will collide");

                    foreach (var group in groupedCars)
                    {
                        //Console.WriteLine($"X: {group.Key.x}, Y: {group.Key.y}, Count : {group.Count()}");

                        foreach (var car in group)
                        {
                            // Console.WriteLine($"Name: {car.CarName}");
                            // set car to be crashed
                            car.IsCrahsed = true;
                            car.CrashedStep = steps;
                            string crashedWithCarName = string.Join(",", CarCompareList.FindAll(target => target.CarName != car.CarName).Select(x => x.CarName));
                        }
                    }
                    successFlag = false;
                    break;
                }
            }

            return CarList;
        }

        public void MoveCar(Car car)
        {
            // need to check the direction and how it move
            string currFacingDir;

            if (!string.IsNullOrEmpty(car.Commands))
            {
                // Get the first character
                char singleCmd = car.Commands[0];
                // Console.WriteLine($"Cmd for car {car.CarName} : {singleCmd} ");

                // Remove the first character from the original string
                car.Commands = car.Commands.Substring(1);
                // Console.WriteLine($"Remaining cmd for car {car.CarName} : {car.Commands} ");

                switch (singleCmd)
                {
                    case 'L':
                        currFacingDir = Enum.GetName(typeof(Direction), car.CarPosition.Item3);
                        car.CarPosition = (car.CarPosition.x, car.CarPosition.y, (Direction)Enum.Parse(typeof(Direction), directionMapping[currFacingDir]["L"]));
                        break;
                    case 'R':
                        currFacingDir = Enum.GetName(typeof(Direction), car.CarPosition.Item3);
                        car.CarPosition = (car.CarPosition.x, car.CarPosition.y, (Direction)Enum.Parse(typeof(Direction), directionMapping[currFacingDir]["R"]));
                        break;

                    case 'F':
                        currFacingDir = Enum.GetName(typeof(Direction), car.CarPosition.Item3);

                        // have to check with this direction, will it exceed the boundary of the car field
                        var isExceedBoundary = CheckExceedBoundary(car.CarPosition.x, car.CarPosition.y, currFacingDir);

                        if (! isExceedBoundary)
                        {
                            car.CarPosition = (car.CarPosition.x + moveMapping[currFacingDir].Item1, car.CarPosition.y + moveMapping[currFacingDir].Item2, car.CarPosition.Item3);
                        }

                        break;

                    default:
                        Console.WriteLine($"Not understand cmd - {singleCmd} as it is not in 'L' 'R' 'F'");
                        break;

                }
            }
        }

        public bool CheckExceedBoundary(int x, int y, string direction)
        {
            // calculate the new position of the car
            var newVal = (x + moveMapping[direction].Item1 , y + moveMapping[direction].Item2 );

            if (newVal.Item1 > FieldDimension.width || newVal.Item1 < 0 || newVal.Item2 > FieldDimension.height || newVal.Item2 < 0)
            {
                return true;
            }

            return false;
        }
    }
}

