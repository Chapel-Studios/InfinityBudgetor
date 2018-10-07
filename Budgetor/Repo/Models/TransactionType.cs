namespace Budgetor.Repo.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TransactionType
    {
        [Key]
        [Column("TransactionType")]
        [StringLength(10)]
        public string Type { get; set; }

        [Required]
        [StringLength(50)]
        public string TransactionName { get; set; }
    }
}
