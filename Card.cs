namespace AutoLotModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Card")]
    public partial class Card
    {
        public int ID { get; set; }


        public int? PIN { get; set; }


        public int? TipCardID { get; set; }

        public int? ContID { get; set; }

        public virtual Cont Cont { get; set; }
    }
}
