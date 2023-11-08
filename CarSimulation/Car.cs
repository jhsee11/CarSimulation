using System;
namespace CarSimulation
{
	public class Car
	{
		public string CarName { get; set; } = "";

        public string Commands { get; set; } = "";

		public bool IsCrahsed { get; set; } = false;

		public int CrashedStep { get; set; }

        public string CrashedWithCar { get; set; }

        public enum Direction { N, S, W, E };

		private (int x, int y, Direction) _carPosition;

		public (int x, int y, Direction) CarPosition
		{
			get { return _carPosition; }
			set
			{
				if (value.x >= 0 && value.y >= 0)
				{
					_carPosition = value;
				}
				else
				{
					throw new Exception("Car Position must be greater or equal than 0");
                }
			}
		}
	}
}

