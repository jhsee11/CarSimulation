using Xunit.Abstractions;

namespace CarSimulation.Tests;

public class UnitTest1
{

    private readonly ITestOutputHelper _output;

    public UnitTest1(ITestOutputHelper output)
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

        var carA = new Car();
        carA.CarName = "A";

        newCarField.AddCar(carA);

        Assert.Equal(1, newCarField.CarList.Count);
        Assert.Equal("A", newCarField.CarList[0].CarName);
    }

    [Fact]
    public void SetCarPosition()
    {
        var carA = new Car();
        carA.CarName = "A";

        carA.CarPosition = (1, 3, Car.Direction.N);

        Assert.Equal((1, 3, Car.Direction.N), carA.CarPosition);
    }

    [Fact]
    public void MoveCarPosition_WhenOneCarFromNorthMoveForward_ReturnsCorrectPosition()
    {
        var carA = new Car();

        carA.CarPosition =  (1, 1, Car.Direction.N);
        carA.Commands = "FFF";

        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        newCarField.AddCar(carA);

        var carList = newCarField.RunSimulation();

       // _output.WriteLine(carA.CarPosition.ToString());
       //  _output.WriteLine("hello");

        Assert.Equal((1, 4, Car.Direction.N), carList[0].CarPosition);
    }

    [Fact]
    public void MoveCarPosition_WhenOneCarFromNorthMoveRightForward_ReturnsCorrectPosition()
    {
        var carA = new Car();

        carA.CarPosition = (1, 1, Car.Direction.N);
        carA.Commands = "RFFF";

        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        newCarField.AddCar(carA);

        var carList = newCarField.RunSimulation();

        // _output.WriteLine(carA.CarPosition.ToString());
        //  _output.WriteLine("hello");

        Assert.Equal((4, 1, Car.Direction.E), carList[0].CarPosition);
    }

    [Fact]
    public void MoveCarPosition_WhenOneCarFromNorthMoveLeftForward_ReturnsCorrectPosition()
    {
        var carA = new Car();

        carA.CarPosition = (5, 5, Car.Direction.N);
        carA.Commands = "LFFF";

        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        newCarField.AddCar(carA);

        var carList = newCarField.RunSimulation();

        // _output.WriteLine(carA.CarPosition.ToString());
        //  _output.WriteLine("hello");

        Assert.Equal((2, 5, Car.Direction.W), carList[0].CarPosition);
    }

    [Fact]
    public void MoveCarPosition_WhenOneCarFromNorthMoveExceedBoundary_ReturnsCorrectPosition()
    {
        var carA = new Car();

        carA.CarPosition = (5, 5, Car.Direction.N);
        carA.Commands = "FFFFF";

        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        newCarField.AddCar(carA);

        var carList = newCarField.RunSimulation();

        // _output.WriteLine(carA.CarPosition.ToString());
        //  _output.WriteLine("hello");

        Assert.Equal((5, 5, Car.Direction.N), carList[0].CarPosition);
    }

    [Fact]
    public void MoveCarPosition_WhenOneCarFromNorthMoveForwardLeftForward_ReturnsCorrectPosition()
    {
        var carA = new Car();

        carA.CarPosition = (5, 5, Car.Direction.N);
        carA.Commands = "FFFFFLF";

        var newCarField = new CarField();
        newCarField.FieldDimension = (5, 5);

        newCarField.AddCar(carA);

        var carList = newCarField.RunSimulation();

        // _output.WriteLine(carA.CarPosition.ToString());
        //  _output.WriteLine("hello");

        Assert.Equal((4, 5, Car.Direction.W), carList[0].CarPosition);
    }
}
