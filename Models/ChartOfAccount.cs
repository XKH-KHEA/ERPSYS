using System.ComponentModel.DataAnnotations;

namespace ERPSYS.Models
{

    public class ChartOfAccount
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        public string? AccountName { get; set; }

        [Required]
        public string? AccountType { get; set; } // Asset, Liability, Equity, etc.

        [Required]
        public decimal Balance { get; set; }
    }
}
