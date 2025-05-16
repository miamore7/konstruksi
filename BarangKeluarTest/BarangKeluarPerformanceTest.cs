using LaptopInventory.Models;
using LaptopInventory.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace BarangKeluarTest
{
    public class BarangKeluarPerformanceTest
    {
        private readonly BarangKeluarManager<Barang> _manager;
        private readonly string _configPath = "Models/BarangKeluarConfig.json";

        public BarangKeluarPerformanceTest()
        {
            _manager = new BarangKeluarManager<Barang>();

            if (!File.Exists(_configPath))
            {
                Directory.CreateDirectory("Models");
                File.WriteAllText(_configPath,
                    @"{
                        ""JumlahMaksimalPerTransaksi"": 100,
                        ""KategoriDilarang"": []
                    }");
            }

            _manager.LoadConfig(_configPath);
        }

        [Fact]
        public void KeluarkanBarang_10000Transaksi_PerformUnder2Seconds()
        {
            var barang = new Barang
            {
                Id = "BULK",
                Nama = "Barang Massal",
                Kategori = "Umum",
                Stok = 10000
            };

            int jumlahTransaksi = 10000;
            int jumlahPerTransaksi = 1;

            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < jumlahTransaksi; i++)
            {
                _manager.KeluarkanBarang(barang, jumlahPerTransaksi);
            }

            sw.Stop();

            // Assert performa: tidak lebih dari 2 detik
            Assert.True(sw.Elapsed.TotalSeconds < 2, $"Waktu eksekusi terlalu lama: {sw.Elapsed.TotalSeconds} detik");

            // Assert stok habis
            Assert.Equal(0, barang.Stok);
        }
    }
}
