using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter_varsi_runtime_configuration
{
    public static class RuntimeConfigProvider
    {
        public static FilterSortConfig GetDefaultConfig()
        {
            return new FilterSortConfig
            {
                Kategori = "Laptop",
                SortBy = "Jumlah",
                Ascending = false
            };
        }
    }
}
