using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobot
{
    public class Robot
    {
        //Member variable declaration region
        #region Declaration
        private short _x = 0;
        private short _y = 0;
        private string _direction = string.Empty;
        private enum Direction
        {
            north,
            east,
            west,
            south
        }
        #endregion

        //Business Logic region
        #region Internal Logical Operations
        /// <summary>
        /// Method to position Robo on 5X5 grid at particular direction
        /// </summary>
        /// <param name="xaxis"></param>
        /// <param name="yaxis"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool Place(short xaxis, short yaxis, string direction)
        {
            if (!ValidateInput(xaxis, yaxis, direction))
                return false;
            //Valid placement - set values
            _x = xaxis; _y = yaxis; _direction = direction;
            return true;
        }

        /// <summary>
        /// Method to move robo by one step in the heading direction
        /// </summary>
        /// <returns></returns>
        private bool Move()
        {
            if (_direction == Direction.north.ToString())
            {
                //Check boundary limit
                if (_y + 1 > 5)
                    return false;
                else _y++;
            }
            if (_direction == Direction.east.ToString())
            {
                //Check boundary limit
                if (_x + 1 > 5)
                    return false;
                else _x++;
            }
            if (_direction == Direction.west.ToString())
            {
                //Check boundary limit
                if (_x - 1 < 0)
                    return false;
                else _x--;
            }
            if (_direction == Direction.south.ToString())
            {
                //Check boundary limit
                if (_y - 1 < 0)
                    return false;
                else _y--;
            }
            return true;
        }

        /// <summary>
        /// Method to change the direction of Robo to turn anti clockwise - 90 degrees
        /// </summary>
        private void Left()
        {
            if (_direction == Direction.north.ToString())
                _direction = Direction.west.ToString();
            else if (_direction == Direction.east.ToString())
                _direction = Direction.north.ToString();
            else if (_direction == Direction.west.ToString())
                _direction = Direction.south.ToString();
            else if (_direction == Direction.south.ToString())
                _direction = Direction.east.ToString();
        }

        /// <summary>
        /// Method to change the direction of Robo to turn clockwise - 90 degrees
        /// </summary>
        private void Right()
        {
            if (_direction == Direction.north.ToString())
                _direction = Direction.east.ToString();
            else if (_direction == Direction.east.ToString())
                _direction = Direction.south.ToString();
            else if (_direction == Direction.west.ToString())
                _direction = Direction.north.ToString();
            else if (_direction == Direction.south.ToString())
                _direction = Direction.west.ToString();
        }

        /// <summary>
        /// Method to execute report command (Display the Robo position and its direction)
        /// </summary>
        private void Report()
        {
            Console.WriteLine("Output : " + _x + ", " + _y + ", " + _direction);
        }

        /// <summary>
        /// Common method created for command processing.
        /// Can be used for file input or manual inputs
        /// </summary>
        /// <param name="inputCommand"></param>
        private void ProcessCommand(string inputCommand)
        {
            //Check Command
            if (inputCommand.ToLower().Trim() == "move")
                Move();
            else if (inputCommand.ToLower().Trim() == "left")
                Left();
            else if (inputCommand.ToLower().Trim() == "right")
                Right();
            else if (inputCommand.ToLower() == "report")
                Report();
            else if (inputCommand.Length > 8 && inputCommand.ToLower().Substring(0, 5).Trim() == "place")
            {
                var inputarr = inputCommand.Substring(5).Split(',');
                if (inputarr.Length == 3)
                {
                    Place(Convert.ToInt16(inputarr[0].Trim()), Convert.ToInt16(inputarr[1].Trim()), inputarr[2].Trim());
                }
            }
        }

        /// <summary>
        /// Method to validate the input command parameters
        /// </summary>
        /// <param name="xaxis"></param>
        /// <param name="yaxis"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool ValidateInput(short xaxis, short yaxis, string direction)
        {
            //Check the max values
            if (xaxis < 0 || xaxis > 5 || yaxis < 0 || yaxis > 5)
                return false;

            // Check permissible values for direction
            if (!String.IsNullOrEmpty(direction) && (direction.ToLower() == Direction.north.ToString() ||
                direction.ToLower() == Direction.east.ToString() ||
                direction.ToLower() == Direction.west.ToString() ||
                direction.ToLower() == Direction.south.ToString()))
                return true;
            else return false;
        }
        #endregion

        //Public methods to be called
        #region External methods exposed
        /// <summary>
        /// Method exposed for manual input
        /// </summary>
        public void ProcessManualInput()
        {
            Console.WriteLine("Thanks for choosing manual input.");
            Console.WriteLine("Please enter command and press enter, to end the input enter END and press enter.");

            //Take input from user
            string inputChar = Console.ReadLine();

            //Loop till user will not enter the termination command
            while (!String.IsNullOrEmpty(inputChar) && inputChar.ToUpper() != "END")
            {
                //Process the command entered by user
                ProcessCommand(inputChar);
                inputChar = Console.ReadLine();
            }
        }

        /// <summary>
        /// Method exposed for file based input
        /// </summary>
        public void ProcessFileInput()
        {
            Console.WriteLine("Thanks for choosing file input.");
            Console.WriteLine("Please enter file path and press enter.");

            //Take file path from user
            string inputChar = Console.ReadLine();

            //Check if file exists or not
            if (File.Exists(inputChar))
            {
                //Read file content
                var lines = File.ReadLines(inputChar);
                if (lines != null && lines.Count() > 0)
                {
                    //Process each command from file
                    foreach (var item in lines)
                    {
                        ProcessCommand(item);
                    }
                }
            }
            else Console.WriteLine("File not found");
        } 
        #endregion
    }
}
