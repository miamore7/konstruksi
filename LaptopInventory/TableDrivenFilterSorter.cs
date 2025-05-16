using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopInventory
{
    internal class TableDrivenFilterSorter
    {
        public enum SortField
        {
            Nama,
            Jumlah,
            TanggalMasuk
        }

        public static List<Barang> FilterByKategori(List<Barang> barangList, string kategori)
        {
            return barangList
                .Where(b => b.Kategori.Equals(kategori, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public static List<Barang> SortBarang(List<Barang> barangList, SortField field, bool ascending = true)
        {
            Func<Barang, object> keySelector = field switch
            {
                SortField.Nama => b => b.Nama,
                SortField.Jumlah => b => b.Jumlah,
                SortField.TanggalMasuk => b => b.TanggalMasuk,
                _ => b => b.Nama
            };

            return ascending
                ? barangList.OrderBy(keySelector).ToList()
                : barangList.OrderByDescending(keySelector).ToList();
        }
    }
}
