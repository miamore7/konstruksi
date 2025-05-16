using System.Collections.Generic;
using Xunit;

public class TableDrivenTests
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
}
