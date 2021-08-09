using System;


namespace GoldenLlama.Cnc
{
    public static class GCode
    {
        public const string FastMove = "G00";
        public const string ControlledMove = "G01";
        public const string ClockwiseArc = "G02";
        public const string CounterClockwiseArc = "G03";

        public const string Dwell = "G04";


        public const string PlaneXY = "G17";
        public const string PlaneXZ = "G18";
        public const string PlaneYZ = "G19";

        /// Use Inches per Minute
        public const string InchUnits = "G20";
        /// Use Millimeters per Minute
        public const string MillimeterUnits = "G21";


        public const string MachineZeroPosCheck = "G27";
        public const string MachineZeroReturnPoint1 = "G28";
        public const string ReturnFromMachineZero = "G29";
        public const string MachineZeroReturnPoint2 = "G30";

        public const string SkipFunction = "G31";

        public const string LocalCoordinateSystemSet = "G52";
        public const string MachineCoordinateSystemSet = "G52";

        public const string WorkCoordinate1 = "G54";
        public const string WorkCoordinate2 = "G55";
        public const string WorkCoordinate3 = "G56";
        public const string WorkCoordinate4 = "G57";
        public const string WorkCoordinate5 = "G58";
        public const string WorkCoordinate6 = "G59";

        public const string AbsoluteCommand = "G90";
        public const string RelativeCommand = "G91";

        public const string FeedRatePerMinB = "G94";
        public const string FeedRatePerRevB = "G94";
    }
}