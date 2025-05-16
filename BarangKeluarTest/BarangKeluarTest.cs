using LaptopInventory.Models;
using LaptopInventory.Services;
using System;
using System.IO;
using Xunit;

namespace BarangKeluarTest
{
    public class BarangKeluarTest
    {
        private readonly BarangKeluarManager<Barang> _manager;
        private readonly Barang _barangValid;
        private readonly string _configPath = "Models/BarangKeluarConfig.json";

        public BarangKeluarTest()
        {
            _manager = new BarangKeluarManager<Barang>();
            _barangValid = new Barang
            {
                Id = "L1",
                Nama = "Laptop ASUS",
                Kategori = "Laptop",
                Stok = 10
            };

            // Buat file config dummy jika tidak ada
            if (!File.Exists(_configPath))
            {
                Directory.CreateDirectory("Models");
                File.WriteAllText(_configPath,
                    @"{
                        ""JumlahMaksimalPerTransaksi"": 5,
                        ""KategoriDilarang"": [""Bahan Berbahaya""]
                    }");
            }
        }

        [Fact]
        public void KeluarkanBarang_StokCukup_StokBerkurang()
        {
            // Arrange
            _manager.LoadConfig(_configPath);
            int jumlahKeluar = 3;
            int stokAwal = _barangValid.Stok;

            // Act
            _manager.KeluarkanBarang(_barangValid, jumlahKeluar);

            // Assert
            Assert.Equal(stokAwal - jumlahKeluar, _barangValid.Stok);
        }

        [Fact]
        public void KeluarkanBarang_JumlahMelebihiStok_ThrowException()
        {
            // Arrange
            _manager.LoadConfig(_configPath);
            int jumlahKeluar = 15;

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(
                () => _manager.KeluarkanBarang(_barangValid, jumlahKeluar)
            );
            Assert.Contains("Stok hanya", ex.Message);
        }

        [Fact]
        public void KeluarkanBarang_JumlahNegatif_ThrowException()
        {
            // Arrange
            _manager.LoadConfig(_configPath);
            int jumlahKeluar = -1;

            // Act & Assert
            Assert.Throws<ArgumentException>(
                () => _manager.KeluarkanBarang(_barangValid, jumlahKeluar)
            );
        }

        [Fact]
        public void KeluarkanBarang_ConfigTidakDiload_ThrowException()
        {
            // Arrange
            var managerBaru = new BarangKeluarManager<Barang>();
            var barang = new Barang { Id = "TEST", Stok = 10 };

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(
                () => managerBaru.KeluarkanBarang(barang, 1)
            );

            // Verifikasi pesan error
            Assert.Contains("Config belum di-load", ex.Message);
        }

        [Fact]
        public void LoadConfig_FileTidakAda_DefaultConfigDigunakan()
        {
            // Arrange
            var managerBaru = new BarangKeluarManager<Barang>();
            string fakePath = "fake_config.json";

            // Act
            managerBaru.LoadConfig(fakePath);
            managerBaru.KeluarkanBarang(_barangValid, 10);

            // Assert
            Assert.Equal(0, _barangValid.Stok); // 10 - 10 = 0
        }
    }
}