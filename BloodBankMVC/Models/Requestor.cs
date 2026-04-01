using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankMVC.Models
{
    public class Requestor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Address { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required]
        [Range(1, 10)]
        [Display(Name = "Units Requested")]
        public int UnitRequested { get; set; }

        [Required]
        [Display(Name = "Request Date")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Blood Group")]
        public int BloodGroupId { get; set; }

        [ForeignKey("BloodGroupId")]
        [ValidateNever]
        public virtual BloodGroup BloodGroup { get; set; }

        // 0 = UnAssigned, 1 = Assigned, 
        public int Status { get; set; } = 0;
    }
}
