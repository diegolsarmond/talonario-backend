using System.Collections.Generic;

namespace Talonario.Api.Server.Application.ViewModels
{
    public class AddressComponent
    {
        #region Public Properties

        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }

        #endregion Public Properties
    }

    public class Bounds
    {
        #region Public Properties

        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }

        #endregion Public Properties
    }

    public class Geometry
    {
        #region Public Properties

        public Bounds bounds { get; set; }
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }

        #endregion Public Properties
    }

    public class GoogleMapsGeoCodeViewModel
    {
        #region Public Properties

        public List<Result> results { get; set; }
        public string status { get; set; }

        #endregion Public Properties
    }

    public class Location
    {
        #region Public Properties

        public double lat { get; set; }
        public double lng { get; set; }

        #endregion Public Properties
    }

    public class Northeast
    {
        #region Public Properties

        public double lat { get; set; }
        public double lng { get; set; }

        #endregion Public Properties
    }

    public class Result
    {
        #region Public Properties

        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public List<string> types { get; set; }

        #endregion Public Properties
    }

    public class Southwest
    {
        #region Public Properties

        public double lat { get; set; }
        public double lng { get; set; }

        #endregion Public Properties
    }

    public class Viewport
    {
        #region Public Properties

        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }

        #endregion Public Properties
    }
}