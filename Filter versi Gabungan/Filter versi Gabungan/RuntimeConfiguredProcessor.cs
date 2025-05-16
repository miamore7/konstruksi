using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter_versi_Gabungan
{
    public static class RuntimeConfiguredProcessor
    {
        public static List<Barang> Apply(List<Barang> barangs, FilterSortConfig config)
        {
            var filtered = string.IsNullOrWhiteSpace(config.Kategori)
                ? barangs
                : barangs.Where(b => b.Kategori.Equals(config.Kategori, StringComparison.OrdinalIgnoreCase)).ToList();

            return config.SortBy switch
            {
                "Nama" => config.Ascending ? filtered.OrderBy(b => b.Nama).ToList() : filtered.OrderByDescending(b => b.Nama).ToList(),
                "Jumlah" => config.Ascending ? filtered.OrderBy(b => b.Jumlah).ToList() : filtered.OrderByDescending(b => b.Jumlah).ToList(),
                "TanggalMasuk" => config.Ascending ? filtered.OrderBy(b => b.TanggalMasuk).ToList() : filtered.OrderByDescending(b => b.TanggalMasuk).ToList(),
                _ => filtered
            };
        }
    }
}
