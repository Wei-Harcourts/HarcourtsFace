using System;

namespace Harcourts.Face.Recognition.Betaface
{
    /// <summary>
    /// Default value for this parameter is an empty string – .
    /// You can use different binary flags to enable/disable different image processing features to make sure
    /// your images are processed as fast as possible and the time is not spent generating information you dont
    /// need. Some of the flags are always enabled, even if not supplied, others may trigger other flags On state
    /// (for example enabling extended measurements will enable Pro facial points detection). You can combine multiple
    /// flags in a single string using any delimiter or just by concatenating all flags in one string.
    /// </summary>
    [Flags]
    public enum DetectionFlags
    {
        /// <summary>
        /// Default processing, equivalent to enabling "propoints,classifiers" flags.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Return only one detected face per image with highest detection score.
        /// </summary>
        BestFace = 1,

        /// <summary>
        /// Enable 86 Pro facial points detection.
        /// </summary>
        ProPoints = 2,

        /// <summary>
        /// Enable classifiers (gender, ethicity, age, etc.).
        /// </summary>
        Classifiers = 4,

        /// <summary>
        /// Extended set of measurements, including colors retured in tags collection.
        /// See GetImageInfo documentation. Warning - processing time will increase significantly.
        /// </summary>
        Extended = 8
    }
}
