
using System;
using System.Collections.Generic;

namespace LaptopInventory
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new LoginService();

            Console.Write("Username: ");
            var username = Console.ReadLine();

            Console.Write("Password: ");
            var password = Console.ReadLine();

            try
            {
                if (service.Login(username, password))
                {
                    if (service.CurrentLoginState == LoginState.LoggedIn)
                    {
                        Console.WriteLine($"Login berhasil sebagai {service.LoggedInUser.Role}");

                        if (service.LoggedInUser.Role == "admin")
                        {
                            AdminMenu(service);
                        }
                        else
                        {
                            Console.WriteLine(service.HandleByRole(service.LoggedInUser.Role));
                        }

                        service.Logout();
                        Console.WriteLine("Logout berhasil.");
                    }
                }
                else
                {
                    Console.WriteLine("Login gagal.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void AdminMenu(LoginService service)
        {
            while (true)
            {
                Console.WriteLine("\n=== Menu Admin ===");
                Console.WriteLine("1. Lihat Semua Barang");
                Console.WriteLine("2. Filter Barang Berdasarkan Kategori");
                Console.WriteLine("3. Lihat Barang Tersorting Nama");
                Console.WriteLine("0. Logout");
                Console.Write("Pilih menu: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowItems(service.GetItems());
                        break;
                    case "2":
                        Console.Write("Masukkan kategori (HP/Laptop): ");
                        var cat = Console.ReadLine();
                        ShowItems(service.GetItems(cat));
                        break;
                    case "3":
                        ShowItems(service.GetItems(sortByName: true));
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Pilihan tidak valid.");
                        break;
                }
            }
        }

        static void ShowItems(List<Item> items)
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Data barang tidak ditemukan.");
                return;
            }

            Console.WriteLine("\nDaftar Barang:");
            foreach (var item in items)
            {
                Console.WriteLine($"- ID: {item.Id}, Nama: {item.Name}, Kategori: {item.Category}, Jumlah: {item.Quantity}");
            }
        }
    }
}

