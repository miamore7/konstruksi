namespace LaptopInventory
{
    public enum WarehouseState  // Enum untuk menggambarkan status gudang (terbuka atau tertutup).
    {
        Closed,  // Status gudang tertutup.
        Open     // Status gudang terbuka.
    }

    public class WarehouseStateMachine  // Kelas yang mengelola status gudang menggunakan state machine.
    {
        public WarehouseState CurrentState { get; private set; }  // Properti untuk menyimpan status gudang saat ini.

        public WarehouseStateMachine()  // Konstruktor yang menginisialisasi status gudang ke "Closed".
        {
            CurrentState = WarehouseState.Closed;
        }

        public void Open()  // Metode untuk membuka gudang dan mengubah status ke "Open".
        {
            CurrentState = WarehouseState.Open;
        }

        public void Close()  // Metode untuk menutup gudang dan mengubah status ke "Closed".
        {
            CurrentState = WarehouseState.Closed;
        }

        public bool CanAddItem()  // Metode untuk memeriksa apakah item bisa ditambahkan berdasarkan status gudang.
        {
            return CurrentState == WarehouseState.Open;  // Mengembalikan true jika gudang terbuka, false jika tertutup.
        }
    }
}
