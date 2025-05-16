using System;
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
        }
    }
}
