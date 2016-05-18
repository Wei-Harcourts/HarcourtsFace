using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Harcourts.Face.Recognition.ProjectOxford.UnitTest
{
    public static class ObjectDumper
    {
        public static void Dump(this object obj)
        {
            Console.WriteLine(JsonConvert.SerializeObject(obj));
        }
    }
}
