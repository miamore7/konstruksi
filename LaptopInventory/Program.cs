using LaptopInventory;
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

        var filtered = TableDrivenFilterSorter.FilterByKategori(barangList, "Laptop");

        var sorted = TableDrivenFilterSorter.SortBarang(
            filtered,
            TableDrivenFilterSorter.SortField.Jumlah,
            ascending: false
        );

        Console.WriteLine("Hasil filter & sort:");
        foreach (var barang in sorted)
        {
            Console.WriteLine($"{barang.Nama} - {barang.Kategori} - {barang.Jumlah} - {barang.TanggalMasuk}");
        }
    }
}