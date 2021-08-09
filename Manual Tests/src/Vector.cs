using System;
using System.Linq;


namespace GoldenLlama.Cnc
{
    public class Vector
    {
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }

        public double? FeedRate { get; set; }

        public Vector() { }
        public Vector(double? x, double? y, double? z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector(double? x, double? y, double? z, double? feedRate)
            : this(x, y, z)
        {
            FeedRate = feedRate;
        }
        public Vector(Vector source)
            : this(source.X, source.Y, source.Z, source.FeedRate)
        {
        }


        public override string ToString()
        {
            var ops = new string[] {
                X.FormatCode(OtherCode.X_Axis),
                Y.FormatCode(OtherCode.Y_Axis),
                Z.FormatCode(OtherCode.Z_Axis),
                FeedRate.FormatCode(OtherCode.FeedRate)
            };
            return String.Join(" ",
                ops.Where(o => !string.IsNullOrWhiteSpace(o))
            );
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}