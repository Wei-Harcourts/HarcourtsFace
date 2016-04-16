using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harcourts.Face.Recognition
{
    /// <summary>
    /// The configuration of face recognition engine.
    /// </summary>
    public static class RecognitionConfigurationRegistry
    {
        private static Type _wellKnownConfigurationType = typeof (RecognitionConfiguration);

        /// <summary>
        /// Sets the type of the well-known face recognition configuration.
        /// </summary>
        /// <typeparam name="TConfiguration">
        /// The type of the configuration. Type must be a class type with a public parameterless constructor
        /// and implement <see cref="IRecognitionConfiguration"/>.
        /// </typeparam>
        public static void SetWellKnownConfigurationType<TConfiguration>()
            where TConfiguration : class, IRecognitionConfiguration, new()
        {
            _wellKnownConfigurationType = typeof (TConfiguration);
        }

        /// <summary>
        /// Creates an instance of the well-known face recognition configuration.
        /// </summary>
        internal static IRecognitionConfiguration CreateWellKnownConfiguration()
        {
            return (IRecognitionConfiguration) Activator.CreateInstance(_wellKnownConfigurationType);
        }
    }
}
