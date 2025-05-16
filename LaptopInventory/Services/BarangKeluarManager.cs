using LaptopInventory.Models;
using System.Text.Json;

namespace LaptopInventory.Services
{
    public class BarangKeluarManager<T> : IBarangKeluarService<T> where T : Barang
    {
        private BarangKeluarConfig? _config;

        public string GetCurrentLanguage()
        {
            return _config?.Bahasa ?? "ID";
        }


        public void LoadConfig(string configPath)
        {
            try
            {
                string json = File.ReadAllText(configPath);
                _config = JsonSerializer.Deserialize<BarangKeluarConfig>(json)
                          ?? new BarangKeluarConfig();
            }
            catch
            {
                _config = new BarangKeluarConfig();
            }
        }

        public class BarangKeluarConfig
        {
            public int JumlahMaksimalPerTransaksi { get; set; } = 100;
            public List<string> KategoriDilarang { get; set; } = new();
            public string Bahasa { get; set; } = "ID"; // Tambahkan properti ini
        }


        public void KeluarkanBarang(T barang, int jumlah)
        {
            // Validasi DbC yang diperketat
            if (_config == null)
            {
                throw new InvalidOperationException("Config belum di-load! Panggil LoadConfig() terlebih dahulu.");
            }

            if (barang == null)
                throw new ArgumentNullException(nameof(barang));

            if (jumlah <= 0)
                throw new ArgumentException("Jumlah harus positif!");

            if (_config.KategoriDilarang.Contains(barang.Kategori))
                throw new InvalidOperationException($"Kategori {barang.Kategori} dilarang!");

            if (jumlah > _config.JumlahMaksimalPerTransaksi)
                throw new InvalidOperationException($"Maksimal {_config.JumlahMaksimalPerTransaksi} per transaksi!");

            if (barang.Stok < jumlah)
                throw new InvalidOperationException($"Stok hanya {barang.Stok}!");

            barang.Stok -= jumlah;
        }
    }

    public class BarangKeluarConfig
    {
        public int JumlahMaksimalPerTransaksi { get; set; } = 100;
        public List<string> KategoriDilarang { get; set; } = new();
    }
}
