using System;
using System.Text;

using GoldenLlama.Cnc;


namespace GoldenLlama.Cnc.Test
{
    //TODO: Should the axis we're testing on be configurable?

    public class BackAndForthRateTester : OpBase
    {
        /// <summary>
        /// Original EstimatedLocation when the test operation is begun. This is used to calculate if we're getting too far off from our starting point.
        /// </summary>
        public Vector StartLocation { get; set; } = new Vector(0, 0, 0);
        public double FeedRateInitial { get; set; } = 4.87;
        public double FeedRateIncrement { get; set; } = 4.87 * 0.25;

        public int PauseTimeInitial { get; set; } = 500;
        public int PauseTimeIncrement { get; set; } = 500;
        public int PauseTimeMax { get; set; } = 5000;

        /// <summary>
        /// How far down to move on the primary axis per round. Defaults to 2"
        /// </summary>
        public double TestDistance { get; set; } = 2;
        /// <summary>
        /// Maximum distance to allow moving on our non-test Axis (Y).
        /// </summary>
        public double MaxLateralDistance { get; set; } = 2;

        public int MaxIterations { get; set; } = 20;


        public string GenerateTest()
        {
            StartLocation = new Vector(EstimatedLocation);

            StringBuilder sb = new StringBuilder();

            //TODO: Write out the comment block?

            // Apply our settings
            sb.AppendLine(WriteComment("Settings"));
            sb.AppendLine(RunOp.Tool(1));
            sb.AppendLine(GCode.PlaneXY);
            sb.AppendLine(GCode.InchUnits);
            sb.AppendLine(RunOp.SpindleSpeed(SpindleSpeed)
                                .Extend(MCode.SpindleOnClockwise));
            sb.AppendLine(RunOp.FeedRate(FeedRateInitial));
            sb.AppendLine();

            // Go Home
            sb.AppendLine(WriteComment("Going Home"));
            sb.AppendLine(GoHome());
            sb.AppendLine();
            sb.AppendLine(WriteComment("Beginning Test"));

            bool isPositive = true;
            double totalDepthEst = 0;
            int pauseTime = PauseTimeInitial;
            // Main loop
            for (int i = 0; i < MaxIterations; i++)
            {
                sb.AppendLine(RunOp.CutTo(new Vector { Z = -1 * StepDown })
                                    .Extend(GCode.RelativeCommand));
                EstimatedLocation.Z -= StepDown;
                if (i % 5 == 0) {
                    sb.AppendLine(WriteComment($"Current Location (est): {this.EstimatedLocation}"));
                }

                // Move proper distance on chosen axis
                var cutDistance = TestDistance * (isPositive ? 1 : -1);
                sb.AppendLine(RunOp.CutTo(new Vector { X = cutDistance })
                                    .Extend(GCode.RelativeCommand));
                EstimatedLocation.X += cutDistance;
                // Flip our sign
                isPositive = !isPositive;

                // Pause!
                sb.AppendLine(RunOp.Dwell(pauseTime));
                if (pauseTime < PauseTimeMax) 
                    pauseTime += PauseTimeIncrement;

                // Make certain not to cut through our stock
                if (totalDepthEst > (MaterialThickness + StepDown))
                {
                    sb.AppendLine(MoveOver());
                }
                // Check our lateral offset
                if (EstimatedLocation.Y > StartLocation.Y + MaxLateralDistance)
                {
                    sb.AppendLine(WriteComment($"Aborting loop due to exceeding max lateral motion (Y-Axis position over {MaxLateralDistance}"));
                    break;
                }
            }
            
            sb.AppendLine(WriteComment($"Operation Complete. Reseting to start."));
            sb.AppendLine(WriteComment($"Final Location (est): {this.EstimatedLocation}"));
            sb.AppendLine(EndOperation());
            sb.AppendLine(WriteComment("END"));

            return sb.ToString();
        }

        protected string MoveOver()
        {
            var opString = String.Join(Environment.NewLine,
                new string[] {
                    WriteComment("Reseting Z=0 and moving over by Tool's Diameter to avoid stock bottom penetration"),
            RunOp.CutTo(new Vector { Z = RetractHeight }),
                    RunOp.CutTo(new Vector { Y = CutterHeadDiameter }),
                    RunOp.CutTo(new Vector { Z = 0 })
            });
            EstimatedLocation.Z = 0;
            EstimatedLocation.Y += CutterHeadDiameter;

            return opString;
        }

    }
}