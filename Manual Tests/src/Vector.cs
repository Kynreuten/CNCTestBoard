using System;


namespace GoldenLlama.Cnc
{
    public class Vector
    {
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }

        public double? FeedRate { get; set; }


        public override string ToString()
        {
            return String.Concat(
                X.FormatCode(OtherCode.X_Axis),
                Y.FormatCode(OtherCode.Y_Axis),
                Z.FormatCode(OtherCode.Z_Axis),
                FeedRate.FormatCode(OtherCode.FeedRate)
            );
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}