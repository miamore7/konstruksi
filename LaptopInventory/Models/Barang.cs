namespace LaptopInventory.Models
{
    public class Barang
    {
        public string Id { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
        public string Kategori { get; set; } = string.Empty;
        public int Stok { get; set; }
    }
}