using Filter_varsi_runtime_configuration;
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

        var config = RuntimeConfigProvider.GetDefaultConfig(); // Ambil dari provider (bisa diubah)
        var result = RuntimeConfiguredProcessor.Apply(barangList, config);

        Console.WriteLine("Hasil filter & sort (runtime config):");
        foreach (var b in result)
        {
            Console.WriteLine($"{b.Nama} - {b.Kategori} - {b.Jumlah} - {b.TanggalMasuk}");
        }
    }
}