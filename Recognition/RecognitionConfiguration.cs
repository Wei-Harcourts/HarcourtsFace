﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Harcourts.Face.Recognition
{
    /// <summary>
    /// The configuration of face recognition engine.
    /// </summary>
    public class RecognitionConfiguration : IRecognitionConfiguration
    {
        /// <summary>
        /// The face recognition service provider.
        /// </summary>
        public IFaceRecognitionServiceProvider ServiceProvider { get; set; }
    }
}
