using System;
using System.Collections.Generic;

namespace SCG.CAD.ETAX.MODEL.etaxModel
{
    public partial class ProfileSellOrg
    {
        [Key]
        public int SellOrgNo { get; set; }
        public string SellOrgCode { get; set; } = null!;
        public string? CompanyCode { get; set; }
        public string? SellOrgDescripttion { get; set; }
        public string? SellOrgEmail { get; set; }
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public int Isactive { get; set; }
    }
}
