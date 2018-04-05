using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace backend.Models
{
    [Table("event")]
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Title")]
        public string Title { get; set; }

        [Display(Name="Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name="Start")]
        [DateLessThan("End", ErrorMessage = "Start date must be before end date")]
        public DateTime Start { get; set; }

        [Required]
        [Display(Name="End")]
        public DateTime End { get; set; }

        [Display(Name="Owners")]
        public virtual ICollection<EventOwner> Owners { get; set; }

        [Display(Name="Attendees")]
        public virtual ICollection<EventAttendee> Attendees { get; set; }

        [Display(Name="Locations")]
        public virtual ICollection<EventLocation> Locations { get; set; }
    }

    public class EventView
    {
        public Event Event { get; set; }

        public List<Location> Locations { get; set; }
        public List<People> Owners { get; set; }
        public List<People> Attendees { get; set; }

        [Display(Name = "Locations")]
        public IEnumerable<SelectListItem> LocationList { get; set; }

        [Display(Name = "Owners")]
        public IEnumerable<SelectListItem> OwnerList { get; set; }

        [Display(Name = "Attendees")]
        public IEnumerable<SelectListItem> AttendeeList { get; set; }

        public IEnumerable<int> SelectedLocations { get; set; }
        public IEnumerable<int> SelectedOwners { get; set; }
        public IEnumerable<int> SelectedAttendees { get; set; }
    }

    // literally copied from stackoverflow lmao
    // https://stackoverflow.com/questions/41900485/custom-validation-attributes-comparing-two-properties-in-the-same-model
    public class DateLessThanAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public DateLessThanAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = (DateTime)value;

            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
                throw new ArgumentException("Property with this name not found");

            var comparisonValue = (DateTime)property.GetValue(validationContext.ObjectInstance);

            if (currentValue > comparisonValue)
                return new ValidationResult(ErrorMessage);

            return ValidationResult.Success;
        }
    }
}
