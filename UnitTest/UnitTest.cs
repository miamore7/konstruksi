using Xunit;
using LaptopInventory;

namespace UnitTest
{
    public class UnitTest
    {
        // InventoryManager Tests
        [Fact]
        public void AddItem_ShouldIncreaseInventoryCount()
        {
            var inventory = new InventoryManager();
            var item = new Item("Asus ROG", ItemType.Laptop, 5);

            inventory.AddItem(item);
            var allInventory = inventory.GetAllInventory();

            Assert.True(allInventory.ContainsKey(ItemType.Laptop));
            Assert.Single(allInventory[ItemType.Laptop]);
            Assert.Equal(5, allInventory[ItemType.Laptop][0].Quantity);
        }

        [Fact]
        public void AddMultipleItems_ShouldGroupByType()
        {
            var inventory = new InventoryManager();

            inventory.AddItem(new Item("HP 1", ItemType.HP, 2));
            inventory.AddItem(new Item("Laptop 1", ItemType.Laptop, 3));
            inventory.AddItem(new Item("HP 2", ItemType.HP, 1));

            var all = inventory.GetAllInventory();

            Assert.Equal(2, all[ItemType.HP].Count);
            Assert.Single(all[ItemType.Laptop]);
        }

        [Fact]
        public void AddSameItem_ShouldIncreaseQuantity()
        {
            var inventory = new InventoryManager();
            inventory.AddItem(new Item("Lenovo", ItemType.Laptop, 2));
            inventory.AddItem(new Item("Lenovo", ItemType.Laptop, 3));

            var quantity = inventory.GetAllInventory()[ItemType.Laptop][0].Quantity;
            Assert.Equal(5, quantity);
        }

        // WarehouseStateMachine Tests
        [Fact]
        public void WarehouseStateMachine_InitialState_ShouldBeClosed()
        {
            var state = new WarehouseStateMachine();
            Assert.Equal(WarehouseState.Closed, state.CurrentState);
        }

        [Fact]
        public void WarehouseStateMachine_Open_ThenClose()
        {
            var state = new WarehouseStateMachine();
            state.Open();
            Assert.Equal(WarehouseState.Open, state.CurrentState);

            state.Close();
            Assert.Equal(WarehouseState.Closed, state.CurrentState);
        }

        [Fact]
        public void WarehouseStateMachine_CanAddItem_ShouldDependOnState()
        {
            var state = new WarehouseStateMachine();
            Assert.False(state.CanAddItem());

            state.Open();
            Assert.True(state.CanAddItem());
        }
    }
}
