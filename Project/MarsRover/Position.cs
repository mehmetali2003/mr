using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover
{
    public class Position
    {
        public long Xcoordinate { get; set; }
        public long Ycoordinate { get; set; }
        public char Orientation { get; set; }

        public Position()
        {

        }

        public Position(long xcoordinate, long ycoordinate, char orientation)
        {
            this.Xcoordinate = xcoordinate;
            this.Ycoordinate = ycoordinate;
            this.Orientation = orientation;
        }

        public string GetInfo()
        {
            return string.Format("{0} {1} {2}", this.Xcoordinate, this.Ycoordinate, this.Orientation);
        }

        public void SetValue(Position position)
        {
            this.Xcoordinate = position.Xcoordinate;
            this.Ycoordinate = position.Ycoordinate;
            this.Orientation = position.Orientation;
        }
    }
}
