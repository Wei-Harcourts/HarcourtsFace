using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harcourts.Face.Recognition
{
    /// <summary>
    /// Provides face recognition features.
    /// </summary>
    public static class FaceRecognition
    {
        private static IRecognitionConfiguration _config;

        /// <summary>
        /// The recognition service provider.
        /// </summary>
        public static IFaceRecognitionServiceProvider Engine
        {
            get { return _config.ServiceProvider; }
        }

        /// <summary>
        /// Initializes the face recognition service engine.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void Initialize(Action<IRecognitionConfiguration> config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            var configuration = RecognitionConfigurationRegistry.CreateWellKnownConfiguration();
            config.Invoke(configuration);

            if (configuration.ServiceProvider == null)
            {
                throw new InvalidOperationException("Service provider is not provided.");
            }

            _config = configuration;
        }
    }
}
