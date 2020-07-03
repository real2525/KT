namespace KTGK2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("phone")]
    public partial class phone
    {
        public int id { get; set; }

        [StringLength(100)]
        public string model { get; set; }

        public decimal? price { get; set; }

        [StringLength(100)]
        public string gerenal_note { get; set; }
    }
}
