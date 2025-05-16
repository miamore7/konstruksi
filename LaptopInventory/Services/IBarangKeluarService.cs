namespace LaptopInventory.Services
{
    public interface IBarangKeluarService<T> where T : Models.Barang
    {
        void LoadConfig(string configPath);
        void KeluarkanBarang(T barang, int jumlah);
    }
}