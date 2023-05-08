using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDApi.Models
{
    public partial class TblRegion
    {
        [Key]
        public int RegionId { get; set; }
        public string RegionName { get; set; }
    }
}
