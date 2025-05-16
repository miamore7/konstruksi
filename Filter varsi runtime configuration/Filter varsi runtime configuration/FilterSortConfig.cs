using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter_varsi_runtime_configuration
{
    public class FilterSortConfig
    {
        public string Kategori { get; set; }
        public string SortBy { get; set; }  // Contoh: "Nama", "Jumlah", "TanggalMasuk"
        public bool Ascending { get; set; }
    }
}
