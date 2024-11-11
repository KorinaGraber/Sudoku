public class CellTests
{
    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(2, 2, 0)]
    [InlineData(3, 0, 1)]
    [InlineData(6, 0, 2)]
    [InlineData(1, 4, 3)]
    [InlineData(5, 5, 4)]
    [InlineData(7, 4, 5)]
    [InlineData(0, 8, 6)]
    [InlineData(5, 7, 7)]
    [InlineData(8, 8, 8)]
    public void Should_get_the_block_the_cell_is_in(int column, int row, int expectedBlock)
    {
        // Arrange
        var cell = new Cell(column, row);

        // Act
        var result = cell.Block;

        // Assert
        Assert.Equal(expectedBlock, result);
    }
}