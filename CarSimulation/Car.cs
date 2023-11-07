using System;
namespace CarSimulation
{
	public class Car
	{
		public string CarName { get; set; } = "";

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
					//do nth here
				}
			}
		}

		public string Commands { get; set; } = "";

		public Car()
		{
		}

		/*
        public void SetCarPosition(int x, int y, Direction direction)
		{
            CarPosition = (x, y, direction);
		}*/

		/*
		public void MoveCarBackUp( string commands )
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
		*/
	}
}

