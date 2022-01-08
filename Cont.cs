namespace AutoLotModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cont")]
    public partial class Cont
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cont()
        {
            Cards = new HashSet<Card>();
            Tranzactiis = new HashSet<Tranzactii>();
        }

        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string numar_cont { get; set; }

        public decimal? sold { get; set; }

        [Column(TypeName = "date")]
        public DateTime? data_deschiderii { get; set; }

        public int? ClientID { get; set; }

        public int? TipContBancarID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Card> Cards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Tranzactii> Tranzactiis { get; set; }

      
    }
}
