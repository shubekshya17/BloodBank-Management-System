using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankMVC.Models
{
    public class BloodInventory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Blood Group")]
        public int BloodGroupId { get; set; }

        [ForeignKey("BloodGroupId")]
        public virtual BloodGroup BloodGroup { get; set; }

        [Required]
        [Display(Name = "Quantity (Units)")]
        public int Quantity { get; set; }
    }
}
