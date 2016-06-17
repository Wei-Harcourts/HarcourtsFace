using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Harcourts.Face.WebsiteService.Lookups;

namespace Harcourts.Face.UnitTest.Services
{
    internal class MockDataFileProvider : IJsonFileProvider
    {
        public string GetDataFilePath(string dataFileName)
        {
            return @"C:\Work\HarcourtsFace\Website\App_Data\" + dataFileName;
        }
    }
}
