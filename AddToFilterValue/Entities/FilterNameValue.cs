using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AddToFilterValue.Entities
{
    [Table("tblFilterNameValueHW3")]
    public class FilterNameValue
    {
        [Key, Column(Order = 0), ForeignKey("FilterName.Id")]
        public int FilterNameId { get; set; }
        public virtual FilterName FilterName { get; set; }
        [Key, Column(Order = 1), ForeignKey("FilterValue.Id")]
        public int FilterValueId { get; set; }
        public virtual FilterValue FilterValue { get; set; }
    }
}
