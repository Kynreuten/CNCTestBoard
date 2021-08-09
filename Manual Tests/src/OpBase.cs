using System;
using System.Text;

using GoldenLlama.Cnc;


namespace GoldenLlama.Cnc.Test
{
    public class OpBase
    {
        public double ClearanceHeight { get; set; } = 0.5;
        public double RetractHeight { get; set; } = 0.25;
        public double RapidRate { get; set; } = 12;
        public double MaterialThickness { get; set; } = 12;


        public double FeedRate { get; set; } = 4.87;

        public int SpindleSpeed { get; set; } = 1500;


        protected string ToRetractHeight()
        {
            return RunOp.RapidTo(new Vector { Z = RetractHeight })
                        .Extend(GCode.AbsoluteCommand);
        }
        protected string ToClearanceHeight()
        {
            return RunOp.RapidTo(new Vector { Z = ClearanceHeight })
                        .Extend(GCode.AbsoluteCommand);
        }

        /// <summary>
        /// Returns to our "safe" home location. This is X=0 Y=0 Z={RetractHeight}
        /// </summary>
        /// <returns>The string to do this by.</returns>
        protected string GoSafeHome()
        {
            var sb = new StringBuilder();
            // We want this to be done in Absolute terms!
            sb.AppendLine(ToClearanceHeight());

            sb.AppendLine(RunOp.RapidTo(new Vector { X = 0, Y = 0 }))
                                .Extend(GCode.AbsoluteCommand);
            sb.AppendLine(ToRetractHeight());
            return sb.ToString();
        }


        /// <summary>
        /// Returns to our home location. This is X=0 Y=0 Z=0. The final movement on Z will be done as a Feed operation for cutting in case there is material.
        /// </summary>
        /// <returns>The string to do this by.</returns>
        protected string GoHome()
        {
            return GoSafeHome()
                    .CutTo(new Vector { Z = 0 })
                    .Extend(GCode.AbsoluteCommand); ;
        }
    }
}