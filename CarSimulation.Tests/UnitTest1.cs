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
        var newCarField = new CarField(5, 5);

        Assert.Equal((5, 5), newCarField.FieldDimension);
    }

    [Fact]
    public void AddCar_ReturnsCorrectCarList()
    {
        var newCarField = new CarField(5, 5);

        var carA = new Car("A");

        newCarField.AddCar(carA);

        Assert.Equal(1, newCarField.CarList.Count);
        Assert.Equal("A", newCarField.CarList[0].CarName);
    }

    [Fact]
    public void SetCarPosition()
    {
        var carA = new Car("A");

        carA.SetCarPosition(1, 3, Car.Direction.N);

        Assert.Equal((1, 3, Car.Direction.N), carA.CarPosition);
    }

    [Fact]
    public void MoveCarPosition_MoveForward_ReturnsCorrectPosition()
    {
        var carA = new Car("A");

        carA.SetCarPosition(1, 1, Car.Direction.N);

        carA.MoveCar("FFF");

        _output.WriteLine(carA.CarPosition.ToString());
        _output.WriteLine("hello");

        Assert.Equal((1, 4, Car.Direction.N), carA.CarPosition);
    }

    [Fact]
    public void MoveCarPosition_WhenNorthMoveRightForward_ReturnsCorrectPosition()
    {
        var carA = new Car("A");

        carA.SetCarPosition(1, 1, Car.Direction.N);

        carA.MoveCar("RFFF");

        _output.WriteLine(carA.CarPosition.ToString());
        _output.WriteLine("hello");

        Assert.Equal((4, 1, Car.Direction.E), carA.CarPosition);
    }

    [Fact]
    public void MoveCarPosition_WhenNorthMoveLeftForward_ReturnsCorrectPosition()
    {
        var carA = new Car("A");

        carA.SetCarPosition(5, 5, Car.Direction.N);

        carA.MoveCar("LFFF");

        _output.WriteLine(carA.CarPosition.ToString());
        _output.WriteLine("hello");

        Assert.Equal((2, 5, Car.Direction.W), carA.CarPosition);
    }
}
