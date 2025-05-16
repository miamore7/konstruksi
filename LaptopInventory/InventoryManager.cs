using System;
using System.Collections.Generic;

namespace LaptopInventory
{
    public class InventoryManager
    {
        private Dictionary<ItemType, List<Item>> inventory = new();  // Dictionary untuk menyimpan inventaris berdasarkan tipe item.

        public InventoryManager()
        {
            // Inisialisasi inventory untuk setiap tipe item yang ada dalam enum ItemType.
            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                inventory[type] = new List<Item>();  // Setiap tipe item mendapatkan list kosong.
            }
        }

        public void AddItem(Item item)
        {
            if (!inventory.ContainsKey(item.Type))  // Jika tipe item belum ada, buat list baru untuk tipe tersebut.
                inventory[item.Type] = new List<Item>();

            // Cek apakah item dengan nama yang sama sudah ada, jika ada, tambahkan kuantitasnya.
            var existingItem = inventory[item.Type].Find(i => i.Name == item.Name);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;  // Update kuantitas item yang ada.
            }
            else
            {
                inventory[item.Type].Add(item);  // Tambahkan item baru jika belum ada.
            }
        }

        public List<Item> GetItemsByType(ItemType type)
        {
            return inventory[type];  // Mengembalikan daftar item berdasarkan tipe.
        }

        public Dictionary<ItemType, List<Item>> GetAllInventory()
        {
            return inventory;  // Mengembalikan seluruh inventaris.
        }
    }
}
