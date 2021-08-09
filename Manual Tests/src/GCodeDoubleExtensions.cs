using System;


namespace GoldenLlama.Cnc
{
    public static class GCodeDoubleExtensions {
        public static string FormatCode(this double value, string prefix = null) {
            string formatted = String.Format("{0:0.#####}", value);
            if (string.IsNullOrWhiteSpace(prefix))
                return formatted;
            else
                return $"{prefix}{formatted}";
        }
        
        public static string FormatCode(this double? value, string prefix = null) {
            if (value.HasValue) 
                return GCodeDoubleExtensions.FormatCode(value.Value, prefix);
            else 
                return string.Empty;
        }
    }
}