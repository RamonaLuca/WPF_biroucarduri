namespace AutoLotModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tranzactii")]
    public partial class Tranzactii
    {
        public int ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? data_tranzactie { get; set; }

        public decimal? suma { get; set; }

        public int? TipTranzactieID { get; set; }

        public int? ContID { get; set; }

        public virtual Cont Cont { get; set; }
    }
}
