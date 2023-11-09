using Xunit.Abstractions;
using static CarSimulation.Car;

namespace CarSimulation.Tests;

public class CarSimulationProgramTest
{

    private readonly ITestOutputHelper _output;

    public CarSimulationProgramTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void CreateCarField_ReturnsCorrectCarField()
    {
        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        Assert.Equal((5, 5), newCarField.FieldDimension);
    }

    [Fact]
    public void AddCar_ReturnsCorrectCarList()
    {
        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        var carA = new Car("A", (3, 4, Direction.E), "");

        newCarField.AddCar(carA);

        Assert.Equal(1, newCarField.CarList.Count);
        Assert.Equal("A", newCarField.CarList[0].CarName);
    }

    [Fact]
    public void AddCar_CarPositionExceedsField_FailedToAddCar()
    {
        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        var carA = new Car("A", (8, 9, Direction.E), "");

        newCarField.AddCar(carA);

        Assert.Equal(0, newCarField.CarList.Count);
    }

    [Fact]
    public void CreateCar_SetCarPosition_ReturnsCorrectCarPosition()
    {
        var carA = new Car("A", (1, 3, Direction.E), "");

        Assert.Equal((1, 3, Car.Direction.N), carA.CarPosition);
    }

    [Fact]
    public void CreateCar_SetWrongPosition_ThrowsException()
    {
        var carA = new Car("A", (3, 4, Direction.E), "");

        var exception = Assert.Throws<Exception>(() => carA.CarPosition = (1, -3, Car.Direction.S));

        Assert.Equal("Car Position must be greater or equal than 0", exception.Message);

    }

    [Fact]
    public void MoveCar_WhenOneCarWithFourCommands_ReturnsCorrectPosition()
    {
        var carA = new Car("A", (1, 1, Direction.N), "RFFF");

        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        newCarField.AddCar(carA);

        //Move Car will only execute 1 command
        newCarField.MoveCar(carA);

        Assert.Equal(3, carA.Commands.Length);
        Assert.Equal((1, 1, Car.Direction.E), carA.CarPosition);
    }

    [Fact]
    public void CheckExceedBoundary_ExceedBoundary_ReturnsCorrectResult()
    {

        Dictionary<string, Tuple<int, int>> moveMapping = new Dictionary<string, Tuple<int, int>>
        {
            {  "N", new Tuple<int,int>(0,1) },
            {  "S", new Tuple<int,int>(0,-1) },
            {  "W", new Tuple<int,int>(-1,0) },
            {  "E", new Tuple<int,int>(1,0) }
        };

        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        var isExceedBoundary =  newCarField.CheckExceedBoundary(5, 5, "E" );

        Assert.Equal(true, isExceedBoundary);
    }

    [Fact]
    public void CheckExceedBoundary_WithinBoundary_ReturnsCorrectResult()
    {

        Dictionary<string, Tuple<int, int>> moveMapping = new Dictionary<string, Tuple<int, int>>
        {
            {  "N", new Tuple<int,int>(0,1) },
            {  "S", new Tuple<int,int>(0,-1) },
            {  "W", new Tuple<int,int>(-1,0) },
            {  "E", new Tuple<int,int>(1,0) }
        };

        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        var isExceedBoundary = newCarField.CheckExceedBoundary(3, 3, "N");

        Assert.Equal(false, isExceedBoundary);
    }

    [Fact]
    public void RunSimulation_WhenOneCarFaceNorthMoveForward_ReturnsCorrectPosition()
    {
        var carA = new Car("A", (1, 1, Direction.N), "FFF");

        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        newCarField.AddCar(carA);

        var carList = newCarField.RunSimulation();

        // _output.WriteLine(carA.CarPosition.ToString());
        //  _output.WriteLine("hello");

        Assert.Equal((1, 4, Car.Direction.N), carList[0].CarPosition);
    }

    [Fact]
    public void RunSimulation_WhenOneCarFaceNorthMoveRightForward_ReturnsCorrectPosition()
    {
        var carA = new Car("A", (1, 1, Direction.N), "RFFF");

        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        newCarField.AddCar(carA);

        var carList = newCarField.RunSimulation();

        Assert.Equal((4, 1, Car.Direction.E), carList[0].CarPosition);
    }

    [Fact]
    public void RunSimulation_WhenOneCarFaceNorthMoveLeftForward_ReturnsCorrectPosition()
    {
        var carA = new Car("A", (5, 5, Direction.N), "LFFF");

        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        newCarField.AddCar(carA);

        var carList = newCarField.RunSimulation();

        Assert.Equal((2, 5, Car.Direction.W), carList[0].CarPosition);
    }

    [Fact]
    public void RunSimulation_WhenOneCarFaceNorthMoveExceedBoundary_ReturnsCorrectPosition()
    {
        var carA = new Car("A", (5, 5, Direction.N), "FFFFF");

        var newCarField = new CarField();
        newCarField.FieldDimension = (8, 8);
        newCarField.AddCar(carA);

        var carList = newCarField.RunSimulation();

        Assert.Equal((5, 8, Car.Direction.N), carList[0].CarPosition);
    }

