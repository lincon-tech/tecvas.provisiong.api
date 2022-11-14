using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Techrunch.TecVas.Entities.Inventory
{
    [Table("vtu_stock_sales_history")]
    public class PartnerStockSalesHistory
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int RecordId { get; set; }
        [Column("tenant_id")]
        public int TenantId { get; set; }

        [Column("partner_id")]
        public int PartnerId { get; set; }
       
        [Column("service_provider_id")]
        public int ServiceProviderId { get; set; }

        [Column("qoh_before")]
        public int QuantityOnHand_Before { get; set; }

        [Column("qoh_after")]
        public int QuantityOnHand_After { get; set; }

        [Column("transref")]
        public string Transref { get; set; }

    }
}
