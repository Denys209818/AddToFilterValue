using System;
using System.Collections.Generic;
using System.Text;

namespace AddToFilterValue.Models
{
    public class JoinedElementsModel
    {
        public int FilterNameId { get; set; }
        public string FilterName { get; set; }
        public int FilterValueId { get; set; }
        public string FilterValue { get; set; }
    }
}
