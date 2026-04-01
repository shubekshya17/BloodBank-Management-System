using System.ComponentModel.DataAnnotations;

namespace BloodBankMVC.Models
{
    public class BloodGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(5)]
        public string BloodGroupName { get; set; }
    }
}
