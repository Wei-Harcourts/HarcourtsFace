using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;

namespace Harcourts.Face.Recognition.ProjectOxford.UnitTest
{
    public abstract class FaceServiceTests
    {
        private readonly string _subscriptionKey;

        protected FaceServiceTests()
        {
            _subscriptionKey = ConfigurationManager.AppSettings["ProjectOxford.Face.SubscriptionKey"];
        }

        protected FaceServiceClient CreateFaceServiceClient()
        {
            return new FaceServiceClient(_subscriptionKey);
        }

        protected async Task Run(Func<Task> action)
        {
            try
            {
                await action.Invoke();
            }
            catch (Exception ex)
            {
                ex.Dump();
                throw;
            }
        }
    }
}
