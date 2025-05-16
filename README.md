# Fitur Mencatat Barang Keluar

## Teknik Konstruksi
1. **Parameterization/Generics**: Class `BarangKeluarManager<T>` mendukung tipe barang apa pun.
2. **Runtime Configuration**: Aturan barang keluar dibaca dari file JSON.
3. **Design by Contract**: Validasi ketat via exception (lihat `IBarangKeluarService`).

## Cara Menjalankan
1. Update `BarangKeluarConfig.json` sesuai kebutuhan.
2. Jalankan unit test dengan NUnit.
3. Demo di `Program.cs`.
