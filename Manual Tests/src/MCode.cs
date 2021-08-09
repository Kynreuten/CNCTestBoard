using System;


namespace GoldenLlama.Cnc
{
    public static class MCode
    {
        public const string ProgramPause = "M00";
        public const string ProgramPauseOptional = "M01";
        public const string ProgramEnd = "M02";

        public const string SpindleOnClockwise = "M03";
        public const string SpindleOnCounterClockwise = "M04";
        public const string SpindleOff = "M05";

        public const string ToolChange = "M06";
        public const string MistCoolantOn = "M07";
        public const string FloodCoolantOn = "M08";
        public const string CoolantOff = "M09";
        public const string VacuumOn = "M10";
        public const string VacuumOff = "M11";

        public const string Done = "M30";
    }
}