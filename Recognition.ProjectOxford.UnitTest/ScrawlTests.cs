using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Harcourts.Face.Recognition.ProjectOxford.UnitTest
{
    public class ScrawlTests
    {
        [Test, Explicit]
        public async Task<List<dynamic>> ScrawlGrenadierAgents()
        {
            string html;
            using (var client = new HttpClient())
            {
                var uri = new Uri("http://grenadier.harcourts.co.nz/Meet-the-Team", UriKind.Absolute);
                html = await client.GetStringAsync(uri);

            }

            if (!html.Contains("harcourtsPublic.websiteContent.masterPageInit"))
            {
                throw new InvalidOperationException("Invalid target page.");
            }

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Select persons.
            const string selector = "//div[@data-sid=\"3290\"]"
                                    + "/section[contains(@class,'photo-board-group')]"
                                    + "/ul[contains(@class,'team-list')]"
                                    + "/li";
            var persons = doc.DocumentNode.SelectNodes(selector);
            if (!persons.Any())
            {
                throw new InvalidOperationException("Not possible unless Grenadier bank corrupted.");
            }

            var personWanted = new Dictionary<string, dynamic>();

            // Resolve each person.
            foreach (var person in persons)
            {
                var imgSrc = person.SelectSingleNode("a[1]/img[@src]")?.Attributes["src"]?.Value ?? string.Empty;
                if (!imgSrc.Contains("photos.harcourts.co.nz") || imgSrc.Contains("agent-no-image"))
                {
                    // Should be using harcourts photos.
                    continue;
                }

                var personName = person.SelectSingleNode("h2[1]")?.InnerText?.Trim();
                if (string.IsNullOrWhiteSpace(personName))
                {
                    // Ask his/her mon why that guys doesn't have a name?!
                    continue;
                }

                var email = person.SelectSingleNode("a[2][contains(@href,'mailto:')]")
                    ?.Attributes["href"]?.Value?.Replace("mailto:", string.Empty);
                if (string.IsNullOrWhiteSpace(email) || personWanted.ContainsKey(email))
                {
                    // No email or email has been added.
                    continue;
                }

                var positionName = person.SelectSingleNode("h3[1]")?.InnerText?.Trim() ?? string.Empty;
                var lastComma = positionName.LastIndexOf(", ", StringComparison.InvariantCultureIgnoreCase);
                if (lastComma >= 0)
                {
                    // Something like CEO, Company. Or, CEO, CFO, Company.
                    positionName =
                        positionName.Remove(lastComma).Trim() +
                        " at " +
                        positionName.Remove(0, lastComma + 2).Trim();
                }

                var profile = person.SelectSingleNode("a[1]")?.Attributes["href"]?.Value ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(profile))
                {
                    profile = "http://grenadier.harcourts.co.nz/" + profile.TrimStart('/');
                }

                personWanted.Add(
                    email,
                    new
                    {
                        personName = personName,
                        position = positionName,
                        emailAddress = email,
                        photo = imgSrc,
                        profile = profile
                    });
            }

            return personWanted.Values.ToList();
        }
    }
}
