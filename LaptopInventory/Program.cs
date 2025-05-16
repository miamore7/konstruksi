<<<<<<< HEAD
﻿using System;
using LaptopInventory;

class Program
{
    static void Main()
    {
        Console.WriteLine("== SISTEM INVENTORY GUDANG ==");

        // Inisialisasi manajer inventory dan state machine gudang
        var inventory = new InventoryManager();
        var stateMachine = new WarehouseStateMachine();

        while (true) // Loop utama program
        {
            // Tampilkan status gudang dan menu utama
            Console.WriteLine($"\nStatus Gudang: {stateMachine.CurrentState}");
            Console.WriteLine("1. Buka Gudang");
            Console.WriteLine("2. Tutup Gudang");
            Console.WriteLine("3. Tambah Barang");
            Console.WriteLine("4. Tampilkan Inventory");
            Console.WriteLine("0. Keluar");

            Console.Write("Pilih menu: ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input tidak valid.");
                continue;
            }

            switch (input)
            {
                case "1":
                    stateMachine.Open(); // Ubah status gudang jadi terbuka
                    Console.WriteLine("Gudang dibuka.");
                    break;
                case "2":
                    stateMachine.Close(); // Ubah status gudang jadi tertutup
                    Console.WriteLine("Gudang ditutup.");
                    break;
                case "3":
                    // Cek apakah gudang terbuka sebelum menambah barang
                    if (!stateMachine.CanAddItem())
                    {
                        Console.WriteLine("Gudang tertutup. Tidak bisa menambahkan barang.");
                        break;
                    }

                    // Input nama barang
                    Console.Write("Nama Barang: ");
                    string name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("Nama barang tidak boleh kosong.");
                        break;
                    }

                    // Input jenis barang (1 = Laptop, 2 = HP)
                    Console.Write("Jenis (1 = Laptop, 2 = HP): ");
                    string typeInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(typeInput) || !int.TryParse(typeInput, out int typeChoice) || typeChoice < 1 || typeChoice > 2)
                    {
                        Console.WriteLine("Jenis tidak valid.");
                        break;
                    }

                    ItemType type = (ItemType)(typeChoice - 1); // Konversi input ke enum ItemType

                    // Input jumlah barang
                    Console.Write("Jumlah: ");
                    string quantityInput = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(quantityInput) || !int.TryParse(quantityInput, out int quantity) || quantity <= 0)
                    {
                        Console.WriteLine("Jumlah tidak valid.");
                        break;
                    }

                    // Buat objek item dan tambahkan ke inventory
                    var item = new Item(name, type, quantity);
                    inventory.AddItem(item);
                    Console.WriteLine("Barang berhasil ditambahkan.");
                    break;
                case "4":
                    // Tampilkan seluruh barang di inventory
                    var all = inventory.GetAllInventory();
                    Console.WriteLine("\n== Daftar Inventory ==");
                    foreach (var category in all)
                    {
                        Console.WriteLine($"- {category.Key}:");
                        foreach (var i in category.Value)
                            Console.WriteLine($"   • {i.Name} - {i.Quantity} unit");
                    }
                    break;
                case "0":
                    return; // Keluar dari program
                default:
                    Console.WriteLine("Menu tidak dikenal.");
                    break;
            }

            // Tunggu input untuk lanjut, lalu bersihkan layar
            Console.WriteLine("\nTekan tombol apa saja untuk melanjutkan...");
            Console.ReadKey();
            Console.Clear();
=======
﻿using LaptopInventory.Models;
using LaptopInventory.Services;
using System.Text;

namespace LaptopInventory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var barangDummy = new List<Barang>
            {
                new() { Id = "L1", Nama = "Laptop ASUS", Kategori = "Laptop", Stok = 150 },
                new() { Id = "L2", Nama = "Acer Predator", Kategori = "Laptop", Stok = 5 },
                new() { Id = "HP1", Nama = "iPhone 13", Kategori = "HP", Stok = 8 }
            };

            var manager = new BarangKeluarManager<Barang>();
            manager.LoadConfig("Models/BarangKeluarConfig.json");

            var langManager = new LanguageManager();
            string selectedLang = "";

            // Pilih bahasa 1x di awal runtime
            while (true)
            {
                Console.WriteLine("Pilih Bahasa / Choose Language:");
                Console.WriteLine("1. Bahasa Indonesia");
                Console.WriteLine("2. English");
                Console.Write("Pilihan Anda / Your Choice (1/2): ");
                string choice = Console.ReadLine()?.Trim();

                if (choice == "1")
                {
                    selectedLang = "ID";
                    break;
                }
                else if (choice == "2")
                {
                    selectedLang = "EN";
                    break;
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid. Coba lagi.");
                }
            }

            langManager.LoadLanguage(selectedLang);

            // Mulai aplikasi
            Console.WriteLine(langManager.Get("JudulAplikasi"));

            while (true)
            {
                try
                {
                    Console.WriteLine("\n" + langManager.Get("DaftarBarang"));
                    Console.WriteLine($"{langManager.Get("KolomID"),-5}| {langManager.Get("KolomNama"),-20}| {langManager.Get("KolomKategori"),-10}| {langManager.Get("KolomStok")}");
                    Console.WriteLine(new string('-', 45));

                    foreach (var barang in barangDummy)
                    {
                        Console.WriteLine($"{barang.Id.PadRight(5)}| {barang.Nama.PadRight(20)}| {barang.Kategori.PadRight(10)}| {barang.Stok}");
                    }

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\n" + langManager.Get("InputID"));
                    string idBarang = Console.ReadLine()?.Trim().ToUpper() ?? string.Empty;

                    if (string.IsNullOrEmpty(idBarang))
                        throw new ArgumentException(langManager.Get("Error_IDKosong"));

                    Console.Write(langManager.Get("InputJumlah"));
                    if (!int.TryParse(Console.ReadLine(), out int jumlah) || jumlah <= 0)
                        throw new ArgumentException(langManager.Get("Error_JumlahInvalid"));

                    var barangDipilih = barangDummy.Find(b =>
                        b.Id.Equals(idBarang, StringComparison.OrdinalIgnoreCase))
                        ?? throw new KeyNotFoundException(
                            string.Format(langManager.Get("Error_BarangTidakDitemukan"), idBarang));

                    int stokLama = barangDipilih.Stok;
                    manager.KeluarkanBarang(barangDipilih, jumlah);

                    Console.WriteLine();
                    Console.WriteLine(langManager.Format("Sukses", jumlah, barangDipilih.Nama));
                    Console.ResetColor();
                    Console.WriteLine(langManager.Format("PerubahanStok", stokLama, barangDipilih.Stok));
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[ERROR] " + ex.Message);
                    Console.ResetColor();

                    if (ex.InnerException != null)
                        Console.WriteLine("Detail: " + ex.InnerException.Message);
                }

                // Konfirmasi lanjut
                Console.Write("\n" + langManager.Get("KonfirmasiKeluar"));
                if (Console.ReadLine()?.ToLower() == "y")
                {
                    Console.WriteLine(langManager.Get("PesanKeluar"));
                    break;
                }
            }
>>>>>>> ageng
        }
    }
}
