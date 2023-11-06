using System;
namespace CarSimulation
{
	public class CarField
	{
		public (int width, int height) FieldDimension { get; set; }

		public List<Car> CarList { get; set; } = new List<Car>();

		public CarField( int width, int height)
		{
			FieldDimension = (width, height);
		}

		public void AddCar ( Car newCar)
		{
			CarList.Add(newCar);
        }
	}
}

