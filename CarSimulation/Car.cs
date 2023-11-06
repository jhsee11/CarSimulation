using System;
namespace CarSimulation
{
	public class Car
	{
		public string CarName { get; set; }

		public enum Direction { N, S, W, E };

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

		public (int x, int y, Direction) CarPosition { get; set; }

		public char[] Commands { get; set; }

		public Car(string carName)
		{
			CarName = carName;
		}

        public void SetCarPosition(int x, int y, Direction direction)
		{
            CarPosition = (x, y, direction);
		}

		public void MoveCar( string commands )
		{
			var commandList = commands.ToCharArray();
			string currFacingDir;


            foreach ( var command in commandList)
			{
                switch (command)
				{
					case 'L':
						currFacingDir = Enum.GetName(typeof(Direction), CarPosition.Item3);
						this.SetCarPosition(this.CarPosition.x, this.CarPosition.y, (Direction)Enum.Parse( typeof(Direction), directionMapping[currFacingDir]["L"]));
						break;
                    case 'R':
                        currFacingDir = Enum.GetName(typeof(Direction), CarPosition.Item3);
                        this.SetCarPosition(this.CarPosition.x, this.CarPosition.y, (Direction)Enum.Parse(typeof(Direction), directionMapping[currFacingDir]["R"]));
                        break;
                    case 'F':
                        currFacingDir = Enum.GetName(typeof(Direction), CarPosition.Item3);

                        this.SetCarPosition( this.CarPosition.x + moveMapping[currFacingDir].Item1, this.CarPosition.y + moveMapping[currFacingDir].Item2, this.CarPosition.Item3);
                        break;

                    default:
						break;

				}
            }
		}
	}
}

