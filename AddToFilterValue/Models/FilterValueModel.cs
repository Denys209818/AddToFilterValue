using System;
using System.Collections.Generic;
using System.Text;

namespace AddToFilterValue.Models
{
    public class FilterValueModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; } = false;
    }
}
