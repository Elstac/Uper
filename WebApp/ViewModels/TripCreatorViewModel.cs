using System;
using WebApp.Data;
using System.Globalization;
using WebApp.Data.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.ViewModels
{
    public class TripCreatorViewModel
    {
        /// <summary>
        /// Get, Set Destination Address
        /// </summary>
        [Required]
        public Address DestinationAddress { get; set; }
        /// <summary>
        /// Get, Set Starting Address
        /// </summary>
        [Required]
        public Address StartingAddress { get; set; }
        /// <summary>
        /// Get, Set Cose
        /// </summary>
        [StringLength(4)]
        [Required]
        public string Cost { get; set; }
        /// <summary>
        /// Get, Set Description
        /// </summary>
        [StringLength(500)]
        [Required]
        public string Description { get; set; }
        /// <summary>
        /// Get, Set VechicleModel
        /// </summary>
        [StringLength(50)]
        [Required]
        public string VechicleModel { get; set; }
        /// <summary>
        /// Get, Set Size
        /// </summary>
        [Required]
        [Range(1, 15)]
        public int Size { get; set; }
        /// <summary>
        /// Get, Set Date
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        /// <summary>
        /// Get, Set Date
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DateEnd { get; set; }
        /// <summary>
        /// Get, Set whatever smoking is allowed or not
        /// </summary>
        [Required]
        public bool IsSmokingAllowed { get; set; }

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
        public string IsValid(TripCreatorViewModel model)
        {
            float Cost_;
            StringBuilder IsValid = new StringBuilder();

            if (float.TryParse(model.Cost, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out Cost_) != true) IsValid.Append("Cost is not a number!\n");

            if (Cost_ < 0.0) IsValid.Append("Cost is negative number!\n");

            if (model.DestinationAddress.City != null)
            {
                if (model.DestinationAddress.City.Length <= 0) IsValid.Append("Destination city not set!\n");
            }
            else IsValid = IsValid.Append("Destination city not set!\n");

            if (model.DestinationAddress.Street != null)
            {
                if (model.DestinationAddress.Street.Length <= 0) IsValid.Append("Destination street not set!\n");
            }
            else IsValid.Append("Destination street not set!\n");

            if (model.StartingAddress.City != null)
            {
                if (model.StartingAddress.City.Length <= 0) IsValid.Append("Starting city not set!\n");
            }
            else IsValid.Append("Starting city not set!\n");

            if (model.StartingAddress.Street != null)
            {
                if (model.StartingAddress.Street.Length <= 0) IsValid.Append("Starting street not set!\n");
            }
            else IsValid.Append("Starting street not set!\n");

            if (model.VechicleModel != null)
            {
                if (model.VechicleModel.Length <= 0) IsValid.Append("Vehicle model not set!\n");
            }
            else IsValid.Append("Vehicle model not set!\n");

            if (model.Size <= 0) IsValid.Append("Amount of seats is incorrect!\n");

            if (model.Description != null) 
            { 
            if (model.Description.Length <= 0) IsValid.Append("Description not set!\n");
            }
            else IsValid.Append("Description not set!\n");

            if (model.Date.CompareTo(DateTime.Now) < 0) IsValid.Append("Start of the trip is set in the past!\n");
            if (model.DateEnd.CompareTo(model.Date) < 0) IsValid.Append("End of the trip is set before start!\n");

            if (IsSmokingAllowed!=true && IsSmokingAllowed != false) IsValid.Append("Smoking not set!\n");
            return IsValid.ToString();
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
            tripDetails.DateEnd = this.DateEnd;
            tripDetails.IsSmokingAllowed = this.IsSmokingAllowed;
            tripDetails.Size = this.Size;

            return tripDetails;
        }
    }
}
