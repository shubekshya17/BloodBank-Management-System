using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodBankMVC.Models
{
    public class Audit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }
        public int? DonorId { get; set; }

        [ForeignKey("DonorId")]
        public virtual Donor Donor { get; set; }

        [Required]
        public int Unit { get; set; }

        public int? RequestorId { get; set; }

        [ForeignKey("RequestorId")]
        public virtual Requestor Requestor { get; set; }

        [Required]
        public string ActionType { get; set; } // "Donation" or "Request"
    }
}
