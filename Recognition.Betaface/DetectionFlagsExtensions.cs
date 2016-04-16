using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harcourts.Face.Recognition.Betaface
{
    /// <summary>
    /// Extensions for <see cref="DetectionFlags"/>.
    /// </summary>
    internal static class DetectionFlagsExtensions
    {
        /// <summary>
        /// Gets detection flag parameter.
        /// </summary>
        public static string ToParameter(this DetectionFlags flags)
        {
            if (flags == DetectionFlags.Default)
            {
                return string.Empty;
            }

            var parameters = new List<string>();

            if (flags.HasFlag(DetectionFlags.BestFace))
            {
                parameters.Add("bestface");
            }
            if (flags.HasFlag(DetectionFlags.ProPoints))
            {
                parameters.Add("propoints");
            }
            if (flags.HasFlag(DetectionFlags.Classifiers))
            {
                parameters.Add("classifiers");
            }
            if (flags.HasFlag(DetectionFlags.Extended))
            {
                parameters.Add("extended");
            }

            return string.Join(",", parameters);
        }
    }
}
