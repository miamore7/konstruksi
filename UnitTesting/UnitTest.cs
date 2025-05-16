using Xunit;  // Menggunakan Xunit sebagai framework untuk pengujian unit.
using LaptopInventory;  // Mengimpor namespace LaptopInventory yang berisi kelas-kelas yang diuji.

namespace UnitTest
{
    public class UnitTest  // Kelas UnitTest untuk menyimpan berbagai metode pengujian.
    {
        [Fact]  // Menandakan metode ini adalah tes unit yang dapat dijalankan oleh Xunit.
        public void AddItem_ShouldIncreaseInventoryCount()  // Test untuk memverifikasi penambahan item meningkatkan jumlah inventaris.
        {
            var inventory = new InventoryManager();  // Membuat instance baru dari InventoryManager.
            var item = new Item("Asus ROG", ItemType.Laptop, 5);  // Membuat item baru dengan nama "Asus ROG", tipe Laptop, dan kuantitas 5.

            inventory.AddItem(item);  // Menambahkan item ke dalam inventaris.
            var allInventory = inventory.GetAllInventory();  // Mengambil seluruh inventaris.

            // Memverifikasi bahwa tipe item Laptop ada dalam inventaris.
            Assert.True(allInventory.ContainsKey(ItemType.Laptop));
            // Memverifikasi bahwa hanya ada satu item dengan tipe Laptop.
            Assert.Single(allInventory[ItemType.Laptop]);
            // Memverifikasi bahwa kuantitas item pertama dalam tipe Laptop adalah 5.
            Assert.Equal(5, allInventory[ItemType.Laptop][0].Quantity);
        }

        [Fact]
        public void AddMultipleItems_ShouldGroupByType()  // Test untuk memverifikasi bahwa item dikelompokkan berdasarkan tipe.
        {
            var inventory = new InventoryManager();  // Membuat instance baru dari InventoryManager.

            // Menambahkan beberapa item dengan tipe berbeda.
            inventory.AddItem(new Item("HP 1", ItemType.HP, 2));
            inventory.AddItem(new Item("Laptop 1", ItemType.Laptop, 3));
            inventory.AddItem(new Item("HP 2", ItemType.HP, 1));

            var all = inventory.GetAllInventory();  // Mengambil seluruh inventaris.

            // Memverifikasi bahwa ada dua item dengan tipe HP.
            Assert.Equal(2, all[ItemType.HP].Count);
            // Memverifikasi bahwa hanya ada satu item dengan tipe Laptop.
            Assert.Single(all[ItemType.Laptop]);
        }

        [Fact]
        public void AddSameItem_ShouldIncreaseQuantity()  // Test untuk memverifikasi bahwa menambahkan item yang sama meningkatkan kuantitas.
        {
            var inventory = new InventoryManager();  // Membuat instance baru dari InventoryManager.
            inventory.AddItem(new Item("Lenovo", ItemType.Laptop, 2));  // Menambahkan item Lenovo dengan kuantitas 2.
            inventory.AddItem(new Item("Lenovo", ItemType.Laptop, 3));  // Menambahkan item Lenovo dengan kuantitas 3.

            // Mengambil kuantitas item Lenovo yang pertama.
            var quantity = inventory.GetAllInventory()[ItemType.Laptop][0].Quantity;
            // Memverifikasi bahwa kuantitas item Lenovo adalah 5 (penambahan dari 2 dan 3).
            Assert.Equal(5, quantity);
        }

        [Fact]
        public void WarehouseStateMachine_InitialState_ShouldBeClosed()  // Test untuk memverifikasi status awal WarehouseStateMachine.
        {
            var state = new WarehouseStateMachine();  // Membuat instance baru dari WarehouseStateMachine.
            // Memverifikasi bahwa status awal adalah Closed.
            Assert.Equal(WarehouseState.Closed, state.CurrentState);
        }

        [Fact]
        public void WarehouseStateMachine_Open_ThenClose()  // Test untuk memverifikasi transisi status gudang.
        {
            var state = new WarehouseStateMachine();  // Membuat instance baru dari WarehouseStateMachine.
            state.Open();  // Membuka gudang.
            // Memverifikasi bahwa status gudang menjadi Open.
            Assert.Equal(WarehouseState.Open, state.CurrentState);

            state.Close();  // Menutup gudang.
            // Memverifikasi bahwa status gudang menjadi Closed.
            Assert.Equal(WarehouseState.Closed, state.CurrentState);
        }

        [Fact]
        public void WarehouseStateMachine_CanAddItem_ShouldDependOnState()  // Test untuk memverifikasi apakah item bisa ditambahkan tergantung status gudang.
        {
            var state = new WarehouseStateMachine();  // Membuat instance baru dari WarehouseStateMachine.
            // Memverifikasi bahwa tidak bisa menambah item ketika gudang tertutup.
            Assert.False(state.CanAddItem());

            state.Open();  // Membuka gudang.
            // Memverifikasi bahwa bisa menambah item ketika gudang terbuka.
            Assert.True(state.CanAddItem());
        }
    }
}
