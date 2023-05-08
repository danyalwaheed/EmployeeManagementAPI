using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDApi.Models
{
    public partial class TblDocumentCategory
    {
        [Key]
        public int DocumentCategoryId { get; set; }
        public string DocumentName { get; set; }
    }
}
