using System;
using System.Text;

using GoldenLlama.Cnc;


namespace GoldenLlama.Cnc.Test
{

    public class BackAndForthRateTester : OpBase
    {
        public double FeedRateInitial { get; set; } = 4.87;
        public double FeedRateIncrement { get; set; } = this.FeedRateInitial * 0.25;

        public int PauseTimeInitial { get; set; } = 500;
        public int PauseTimeIncrement { get; set; } = 500;

        /// <summary>
        /// How far down to move on the primary axis per round. Defaults to 2"
        /// </summary>
        public double TestDistance { get; set; } = 2;

        public int MaxIterations { get; set; } = 20;


        public string Test()
        {
            StringBuilder sb = new StringBuilder();

            //TODO: Write out the comment block?

            // Apply our settings
            sb.AppendLine(RunOp.Tool(1));
            sb.AppendLine(GCode.PlaneXY);
            sb.AppendLine(GCode.InchUnits);
            sb.AppendLine(RunOp.SpindleSpeed(SpindleSpeed)
                                .Extend(MCode.SpindleOnClockwise)
                                .FeedRate(FeedRateInitial));

            // Go Home
            sb.AppendLine(GoHome());

            bool isPositive = true;
            for (int i = 0; i < MaxIterations; i++)
            {
                sb.AppendLine(RunOp.CutTo(new Vector { Z = StepDown }));
                // Move proper distance on chosen axis
                sb.AppendLine(RunOp.CutTo(new Vector { X = TestDistance * (isPositive ? 1 : -1) }));
                // Flip our sign
                isPositive = !isPositive;
            }

            //TODO: Account for material thickness, move over before penetration

            return sb.ToString();
        }

    }
}