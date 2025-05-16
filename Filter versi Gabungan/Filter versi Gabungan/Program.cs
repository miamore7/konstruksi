using Filter_versi_Gabungan;
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        var barangList = new List<Barang>
        {
            new Barang { Nama = "Laptop A", Kategori = "Laptop", Jumlah = 5, TanggalMasuk = DateTime.Now.AddDays(-1) },
            new Barang { Nama = "HP B", Kategori = "HP", Jumlah = 10, TanggalMasuk = DateTime.Now.AddDays(-2) },
            new Barang { Nama = "Laptop C", Kategori = "Laptop", Jumlah = 3, TanggalMasuk = DateTime.Now }
        };

        var config = new FilterSortConfig
        {
            Kategori = "Laptop",
            SortBy = "Jumlah",
            Ascending = false
        };

        // 1. Runtime Configuration
        var resultRuntime = RuntimeConfiguredProcessor.Apply(barangList, config);
        Console.WriteLine("Runtime Configuration:");
        Print(resultRuntime);

        // 2. Table-driven Sorting
        var resultTable = TableDrivenFilterSorter.Apply(barangList, "TanggalMasuk", ascending: true);
        Console.WriteLine("Table-Driven:");
        Print(resultTable);

        // 3. Generic Filter & Sort
        var filteredGeneric = GenericFilterSorter.FilterBy(barangList, b => b.Kategori == "HP");
        var sortedGeneric = GenericFilterSorter.SortBy(filteredGeneric, b => b.Nama);
        Console.WriteLine("Generics:");
        Print(sortedGeneric);
    }

    static void Print(List<Barang> list)
    {
        foreach (var b in list)
            Console.WriteLine($"{b.Nama} - {b.Kategori} - {b.Jumlah} - {b.TanggalMasuk:yyyy-MM-dd}");
    }
}