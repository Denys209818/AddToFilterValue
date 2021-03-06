using System;
using System.Collections.Generic;
using System.Text;

namespace AddToFilterValue.Models
{
    public class FilterNameModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCollapsed { get; set; } = true;
        public ICollection<FilterValueModel> Children { get; set; }
    }
}
