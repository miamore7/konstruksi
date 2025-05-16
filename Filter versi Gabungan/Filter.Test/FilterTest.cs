using Filter_versi_Gabungan;
using System.Diagnostics;

namespace Filter.Test
{
    public class FilterTest
    {
        [Fact]
        public void Sort_By_Jumlah_Descending_Should_Return_Correct_Order()
        {
            var data = new List<Barang>
        {
            new Barang { Nama = "A", Jumlah = 1 },
            new Barang { Nama = "B", Jumlah = 3 },
            new Barang { Nama = "C", Jumlah = 2 }
        };

            var result = TableDrivenFilterSorter.Apply(data, "Jumlah", ascending: false);

            Assert.Equal("B", result[0].Nama);
            Assert.Equal("C", result[1].Nama);
            Assert.Equal("A", result[2].Nama);
        }
        [Fact]
        public void Filter_By_Kategori_Should_Return_Only_HP()
        {
            var data = new List<Barang>
        {
            new Barang { Nama = "X", Kategori = "HP" },
            new Barang { Nama = "Y", Kategori = "Laptop" }
        };

            var result = GenericFilterSorter.FilterBy(data, b => b.Kategori == "HP");

            Assert.Single(result);
            Assert.Equal("HP", result[0].Kategori);
        }

        [Fact]
        public void Sort_By_Nama_Ascending_Should_Work()
        {
            var data = new List<Barang>
        {
            new Barang { Nama = "Z" },
            new Barang { Nama = "A" }
        };

            var result = GenericFilterSorter.SortBy(data, b => b.Nama);

            Assert.Equal("A", result[0].Nama);
            Assert.Equal("Z", result[1].Nama);
        }
        [Fact]
        public void Runtime_Filter_Laptop_And_Sort_By_Jumlah()
        {
            var data = new List<Barang>
        {
            new Barang { Nama = "HP1", Kategori = "HP", Jumlah = 1 },
            new Barang { Nama = "Laptop1", Kategori = "Laptop", Jumlah = 2 },
            new Barang { Nama = "Laptop2", Kategori = "Laptop", Jumlah = 1 }
        };

            var config = new FilterSortConfig
            {
                Kategori = "Laptop",
                SortBy = "Jumlah",
                Ascending = true
            };

            var result = RuntimeConfiguredProcessor.Apply(data, config);

            Assert.Equal(2, result.Count);
            Assert.Equal("Laptop2", result[0].Nama);
            Assert.Equal("Laptop1", result[1].Nama);
        }
        public class FilterSort_PerformanceTests
        {
            private List<Barang> GenerateBarang(int count)
            {
                var list = new List<Barang>();
                for (int i = 0; i < count; i++)
                {
                    list.Add(new Barang
                    {
                        Nama = $"Barang{i}",
                        Kategori = i % 2 == 0 ? "Laptop" : "HP",
                        Jumlah = i % 100
                    });
                }
                return list;
            }

            [Fact]
            public void RuntimeConfiguredProcessor_PerformUnder2Seconds()
            {
                var data = GenerateBarang(10000);

                var config = new FilterSortConfig
                {
                    Kategori = "Laptop",
                    SortBy = "Jumlah",
                    Ascending = true
                };

                Stopwatch sw = Stopwatch.StartNew();

                var result = RuntimeConfiguredProcessor.Apply(data, config);

                sw.Stop();

                // Perform under 2 seconds
                Assert.True(sw.Elapsed.TotalSeconds < 2,
                    $"Runtime terlalu lambat: {sw.Elapsed.TotalSeconds} detik");

                // Result tidak kosong dan valid
                Assert.All(result, r => Assert.Equal("Laptop", r.Kategori));
            }
        }
    }
}
