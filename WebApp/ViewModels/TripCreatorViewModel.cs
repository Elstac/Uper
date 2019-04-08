using System;
using WebApp.Data;
using System.Globalization;
namespace WebApp.ViewModels
{
    public class TripCreatorViewModel
    {
        /// <summary>
        /// Get, Set Destination Address
        /// </summary>
        public Address DestinationAddress { get; set; }
        /// <summary>
        /// Get, Set Starting Address
        /// </summary>
        public Address StartingAddress { get; set; }
        /// <summary>
        /// Get, Set Cose
        /// </summary>
        public string Cost { get; set; }
        /// <summary>
        /// Get, Set Description
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Get, Set VechicleModel
        /// </summary>
        public string VechicleModel { get; set; }
        /// <summary>
        /// Get, Set Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Check if cost that is took as a string is required float 
        /// </summary>
        /// <param name="_Cost">
        /// String took from page
        /// </param>
        /// <returns>
        /// return true if _Cost is float and is >= 0, otherwise return false 
        /// </returns>
        public bool IsCostValid(string _Cost)
        {
            float Cost_;
            bool IsValid = float.TryParse(_Cost,NumberStyles.Float,CultureInfo.InvariantCulture.NumberFormat,out Cost_);
            if(IsValid)
            {
                if (Cost_ >= 0.0) return true;
            }
            return false;
        }

        public void ToDataBase()
        {
            //TO DO 
            //Add saving to database
            //Add DriverId 
        }
    }
}
