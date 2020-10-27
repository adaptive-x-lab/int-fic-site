using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace IntFicSite.Pages
{
    [EnableCors("MyAllowPolicy")]
    public class Last15Model : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Node { get; set; } // this should really be "passage" in Twine-speak

        [BindProperty(SupportsGet = true)]
        public string Session { get; set; }

        private TelemetryClient telemetry = new TelemetryClient();

        private  Dictionary<string, double> ranks = new Dictionary<string, double>
        {
            {"D4", 1},
            {"F4", 6},
            {"I2", 2},
            {"L3", 12},
            {"Q4", 8},
            {"O1", 10},
            {"T1", 11},
            {"R3", 4},
            {"R4", 3},
            {"Q2", 9},
            {"K9", 7},
            {"K8", 5}
        };

        public IActionResult OnGet()
        {
            var properties = new Dictionary <string, string> 
            {
                {"Story", "Last15"},
                {"Passage", Node}, 
                {"Session", Session}
            };

            if (ranks.ContainsKey(Node.ToUpper()))
            {
                properties.Add("End",Node);
                properties.Add("Rank",GetScore(Node).ToString());
            }


            var metrics = new Dictionary <string, double>
            {
                {"Rank", GetScore(Node)}
            };

            telemetry.TrackEvent(Node,properties, metrics);

            return new JsonResult(new {satus = "OK",site="Last15", node=Node,session=Session});
        }

        private double GetScore(string node)
        {
            return ranks.ContainsKey(node.ToUpper()) ? ranks[node.ToUpper()] : 0;
        }
    }
}
