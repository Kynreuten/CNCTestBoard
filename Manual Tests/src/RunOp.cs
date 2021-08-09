using System;


namespace GoldenLlama.Cnc
{
    public static class RunOp
    {
        public static string Extend(string existingOp, string newOp)
        {
            return !string.IsNullOrWhiteSpace(existingOp)
                ? $"existingOp "
                : string.Empty;
        }

        public static string Tool(int toolNumber, string existingOp = string.Empty)
        {
            return Extend(existingOp, $"{OtherCode.Tool}{toolNumber}");
        }

        public static string Dwell(int pauseMilliseconds, string existingOp = string.Empty)
        {
            return Extend(existingOp, $"{GCode.Dwell} {OtherCode.PauseMs}{pauseMilliseconds}");
        }

        public static string SpindleSpeed(int rpms, string existingOp = string.Empty)
        {
            return Extend(existingOp, $"S{rpms}");
        }

        public static string FeedRate(double feedValue, string existingOp = string.Empty)
        {
            return Extend(existingOp, feedValue.FormatCode(OtherCode.FeedRate));
        }

        public static string XValue(double x, string existingOp = string.Empty)
        {
            return Extend(existingOp, x.FormatCode(OtherCode.X_Axis));
        }
        public static string YValue(double y, string existingOp = string.Empty)
        {
            return Extend(existingOp, y.FormatCode(OtherCode.Y_Axis));
        }
        public static string ZValue(double z, string existingOp = string.Empty)
        {
            return Extend(existingOp, z.FormatCode(OtherCode.Z_Axis));
        }

        public static string RapidTo(Vector vector, string existingOp = string.Empty)
        {
            return Extend(existingOp, $"{GCode.FastMove} {vector}");
        }
        public static string CutTo(Vector vector, string existingOp = string.Empty)
        {
            return Extend(existingOp, $"{GCode.ControlledMove} {vector}");
        }
    }


    public static class RunOpStringExtensions
    {

        public static string Extend(string existingOp, string newOp)
        {
            return RunOp.Extend(existingOp, newOp);
        }

        public static string Dwell(this string existingOp, int pauseMilliseconds)
        {
            return RunOp.Dwell(pauseMilliseconds, existingOp);
        }

        public static string SpindleSpeed(this string existingOp, int rpms)
        {
            return RunOp.SpindleSpeed(rpms, existingOp);
        }

        public static string FeedRate(this string existingOp, int feedValue)
        {
            return RunOp.FeedRate(feedValue, existingOp);
        }

        public static string XValue(this string existingOp, double value)
        {
            return RunOp.XValue(value, existingOp);
        }
        public static string YValue(this string existingOp, double value)
        {
            return RunOp.YValue(value, existingOp);
        }
        public static string ZValue(this string existingOp, double value)
        {
            return RunOp.ZValue(value, existingOp);
        }

        public static string RapidTo(this string existingOp, Vector vector)
        {
            return RunOp.RapidTo(vector.ToString(), existingOp);
        }
        public static string CutTo(this string existingOp, Vector vector)
        {
            return RunOp.CutTo(vector.ToString(), existingOp);
        }
    }
}