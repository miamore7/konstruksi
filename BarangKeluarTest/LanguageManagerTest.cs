using LaptopInventory.Services;
using System;
using Xunit;

namespace BarangKeluarTest
{
    public class LanguageManagerTest : IDisposable
    {
        private const string TestLangFile = "Models/TestLanguages.json";
        private readonly LanguageManager _manager;

        public LanguageManagerTest()
        {
            // Setup test file
            Directory.CreateDirectory("Models");
            File.WriteAllText(TestLangFile,
            @"{
                ""ID"": {
                    ""JudulAplikasi"": ""SISTEM PENCATATAN BARANG KELUAR"",
                    ""Error_IDKosong"": ""ID Barang tidak boleh kosong!""
                },
                ""EN"": {
                    ""JudulAplikasi"": ""ITEM OUTGOING SYSTEM"",
                    ""Error_IDKosong"": ""Item ID cannot be empty!""
                }
            }");

            _manager = new LanguageManager();
        }

        public void Dispose()
        {
            // Cleanup
            if (File.Exists(TestLangFile))
                File.Delete(TestLangFile);
        }

        [Fact]
        public void LoadLanguage_BahasaID_Berhasil()
        {
            // Arrange & Act
            _manager.LoadLanguage("ID", TestLangFile);

            // Assert
            Assert.Equal("SISTEM PENCATATAN BARANG KELUAR", _manager.Get("JudulAplikasi"));
        }

        [Fact]
        public void LoadLanguage_KeyTidakAda_ReturnPlaceholder()
        {
            // Arrange
            _manager.LoadLanguage("EN", TestLangFile);

            // Act & Assert
            Assert.Equal("[MISSING:NonExistentKey]", _manager.Get("NonExistentKey"));
        }

        [Fact]
        public void LoadLanguage_KodeTidakValid_ThrowException()
        {
            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(
                () => _manager.LoadLanguage("ZZ", TestLangFile)
            );
            Assert.Contains("not found", ex.Message);
        }

        [Fact]
        public void Format_MenggunakanPlaceholder_ReturnKalimat()
        {
            // Arrange
            _manager.LoadLanguage("ID", TestLangFile);

            // Act
            var result = _manager.Get("Error_IDKosong");

            // Assert
            Assert.Equal("ID Barang tidak boleh kosong!", result);
        }
    }
}