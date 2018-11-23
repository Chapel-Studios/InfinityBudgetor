namespace Budgetor.Repo.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class Account
    {
        [Key]
        public int LocalId { get; set; }

        [Required]
        [StringLength(50)]
        public string AccountName { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountType { get; set; }

        [StringLength(250)]
        public string Notes { get; set; }

        public DateTime DateTime_Created { get; set; }

        public DateTime? DateTime_Deactivated { get; set; }

        public bool IsSystem { get; set; }

    }
}
