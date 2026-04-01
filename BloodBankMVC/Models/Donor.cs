using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankMVC.Models
{
    public class Donor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Blood Group")]
        public int BloodGroupId { get; set; }

        [ForeignKey("BloodGroupId")]
        [ValidateNever]
        public virtual BloodGroup BloodGroup { get; set; }

        [Required]
        [Display(Name = "Donation Date")]
        [DataType(DataType.Date)]
        public DateTime DonateDate { get; set; }

        [Required]
        [Range(1, 10)]
        [Display(Name = "Units")]
        public int Unit { get; set; }

        [Required]
        [Range(18, 65)]
        public int Age { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        // Status: 0 = Pending, 1 = Approved, 2 = Rejected
        public int Status { get; set; } = 0;
    }
}
