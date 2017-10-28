using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Threading.Tasks;

using System.Text;
using System.IO;

using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.SimpleDB;
using Amazon.SimpleDB.Model;
using Amazon.S3;
using Amazon.S3.Model;

using System.Web.UI.HtmlControls;

namespace OpenPC_WebInterface
{
    public partial class _Default : System.Web.UI.Page
    {
        protected string sqlConnectionString = "server=openpc.cw6jjqgre3tn.us-east-1.rds.amazonaws.com;uid=openadmin;pwd=qKMymuX4eC7jk2aRxRSKUUUy;database=openpc";

        //private string defaultGoogleMapsConnectionUrl = "https://maps.googleapis.com/maps/api/staticmap?center=University%20of%20Tennessee,Knoxville,TN&zoom=16&size=1000x1000&maptype=satelite&markers=color:blue%7Clabel:S|MinKaoElectricalEngineeringandComputerScienceBuilding,MiddleDrive,Knoxville,TN&key=AIzaSyAkOJ4RRYeAmNU53Ccz8CWV_-fyCjyR0y4";

        protected string implicitlyDefinedGoogleMapsUrl = "https://maps.google.com/maps/api/staticmap?size=1000x1000&key=REPLACE&markers=color:blue|";

        protected List<string> sampleMarkerList = new List<string>
        {
            "35.958766,-83.924599",
            //"35.955093,-83.929786",
            "35.957579,-83.925751",
            "35.957762,-83.923997",
            "35.956258,-83.924128"

        };

        protected void Page_Load(object sender, EventArgs e)
        {
            // Load the page, silly.

            // Initialize the search

            //BuildingList.A

            // Load the map
            //LoadMap(sampleMarkerList);
            
            
        }

        protected void LoadMap(List<string> markerList)
        {
            string formattedMarkers = "";

            foreach (string marker in markerList)
            {
                formattedMarkers = formattedMarkers + "|" + marker;
            }
           
            GoogleMapsFrame.Attributes.Add("src", implicitlyDefinedGoogleMapsUrl.Replace("REPLACE", GetGoogleMapsStaticApiKey()) + formattedMarkers);
            GoogleMapsFrame.Visible = true;
        }

        /// <summary>
        /// Constructs a list of all the coordinates with machines matching the parameters specified in the search.
        /// </summary>
        /// <returns> A list of every location with a computer fitting the search criteria. </returns>
        protected List<string> GetCoordinateList(Dictionary<string, object> searchParameters)
        {
            return null;
        }

        /// <summary>
        /// Gets the API key for the Google Maps Static API.
        /// TODO: Make this read a file for the key.
        /// </summary>
        /// <returns> The API key to use with the Google Maps Static API. </returns>
        protected string GetGoogleMapsStaticApiKey()
        {
            return "AIzaSyAkOJ4RRYeAmNU53Ccz8CWV_-fyCjyR0y4";
        }

        protected void SubmitQuery_Click(object sender, EventArgs e)
        {
            LoadMap(sampleMarkerList);
        }
    }
}