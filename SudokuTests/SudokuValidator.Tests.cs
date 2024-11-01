public class SudokuValidatorTests
{
    [Theory]
    [MemberData(nameof(ValidSudokuGrids))]
    public void Should_return_true_for_a_valid_sudoku_grid(int[] gridNumbers)
    {
        // Arrange
        var validator = new SudokuValidator();
        var grid = getCellGridFromNumbers(gridNumbers);

        // Act
        var result = validator.IsValidSudokuGrid(grid);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [MemberData(nameof(InvalidSudokuGrids))]
    public void Should_return_false_for_an_invalid_sudoku_grid(int[] gridNumbers)
    {
        // Arrange
        var validator = new SudokuValidator();
        var grid = getCellGridFromNumbers(gridNumbers);

        // Act
        var result = validator.IsValidSudokuGrid(grid);

        // Assert
        Assert.False(result);
    }

    [Theory]
    [MemberData(nameof(IncompleteSudokuGrids))]
    public void Should_return_false_for_an_incomplete_sudoku_grid(int[] gridNumbers)
    {
        // Arrange
        var validator = new SudokuValidator();
        var grid = getCellGridFromNumbers(gridNumbers);

        // Act
        var result = validator.IsValidSudokuGrid(grid);

        // Assert
        Assert.False(result);
    }

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

    [Theory]
    [MemberData(nameof(GetColumnFromGridData))]
    public void Should_return_the_specified_column_from_the_grid(int[] gridNumbers, int columnIndex, int[] expectedColumnValues)
    {
        // Arrange
        var validator = new SudokuValidator();
        var grid = getCellGridFromNumbers(gridNumbers);

        // Act
        var returnedColumn = validator.GetColumn(grid, columnIndex);

        // Assert
        Assert.Collection(returnedColumn, 
            cell => Assert.Equal(expectedColumnValues[0], cell.Value),
            cell => Assert.Equal(expectedColumnValues[1], cell.Value),
            cell => Assert.Equal(expectedColumnValues[2], cell.Value),
            cell => Assert.Equal(expectedColumnValues[3], cell.Value),
            cell => Assert.Equal(expectedColumnValues[4], cell.Value),
            cell => Assert.Equal(expectedColumnValues[5], cell.Value),
            cell => Assert.Equal(expectedColumnValues[6], cell.Value),
            cell => Assert.Equal(expectedColumnValues[7], cell.Value),
            cell => Assert.Equal(expectedColumnValues[8], cell.Value));
    }

    [Theory]
    [MemberData(nameof(GetRowFromGridData))]
    public void Should_return_the_specified_row_from_the_grid(int[] gridNumbers, int rowIndex, int[] expectedRowValues)
    {
        // Arrange
        var validator = new SudokuValidator();
        var grid = getCellGridFromNumbers(gridNumbers);

        // Act
        var returnedRow = validator.GetRow(grid, rowIndex);

        // Assert
        Assert.Collection(returnedRow, 
            cell => Assert.Equal(expectedRowValues[0], cell.Value),
            cell => Assert.Equal(expectedRowValues[1], cell.Value),
            cell => Assert.Equal(expectedRowValues[2], cell.Value),
            cell => Assert.Equal(expectedRowValues[3], cell.Value),
            cell => Assert.Equal(expectedRowValues[4], cell.Value),
            cell => Assert.Equal(expectedRowValues[5], cell.Value),
            cell => Assert.Equal(expectedRowValues[6], cell.Value),
            cell => Assert.Equal(expectedRowValues[7], cell.Value),
            cell => Assert.Equal(expectedRowValues[8], cell.Value));
    }

    [Theory]
    [MemberData(nameof(GetBlockFromGridData))]
    public void Should_return_the_specified_block_from_the_grid(int[] gridNumbers, int blockIndex, int[] expectedBlockValues)
    {
        // Arrange
        var validator = new SudokuValidator();
        var grid = getCellGridFromNumbers(gridNumbers);

        // Act
        var returnedBlock = validator.GetBlock(grid, blockIndex);

        // Assert
        Assert.Collection(returnedBlock, 
            cell => Assert.Equal(expectedBlockValues[0], cell.Value),
            cell => Assert.Equal(expectedBlockValues[1], cell.Value),
            cell => Assert.Equal(expectedBlockValues[2], cell.Value),
            cell => Assert.Equal(expectedBlockValues[3], cell.Value),
            cell => Assert.Equal(expectedBlockValues[4], cell.Value),
            cell => Assert.Equal(expectedBlockValues[5], cell.Value),
            cell => Assert.Equal(expectedBlockValues[6], cell.Value),
            cell => Assert.Equal(expectedBlockValues[7], cell.Value),
            cell => Assert.Equal(expectedBlockValues[8], cell.Value));
    }

    private Cell[] getCellListFromNumbers(int[] numbers)
    {
        var random = new Random();
        var cells = new Cell[9];
        for(int index = 0; index < 9; index++) {
            cells[index] = new Cell(random.Next(0, 8), random.Next(0, 8)) { Value = numbers[index] };
        }
        return cells;
    }

    private Cell[,] getCellGridFromNumbers(int[] numbers)
    {
        var grid = new Cell[9, 9];
        for(int x = 0; x < 9; x++) {
            for(int y = 0; y < 9; y++) {
                grid[x, y] = new Cell(x, y) { Value = numbers[x + (y * 9)] };
            }
        }
        return grid;
    }

    public static IEnumerable<object[]> ValidSudokuGrids =>
        new List<object[]>
        {
            new object[] { new int[] {
                6, 3, 9, 5, 7, 4, 1, 8, 2,
                5, 4, 1, 8, 2, 9, 3, 7, 6,
                7, 8, 2, 6, 1, 3, 9, 5, 4,
                1, 9, 8, 4, 6, 7, 5, 2, 3,
                3, 6, 5, 9, 8, 2, 4, 1, 7,
                4, 2, 7, 1, 3, 5, 8, 6, 9,
                9, 5, 6, 7, 4, 8, 2, 3, 1,
                8, 1, 3, 2, 9, 6, 7, 4, 5,
                2, 7, 4, 3, 5, 1, 6, 9, 8,
            }},
            new object[] { new int[] {
                7, 2, 6, 3, 5, 9, 4, 1, 8,
                4, 5, 8, 1, 6, 7, 2, 3, 9,
                9, 1, 3, 8, 2, 4, 7, 6, 5,
                1, 6, 2, 9, 7, 5, 3, 8, 4,
                3, 9, 4, 2, 8, 6, 1, 5, 7,
                8, 7, 5, 4, 1, 3, 9, 2, 6,
                5, 3, 7, 6, 4, 1, 8, 9, 2,
                6, 8, 9, 7, 3, 2, 5, 4, 1,
                2, 4, 1, 5, 9, 8, 6, 7, 3,
            }},
        };

    public static IEnumerable<object[]> InvalidSudokuGrids =>
        new List<object[]>
        {
            new object[] { new int[] {
                6, 3, 9, 5, 7, 4, 1, 6, 2,
                5, 4, 1, 8, 2, 9, 3, 7, 6,
                7, 8, 2, 6, 1, 3, 9, 5, 4,
                1, 9, 8, 4, 6, 7, 5, 2, 3,
                3, 6, 5, 6, 8, 2, 4, 1, 7,
                4, 2, 7, 1, 3, 5, 8, 6, 9,
                9, 5, 6, 7, 4, 8, 2, 3, 1,
                8, 1, 3, 2, 6, 6, 7, 4, 5,
                2, 7, 4, 3, 5, 1, 6, 9, 8,
            }},
            new object[] { new int[] {
                7, 2, 3, 3, 5, 9, 4, 1, 8,
                4, 5, 8, 1, 6, 7, 2, 3, 9,
                9, 1, 3, 8, 2, 4, 7, 6, 5,
                1, 6, 2, 9, 7, 5, 3, 8, 4,
                3, 9, 4, 2, 3, 6, 1, 5, 7,
                8, 7, 3, 4, 1, 3, 9, 2, 6,
                5, 3, 7, 6, 4, 1, 8, 9, 2,
                6, 8, 9, 7, 3, 2, 3, 4, 1,
                2, 4, 1, 5, 9, 8, 6, 7, 3,
            }},
            new object[] { new int[] {
                6, 3, 9, 5, 7, 4, 1, 8, 2,
                5, 4, 1, 8, 2, 9, 3, 7, 6,
                1, 9, 8, 4, 6, 7, 5, 2, 3,
                7, 8, 2, 6, 1, 3, 9, 5, 4,
                3, 6, 5, 9, 8, 2, 4, 1, 7,
                4, 2, 7, 1, 3, 5, 8, 6, 9,
                9, 5, 6, 7, 4, 8, 2, 3, 1,
                8, 1, 3, 2, 9, 6, 7, 4, 5,
                2, 7, 4, 3, 5, 1, 6, 9, 8,
            }},
        };

    public static IEnumerable<object[]> IncompleteSudokuGrids =>
        new List<object[]>
        {
            new object[] { new int[] {
                6, 3, 9, 5, 7, 4, 1, 6, 2,
                5, 4, 0, 8, 2, 9, 3, 7, 6,
                7, 8, 2, 6, 1, 3, 9, 0, 4,
                1, 9, 8, 4, 6, 7, 0, 2, 3,
                3, 6, 5, 6, 8, 2, 4, 1, 7,
                4, 2, 7, 1, 3, 5, 8, 6, 9,
                9, 5, 6, 7, 4, 8, 2, 3, 1,
                8, 1, 3, 2, 6, 0, 7, 4, 5,
                2, 7, 4, 3, 5, 1, 6, 9, 8,
            }},
            new object[] { new int[] {
                7, 2, 0, 3, 5, 9, 4, 1, 8,
                4, 5, 8, 1, 6, 7, 2, 3, 9,
                9, 1, 3, 8, 2, 4, 7, 6, 5,
                1, 6, 2, 9, 7, 5, 3, 8, 4,
                3, 9, 4, 2, 3, 6, 1, 5, 7,
                8, 7, 3, 4, 1, 3, 9, 2, 6,
                5, 3, 7, 6, 4, 1, 8, 9, 2,
                6, 8, 9, 7, 3, 2, 3, 4, 1,
                2, 4, 1, 5, 9, 8, 6, 7, 3,
            }},
        };

    public static IEnumerable<object[]> GetColumnFromGridData =>
        new List<object[]>
        {
            new object[] { new int[] {
                6, 3, 9, 5, 7, 4, 1, 8, 2,
                5, 4, 1, 8, 2, 9, 3, 7, 6,
                7, 8, 2, 6, 1, 3, 9, 5, 4,
                1, 9, 8, 4, 6, 7, 5, 2, 3,
                3, 6, 5, 9, 8, 2, 4, 1, 7,
                4, 2, 7, 1, 3, 5, 8, 6, 9,
                9, 5, 6, 7, 4, 8, 2, 3, 1,
                8, 1, 3, 2, 9, 6, 7, 4, 5,
                2, 7, 4, 3, 5, 1, 6, 9, 8,
            }, 0, new int[] {
                6, 5, 7, 1, 3, 4, 9, 8, 2,
            }},
            new object[] { new int[] {
                6, 3, 9, 5, 7, 4, 1, 8, 2,
                5, 4, 1, 8, 2, 9, 3, 7, 6,
                7, 8, 2, 6, 1, 3, 9, 5, 4,
                1, 9, 8, 4, 6, 7, 5, 2, 3,
                3, 6, 5, 9, 8, 2, 4, 1, 7,
                4, 2, 7, 1, 3, 5, 8, 6, 9,
                9, 5, 6, 7, 4, 8, 2, 3, 1,
                8, 1, 3, 2, 9, 6, 7, 4, 5,
                2, 7, 4, 3, 5, 1, 6, 9, 8,
            }, 4, new int[] {
                7, 2, 1, 6, 8, 3, 4, 9, 5,
            }},
        };

    public static IEnumerable<object[]> GetRowFromGridData =>
        new List<object[]>
        {
            new object[] { new int[] {
                6, 3, 9, 5, 7, 4, 1, 8, 2,
                5, 4, 1, 8, 2, 9, 3, 7, 6,
                7, 8, 2, 6, 1, 3, 9, 5, 4,
                1, 9, 8, 4, 6, 7, 5, 2, 3,
                3, 6, 5, 9, 8, 2, 4, 1, 7,
                4, 2, 7, 1, 3, 5, 8, 6, 9,
                9, 5, 6, 7, 4, 8, 2, 3, 1,
                8, 1, 3, 2, 9, 6, 7, 4, 5,
                2, 7, 4, 3, 5, 1, 6, 9, 8,
            }, 0, new int[] {
                6, 3, 9, 5, 7, 4, 1, 8, 2,
            }},
            new object[] { new int[] {
                6, 3, 9, 5, 7, 4, 1, 8, 2,
                5, 4, 1, 8, 2, 9, 3, 7, 6,
                7, 8, 2, 6, 1, 3, 9, 5, 4,
                1, 9, 8, 4, 6, 7, 5, 2, 3,
                3, 6, 5, 9, 8, 2, 4, 1, 7,
                4, 2, 7, 1, 3, 5, 8, 6, 9,
                9, 5, 6, 7, 4, 8, 2, 3, 1,
                8, 1, 3, 2, 9, 6, 7, 4, 5,
                2, 7, 4, 3, 5, 1, 6, 9, 8,
            }, 4, new int[] {
                3, 6, 5, 9, 8, 2, 4, 1, 7,
            }},
        };

    public static IEnumerable<object[]> GetBlockFromGridData =>
        new List<object[]>
        {
            new object[] { new int[] {
                6, 3, 9, 5, 7, 4, 1, 8, 2,
                5, 4, 1, 8, 2, 9, 3, 7, 6,
                7, 8, 2, 6, 1, 3, 9, 5, 4,
                1, 9, 8, 4, 6, 7, 5, 2, 3,
                3, 6, 5, 9, 8, 2, 4, 1, 7,
                4, 2, 7, 1, 3, 5, 8, 6, 9,
                9, 5, 6, 7, 4, 8, 2, 3, 1,
                8, 1, 3, 2, 9, 6, 7, 4, 5,
                2, 7, 4, 3, 5, 1, 6, 9, 8,
            }, 0, new int[] {
                6, 3, 9, 5, 4, 1, 7, 8, 2,
            }},
            new object[] { new int[] {
                6, 3, 9, 5, 7, 4, 1, 8, 2,
                5, 4, 1, 8, 2, 9, 3, 7, 6,
                7, 8, 2, 6, 1, 3, 9, 5, 4,
                1, 9, 8, 4, 6, 7, 5, 2, 3,
                3, 6, 5, 9, 8, 2, 4, 1, 7,
                4, 2, 7, 1, 3, 5, 8, 6, 9,
                9, 5, 6, 7, 4, 8, 2, 3, 1,
                8, 1, 3, 2, 9, 6, 7, 4, 5,
                2, 7, 4, 3, 5, 1, 6, 9, 8,
            }, 4, new int[] {
                4, 6, 7, 9, 8, 2, 1, 3, 5,
            }},
            new object[] { new int[] {
                6, 3, 9, 5, 7, 4, 1, 8, 2,
                5, 4, 1, 8, 2, 9, 3, 7, 6,
                7, 8, 2, 6, 1, 3, 9, 5, 4,
                1, 9, 8, 4, 6, 7, 5, 2, 3,
                3, 6, 5, 9, 8, 2, 4, 1, 7,
                4, 2, 7, 1, 3, 5, 8, 6, 9,
                9, 5, 6, 7, 4, 8, 2, 3, 1,
                8, 1, 3, 2, 9, 6, 7, 4, 5,
                2, 7, 4, 3, 5, 1, 6, 9, 8,
            }, 8, new int[] {
                2, 3, 1, 7, 4, 5, 6, 9, 8,
            }},
        };
}