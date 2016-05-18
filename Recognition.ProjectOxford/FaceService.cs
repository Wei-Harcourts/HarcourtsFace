using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;

namespace Harcourts.Face.Recognition.ProjectOxford
{
    public class FaceService
    {
        private static readonly string SubscriptionKey;

        static FaceService()
        {
            SubscriptionKey = ConfigurationManager.AppSettings["ProjectOxford.Face.SubscriptionKey"];
        }
    }
}
