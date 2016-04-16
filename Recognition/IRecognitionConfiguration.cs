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
    public interface IRecognitionConfiguration
    {
        /// <summary>
        /// The face recognition service provider.
        /// </summary>
        IFaceRecognitionServiceProvider ServiceProvider { get; set; }
    }
}