    [Fact]
    public void RunSimulation_WhenOneCarFaceNorthMoveForwardLeftRight_ReturnsCorrectPosition()
    {
        var carA = new Car("A", (20, 20, Direction.N), "FFFFFLFR");

        var newCarField = new CarField();
        newCarField.FieldDimension = (100, 100);

        newCarField.AddCar(carA);

        var carList = newCarField.RunSimulation();


        Assert.Equal((19, 25, Car.Direction.N), carList[0].CarPosition);
    }

    [Fact]
    public void RunSimulation_WhenOneCarFaceNorthMoveLeftRight_ReturnsCorrectPosition()
    {
        var carA = new Car("A", (20, 20, Direction.N), "LRRR");

        var newCarField = new CarField();
        newCarField.FieldDimension = (100, 100);

        newCarField.AddCar(carA);

        var carList = newCarField.RunSimulation();

        Assert.Equal((20, 20, Car.Direction.S), carList[0].CarPosition);
    }


    [Fact]
    public void RunSimulation_WhenTwoCarNotCollides_ReturnsCorrectPosition()
    {
        var carA = new Car("A", (20, 20, Direction.N), "FFR");
        var carB = new Car("B", (30, 30, Direction.N), "LLF");

        var newCarField = new CarField();
        newCarField.FieldDimension = (100, 100);

        newCarField.AddCar(carA);

        newCarField.AddCar(carB);

        var carList = newCarField.RunSimulation();

        /*
        foreach (var car in carList)
        {
            if (car.IsCrahsed)
            {
                Console.WriteLine($" - {car.CarName}, collides with {car.CrashedWithCar} at {(car.CarPosition.x, car.CarPosition.y)} at step {car.CrashedStep}");
            }
            else
            {
                Console.WriteLine($" - {car.CarName}, {(car.CarPosition.x, car.CarPosition.y)} {Enum.GetName(typeof(Direction), car.CarPosition.Item3)}");
            }

        }*/

        Assert.Equal(false, carList[0].IsCrahsed);
        Assert.Equal(false, carList[1].IsCrahsed);

        Assert.Equal((20, 22, Car.Direction.E), carList[0].CarPosition);
        Assert.Equal((30, 29, Car.Direction.S), carList[1].CarPosition);
    }

    [Fact]
    public void RunSimulation_WhenTwoCarCollides_ReturnsCorrectPosition()
    {
        var carA = new Car("A", (20, 20, Direction.N), "FFR");
        var carB = new Car("B", (20, 24, Direction.S), "FFL");

        var newCarField = new CarField();
        newCarField.FieldDimension = (100, 100);

        newCarField.AddCar(carA);

        newCarField.AddCar(carB);

        var carList = newCarField.RunSimulation();


        Assert.Equal(true, carList[0].IsCrahsed);
        Assert.Equal(true, carList[1].IsCrahsed);

        Assert.Equal(2, carList[0].CrashedStep);
        Assert.Equal(2, carList[1].CrashedStep);

        Assert.Equal((20, 22, Car.Direction.N), carList[0].CarPosition);
        Assert.Equal((20, 22, Car.Direction.S), carList[1].CarPosition);
    }

    [Fact]
    public void RunSimulation_WhenThreeCarNotCollides_ReturnsCorrectPosition()
    {
        var carA = new Car("A", (20, 20, Direction.N), "FFR");
        var carB = new Car("B", (30, 30, Direction.N), "LLF");
        var carC = new Car("C", (1, 1, Direction.W), "FFF");

        var newCarField = new CarField();
        newCarField.FieldDimension = (100, 100);

        newCarField.AddCar(carA);
        newCarField.AddCar(carB);
        newCarField.AddCar(carC);

        var carList = newCarField.RunSimulation();

        Assert.Equal(false, carList[0].IsCrahsed);
        Assert.Equal(false, carList[1].IsCrahsed);
        Assert.Equal(false, carList[2].IsCrahsed);

        Assert.Equal((20, 22, Car.Direction.E), carList[0].CarPosition);
        Assert.Equal((30, 29, Car.Direction.S), carList[1].CarPosition);
        Assert.Equal((0, 1, Car.Direction.W), carList[2].CarPosition);
    }


    [Fact]
    public void RunSimulation_WhenThreeCarCollides_ReturnsCorrectPosition()
    {
        var carA = new Car("A", (20, 19, Direction.N), "FFR");
        var carB = new Car("B", (20, 21, Direction.S), "FFL");
        var carC = new Car("C", (21, 20, Direction.W), "FFF");

        var newCarField = new CarField();
        newCarField.FieldDimension = (100, 100);

        newCarField.AddCar(carA);
        newCarField.AddCar(carB);
        newCarField.AddCar(carC);

        var carList = newCarField.RunSimulation();

        Assert.Equal(true, carList[0].IsCrahsed);
        Assert.Equal(true, carList[1].IsCrahsed);
        Assert.Equal(true, carList[2].IsCrahsed);

        Assert.Equal(1, carList[0].CrashedStep);
        Assert.Equal(1, carList[1].CrashedStep);
        Assert.Equal(1, carList[2].CrashedStep);

        Assert.Equal((20, 20, Car.Direction.N), carList[0].CarPosition);
        Assert.Equal((20, 20, Car.Direction.S), carList[1].CarPosition);
        Assert.Equal((20, 20, Car.Direction.W), carList[2].CarPosition);
    }
}
