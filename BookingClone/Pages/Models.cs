using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookingClone.Pages
{
    public class Models
    {
    }


    public class OfferingModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstMidName { get; set; }
        [Required]
        public string LastName { get; set; }
    }

    public class BookModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Offer Added")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Agent Name")]
        public string LeaderName { get; set; }
        [Display(Name = "Hotel Reference Number")]
        public string ReferenceNo { get; set; }
        [Display(Name = "Hotel Address")]
        public string HotelAddress { get; set; }
        [Display(Name = "Hotel Name")]
        public string HotelName { get; set; }
        [Display(Name = "Valid From")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ValidFrom { get; set; }
        [Display(Name = "Valid Until")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ValidUntil { get; set; }
        [Display(Name = "Total Nights")]
        public int TotalNights { get; set; }
        [Display(Name = "Room Type")]
        public string RoomType { get; set; }
        [Display(Name = "Meal Type")]
        public string Meal { get; set; }
        [Display(Name = "Total Rooms Available")]
        public int TotalRooms { get; set; }
        [Display(Name = "Special Comments")]
        public string SpecialComments { get; set; }
        [Display(Name = "Destination")]
        public string Destination { get; set; }
        [Display(Name = "Total Price")]
        public int NetRate { get; set; }
        [Display(Name = "Original Room Price")]
        public int GrossRate { get; set; }
        [Display(Name = "Discount Included")]
        public int Discount { get; set; }
        [Display(Name = "Maximum Adults")]
        public int MaximumAdults { get; set; }
        [Display(Name = "Maximum Kids")]
        public int MaximumKids { get; set; }
        [Display(Name = "Cancellation Policy")]
        public string CancellationPolicy { get; set; }
        [Display(Name = "Offering Images")]
        public string OfferingImage { get; set; }
        public ICollection<Image> ImagesUploaded { get; set; }
        [Display(Name = "Hotel Rating")]
        [Range(0, 5)]
        public int Rating { get; set; }
        [Display(Name = "Full Summary in Detail")]
        public string OfferingDescription { get; set; }
        // public List<Guid> ImageIds { get; set; }
        [NotMapped]
        public ICollection<Gender> Features { get; set; }

    }

    public enum Features
    {
        Breakfast,
        Gym,
        [Display(Name = "Air Conditioning")]
        AirConditioning,
        TV,
        [Display(Name = "Free Parking")]
        FreeParking,
        [Display(Name = "Free Wifi")]
        FreeWifi,
        [Display(Name = "Tourist Focused")]
        Tourist,
        [Display(Name = "Business Focused")]
        Business,
        [Display(Name = "Swimming")]
        SwimmingPool,
        [Display(Name = "Room Service")]
        RoomService,
        Restaurant
    }

    public enum Gender
    {
        Male,
        Female,
        Other
    }
    public class GuestDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }
        [Display(Name = "Gender")]
        public string Gender { get; set; }
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
        [Display(Name = "Residence Country")]
        public string ResidenceCountry { get; set; }
        [Display(Name = "Travel Purpose")]
        public string TravelPurpose { get; set; }
        [Display(Name = "Identification Number")]
        public string IdentificationNumber { get; set; }
        public Guid BookingOfferId {get; set;}

    }


    public class Image
    {
        [Key]
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public BookModel BookModel { get; set; }
        [ForeignKey("BookModel")]
        public Guid BookModelId { get; set; }
    }


    public class ContactFormModel
    {
        [Required]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }

    }
}
