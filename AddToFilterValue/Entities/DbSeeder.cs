using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddToFilterValue.Entities
{
    public static class DbSeeder
    {
        public static void SeedAll(EFContext context) 
        {
            SeedFilter(context);
        }

        private static void SeedFilter(EFContext context) 
        {
            //  FilterNames
            string[] filterNames = { "Легкові автомобілі", "Вантажівки" };
            foreach (var fName in filterNames) 
            {
                if (context.FilterNames.SingleOrDefault(x => x.Name == fName) == null) 
                {
                    context.FilterNames.Add(new FilterName { 
                    Name = fName
                    });

                    context.SaveChanges();
                }
            }
            //  FilterValues
            List<string[]> filterValuesTwoArr = new List<string[]> 
            {
                new string[] { "BMW", "Mercedes", "Mazda" },
                new string[] { "MAN", "Mercedes", "Ford" }
            };
            foreach (var vymir in filterValuesTwoArr) 
            {
                foreach (var item in vymir) 
                {
                    if (context.FilterValue.SingleOrDefault(x => x.Name == item) == null) 
                    {
                        context.FilterValue.Add(new FilterValue { 
                            Name = item
                        });

                        context.SaveChanges();
                    }
                }
            }
            //  FilterNameValues
            for (int i = 0; i < filterNames.Length; i++) 
            {
                var filterNameId = context.FilterNames.SingleOrDefault(x => x.Name == filterNames[i]).Id;
                foreach (var value in filterValuesTwoArr[i]) 
                {
                    var filterValueId = context.FilterValue.SingleOrDefault(x => x.Name == value).Id;
                    if (context.FilterNameValues.SingleOrDefault(x => x.FilterNameId == filterNameId &&
                    x.FilterValueId == filterValueId) == null) 
                    {
                        context.FilterNameValues.Add(new FilterNameValue { 
                            FilterNameId = filterNameId,
                            FilterValueId = filterValueId
                        });
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
