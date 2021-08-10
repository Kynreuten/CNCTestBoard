using System;
using System.Text;

using GoldenLlama.Cnc;


namespace GoldenLlama.Cnc.Test
{
    public class OpBase
    {
        /// <summary>
        /// Absolute height above Z0 that should be "safe" for never hitting anything. Move to this before starting things.
        /// Defaults to 0.5"
        /// </summary>
        public double ClearanceHeight { get; set; } = 0.25;
        /// <summary>
        /// Absolute height above Z0 to move to prior to doing a rapid move. Defaults to 0.25"
        /// </summary>
        public double RetractHeight { get; set; } = 0.15;
        /// <summary>
        /// How fast to move when doing a Rapid Move. Defaults to 12 IPM
        /// </summary>
        public double RapidRate { get; set; } = 12;

        public double FeedRate { get; set; } = 4.87;
        public int SpindleSpeed { get; set; } = 1500;
        /// <summary>
        /// How far down (Z) to step between passes. Defaults to 0.01968504" (0.5mm)
        /// </summary>
        public double StepDown { get; set; } = 0.01968504;

        public double CutterHeadDiameter { get; set; } = 0.125;
        public double MaterialThickness { get; set; } = 1;

        /// <summary>
        /// Estimate of where we are in absolute terms. May become off due to deflection.
        /// </summary>
        public Vector EstimatedLocation { get; set; } = new Vector { X = 0, Y = 0, Z = 0 };


        /// <summary>
        /// Returns to our Retract height Z={RetractHeight}. Do this before any rapid movement.
        /// Handles updating EstimatedLocation.
        /// </summary>
        /// <returns>The string to do this by.</returns>
        protected string ToRetractHeight()
        {
            var opString = RunOp.RapidTo(new Vector { Z = RetractHeight })
                        .Extend(GCode.AbsoluteCommand);
            EstimatedLocation.Z = RetractHeight;

            return opString;
        }

        /// <summary>
        /// Returns to our Clearance height Z={ClearanceHeight}. Do this before major operation start.
        /// Handles updating EstimatedLocation.
        /// </summary>
        /// <returns>The string to do this by.</returns>
        protected string ToClearanceHeight()
        {
            var opString = RunOp.RapidTo(new Vector { Z = ClearanceHeight })
                        .Extend(GCode.AbsoluteCommand);
            EstimatedLocation.Z = ClearanceHeight;

            return opString;
        }

        /// <summary>
        /// Returns to our "safe" home location. This is X=0 Y=0 Z={RetractHeight}
        /// Handles updating EstimatedLocation.
        /// </summary>
        /// <returns>The string to do this by.</returns>
        protected string GoSafeHome()
        {
            var sb = new StringBuilder();
            // We want this to be done in Absolute terms!
            sb.AppendLine(ToClearanceHeight());

            sb.AppendLine(RunOp.RapidTo(new Vector { X = 0, Y = 0 })
                                .Extend(GCode.AbsoluteCommand));
            EstimatedLocation.X = 0;
            EstimatedLocation.Z = 0;
            sb.AppendLine(ToRetractHeight());
            return sb.ToString();
        }


        /// <summary>
        /// Returns to our home location. This is X=0 Y=0 Z=0. The final movement on Z will be done as a Feed operation for cutting in case there is material.
        /// Handles updating EstimatedLocation.
        /// </summary>
        /// <returns>The string to do this by.</returns>
        protected string GoHome()
        {
            var sb = new StringBuilder(GoSafeHome());
            sb.Append(RunOp.CutTo(new Vector { Z = 0 })
                        .Extend(GCode.AbsoluteCommand));
            EstimatedLocation.Z = 0;
            return sb.ToString();
        }

        /// <summary>
        /// Formats the given <paramref param="comment"> as a comment
        /// </summary>
        /// <param name="comment">The comment text to format</param>
        /// <returns>String of the comment to be written as GCode.</returns>
        protected string WriteComment(string comment)
        {
            return $"({comment})";
        }

        /// <summary>
        /// Goes to our safe home, then sends the appropriate commands for ending a running operation.
        /// Handles updating EstimatedLocation.
        /// </summary>
        /// <returns>String for the operation.</returns>
        protected string EndOperation()
        {
            var opString = String.Join(Environment.NewLine,
                new string[] {
                    GoSafeHome(),
                    MCode.SpindleOff,
                    MCode.Done
                });

            return opString;
        }
    }
}