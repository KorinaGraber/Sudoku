using Bunit;
using Sudoku.Pages;

public class SudokuValidatorTests
{
    [Theory]
    [InlineData(new[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
    [InlineData(new[]{ 3, 8, 9, 4, 2, 7, 6, 5, 1 })]
    [InlineData(new[]{ 7, 3, 4, 2, 5, 6, 1, 8, 9 })]
    [InlineData(new[]{ 6, 5, 9, 2, 7, 4, 1, 8, 3 })]
    [InlineData(new[]{ 2, 4, 7, 5, 8, 6, 3, 1, 9 })]
    [InlineData(new[]{ 5, 3, 8, 9, 6, 4, 2, 1, 7 })]
    [InlineData(new[]{ 4, 9, 5, 7, 2, 8, 3, 6, 1 })]
    [InlineData(new[]{ 4, 3, 9, 6, 1, 5, 8, 2, 7 })]
    [InlineData(new[]{ 9, 8, 7, 6, 5, 4, 3, 2, 1 })]
    public void Should_return_true_if_each_number_is_represented_once(int[] numbers)
    {
        // Arrange
        var validator = new SudokuValidator();
        var cells = getCellListFromNumbers(numbers);

        // Act
        var result = validator.IsValidCellGroup(cells);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(new[]{ 1, 1, 3, 4, 5, 6, 7, 8, 9 })]
    [InlineData(new[]{ 2, 2, 3, 4, 5, 6, 7, 8, 9 })]
    [InlineData(new[]{ 1, 2, 3, 4, 5, 3, 7, 8, 9 })]
    [InlineData(new[]{ 4, 2, 3, 4, 5, 6, 7, 8, 9 })]
    [InlineData(new[]{ 1, 2, 5, 4, 5, 6, 7, 8, 9 })]
    [InlineData(new[]{ 1, 2, 3, 4, 5, 6, 7, 6, 9 })]
    [InlineData(new[]{ 1, 7, 3, 4, 5, 6, 7, 8, 9 })]
    [InlineData(new[]{ 1, 2, 3, 4, 5, 6, 8, 8, 9 })]
    [InlineData(new[]{ 1, 2, 9, 4, 5, 6, 7, 8, 9 })]
    [InlineData(new[]{ 9, 2, 9, 4, 9, 6, 7, 8, 9 })]
    [InlineData(new[]{ 9, 2, 8, 4, 9, 6, 3, 8, 3 })]
    [InlineData(new[]{ 9, 1, 9, 4, 1, 6, 1, 8, 9 })]
    [InlineData(new[]{ 9, 2, 5, 4, 9, 6, 5, 8, 9 })]
    [InlineData(new[]{ 9, 8, 5, 4, 9, 8, 5, 8, 9 })]
    [InlineData(new[]{ 9, 4, 5, 4, 9, 4, 5, 8, 9 })]
    [InlineData(new[]{ 9, 3, 5, 4, 9, 6, 3, 8, 9 })]
    public void Should_return_false_if_any_number_is_represented_multiple_times(int[] numbers)
    {
        // Arrange
        var random = new Random();
        var validator = new SudokuValidator();
        var cells = new[]
        {
            new Cell(random.Next(0, 8), random.Next(0, 8)) { Value = numbers[0]},
            new Cell(random.Next(0, 8), random.Next(0, 8)) { Value = numbers[1]},
            new Cell(random.Next(0, 8), random.Next(0, 8)) { Value = numbers[2]},
            new Cell(random.Next(0, 8), random.Next(0, 8)) { Value = numbers[3]},
            new Cell(random.Next(0, 8), random.Next(0, 8)) { Value = numbers[4]},
            new Cell(random.Next(0, 8), random.Next(0, 8)) { Value = numbers[5]},
            new Cell(random.Next(0, 8), random.Next(0, 8)) { Value = numbers[6]},
            new Cell(random.Next(0, 8), random.Next(0, 8)) { Value = numbers[7]},
            new Cell(random.Next(0, 8), random.Next(0, 8)) { Value = numbers[8]},
        };

        // Act
        var result = validator.IsValidCellGroup(cells);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [InlineData(new[]{ 1, 2, 0, 4, 5, 6, 7, 8, 9 })]
    [InlineData(new[]{ 3, 8, 9, 4, 2, 0, 6, 5, 1 })]
    [InlineData(new[]{ 7, 3, 4, 0, 5, 6, 1, 8, 9 })]
    [InlineData(new[]{ 6, 5, 8, 2, 0, 4, 1, 8, 3 })]
    [InlineData(new[]{ 2, 4, 7, 5, 8, 6, 3, 0, 9 })]
    [InlineData(new[]{ 5, 0, 8, 9, 6, 4, 2, 1, 7 })]
    [InlineData(new[]{ 4, 9, 5, 0, 2, 8, 3, 6, 1 })]
    [InlineData(new[]{ 4, 0, 9, 6, 1, 5, 8, 2, 7 })]
    [InlineData(new[]{ 9, 8, 7, 0, 5, 4, 3, 2, 1 })]
    [InlineData(new[]{ 9, 8, 0, 6, 5, 4, 3, 0, 1 })]
    [InlineData(new[]{ 9, 0, 7, 6, 0, 4, 3, 0, 1 })]
    [InlineData(new[]{ 0, 8, 7, 6, 5, 0, 0, 2, 1 })]
    [InlineData(new[]{ 0, 0, 0, 6, 5, 4, 3, 2, 1 })]
    public void Should_return_false_if_any_number_is_missing(int[] numbers)
    {
        // Arrange
        var validator = new SudokuValidator();
        var cells = getCellListFromNumbers(numbers);

        // Act
        var result = validator.IsValidCellGroup(cells);

        // Assert
        Assert.False(result);
    }

    private Cell[] getCellListFromNumbers(int[] numbers)
    {
        var random = new Random();
        var cells = new Cell[9];
        for(int index = 0; index < numbers.Count(); index++) {
            cells[index] = new Cell(random.Next(0, 8), random.Next(0, 8)) { Value = numbers[index] };
        }
        return cells;
    }
}