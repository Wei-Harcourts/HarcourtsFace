using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace Harcourts.Face.Recognition.Animetrics
{
    public class AnimetricsEngine
    {
        public void Detect()
        {
            var client = new RestClient("https://animetrics.p.mashape.com");
            var request = new RestRequest("detect", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Mashape-Key", "MXqssAFMaamshhJjEf3eL0gpNFE3p194wmHjsnvdbtX91GsnZB");

            request.AddQueryParameter("api_key", "26feb622bcf9ce4e884d43259e60d2ce");

            request.AddParameter("selector", "FULL");
            request.AddParameter("url", "");
            request.AddFile("cat2.jpg", @"C:\Work\TestImages\cat2.jpg");
            //l1T4yrLkvE6r3ltGk6i5eoprQ7xcBs3t


            var response = client.Execute<dynamic>(request);
            
        }
    }
}
