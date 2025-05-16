namespace LaptopInventory
{
    public enum ItemType  // Mendeklarasikan enum ItemType yang berisi tipe-tipe item yang dapat ada di inventaris.
    {
        Laptop,  // Tipe item Laptop.
        HP      // Tipe item HP.
    }

    public class Item  // Kelas Item yang merepresentasikan sebuah item dalam inventaris.
    {
        public string Name { get; set; }  // Nama item (misalnya nama laptop atau HP).
        public ItemType Type { get; set; }  // Tipe item, yang merupakan nilai dari enum ItemType.
        public int Quantity { get; set; }  // Kuantitas item yang ada.

        // Konstruktor untuk membuat instance baru dari Item dengan nama, tipe, dan kuantitas tertentu.
        public Item(string name, ItemType type, int quantity)
        {
            Name = name;
            Type = type;
            Quantity = quantity;
        }
    }
}
