using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter_versi_Gabungan
{
    public static class TableDrivenFilterSorter
    {
        private static readonly Dictionary<string, Func<Barang, object>> sortTable = new()
        {
            { "Nama", b => b.Nama },
            { "Jumlah", b => b.Jumlah },
            { "TanggalMasuk", b => b.TanggalMasuk }
        };

        public static List<Barang> Apply(List<Barang> barangs, string sortKey, bool ascending = true)
        {
            if (!sortTable.ContainsKey(sortKey)) return barangs;

            var keySelector = sortTable[sortKey];
            return ascending
                ? barangs.OrderBy(keySelector).ToList()
                : barangs.OrderByDescending(keySelector).ToList();
        }
    }
}
