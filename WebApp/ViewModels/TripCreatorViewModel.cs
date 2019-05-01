using System;
using WebApp.Data;
using System.Globalization;
using WebApp.Data.Repositories;

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
        /// Get, Set Size
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Get, Set Date
        /// </summary>
        public DateTime Date { get; set; }

        public string MapData { get; set; }

        /// <summary>
        /// Check if data is valid
        /// </summary>
        /// <param name="model">
        /// New model that was created in TripCreator 
        /// </param>
        /// <returns>
        /// Retruns true if data is valid, or false if is not
        /// </returns>
        public bool IsValid(TripCreatorViewModel model)
        {
            float Cost_;
            bool IsValid = true;

            if (float.TryParse(model.Cost, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out Cost_) != true) IsValid = false;

            if (Cost_ < 0.0) IsValid = false;

            if (model.DestinationAddress.City != null)
            {
                if (model.DestinationAddress.City.Length <= 0) IsValid = false;
            }
            else IsValid = false;

            if (model.DestinationAddress.Street != null)
            {
                if (model.DestinationAddress.Street.Length <= 0) IsValid = false;
            }
            else IsValid = false;

            if (model.DestinationAddress.City != null)
            {
                if (model.StartingAddress.City.Length <= 0) IsValid = false;
            }
            else IsValid = false;

            if (model.DestinationAddress.City != null)
            {
                if (model.StartingAddress.Street.Length <= 0) IsValid = false;
            }
            else IsValid = false;

            if (model.VechicleModel != null)
            {
                if (model.VechicleModel.Length <= 0) IsValid = false;
            }
            else IsValid = false;

            if (model.Size <= 0) IsValid = false;

            if(model.Description != null)
            { 
            if (model.Description.Length <= 0) IsValid = false;
            }
            else IsValid = false;

            if (model.Date.CompareTo(DateTime.Now) < 0) IsValid = false;

            return IsValid;
        }

        public TripDetails GetTripDetailsModel()
        {
            TripDetails tripDetails = new TripDetails();
            tripDetails.DestinationAddress = this.DestinationAddress;
            tripDetails.StartingAddress = this.StartingAddress;
            tripDetails.Cost = float.Parse(this.Cost, CultureInfo.InvariantCulture.NumberFormat);
            tripDetails.Description = this.Description;
            tripDetails.VechicleModel = this.VechicleModel;
            tripDetails.Date = this.Date;
            tripDetails.Size = this.Size;

            return tripDetails;
        }
    }
}
