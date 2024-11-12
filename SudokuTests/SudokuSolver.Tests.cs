public class SudokuSolverTests
{
    [Theory]
    [MemberData(nameof(SampleGridToCompare))]
    public void Should_successfully_compare_a_grid_to_itself(int[] gridNumbers) 
    {
        // Arrange
        var grid = getCellGridFromNumbers(gridNumbers);

        // Assert
        assertGridAgainstNumberList(grid, gridNumbers);
    }

    [Theory]
    [MemberData(nameof(IncompleteToSolvedGrids))]
    public void Should_convert_an_incomplete_grid_into_a_solved_grid(int[] incompleteGridNumbers, int[] expectedSolvedGridNumbers)
    {
        // Arrange
        var solver = new SudokuSolver(new SudokuValidator());
        var grid = getCellGridFromNumbers(incompleteGridNumbers);

        // Act
        var solvedGrid = solver.SolveGrid(grid);

        // Assert
        assertGridAgainstNumberList(solvedGrid, expectedSolvedGridNumbers);
    }

    private Cell[,] getCellGridFromNumbers(int[] numbers)
    {
        var grid = new Cell[9, 9];
        for(int x = 0; x < 9; x++) {
            for(int y = 0; y < 9; y++) {
                var nextValue = numbers[x + (y * 9)];
                var newCell = new Cell(x, y) { Value = nextValue };
                if (nextValue != 0) {
                    newCell.Editable = false;
                }
                grid[x, y] = newCell;
            }
        }
        return grid;
    }

    private void assertGridAgainstNumberList(Cell[,] gridToVerify, int[] expectedValues)
    {
        var validator = new SudokuValidator();
        for (int columnIndex = 0; columnIndex < 9; columnIndex++)
        {
            Assert.Collection(validator.GetColumn(gridToVerify, columnIndex), 
                cell => Assert.Equal(expectedValues[columnIndex + 0], cell.Value),
                cell => Assert.Equal(expectedValues[columnIndex + 9], cell.Value),
                cell => Assert.Equal(expectedValues[columnIndex + 18], cell.Value),
                cell => Assert.Equal(expectedValues[columnIndex + 27], cell.Value),
                cell => Assert.Equal(expectedValues[columnIndex + 36], cell.Value),
                cell => Assert.Equal(expectedValues[columnIndex + 45], cell.Value),
                cell => Assert.Equal(expectedValues[columnIndex + 54], cell.Value),
                cell => Assert.Equal(expectedValues[columnIndex + 63], cell.Value),
                cell => Assert.Equal(expectedValues[columnIndex + 72], cell.Value));
        }
    }

    public static IEnumerable<object[]> SampleGridToCompare =>
        new List<object[]>
        {
            new object[] { new int[] {
                2, 4, 8, 3, 9, 5, 7, 1, 6,
                5, 7, 1, 6, 2, 8, 3, 4, 9,
                9, 3, 6, 7, 4, 1, 5, 8, 2,
                6, 8, 2, 5, 3, 9, 1, 7, 4,
                3, 5, 9, 1, 7, 4, 6, 2, 8,
                7, 1, 4, 8, 6, 2, 9, 5, 3,
                8, 6, 3, 4, 1, 7, 2, 9, 5,
                1, 9, 5, 2, 8, 6, 4, 3, 7,
                4, 2, 7, 9, 5, 3, 8, 6, 1,
            }},
        };

    public static IEnumerable<object[]> IncompleteToSolvedGrids =>
        // Example grid and solution from https://sudokukingdom.com/rules.php
        new List<object[]>
        {
            new object[] { new int[] {
                0, 0, 0, 3, 9, 0, 0, 1, 0,
                5, 0, 1, 0, 0, 0, 0, 4, 0,
                9, 0, 0, 7, 0, 0, 5, 0, 0,
                6, 0, 2, 5, 3, 0, 0, 7, 0,
                0, 0, 0, 0, 7, 0, 0, 0, 8,
                7, 0, 0, 8, 0, 0, 9, 0, 3,
                8, 0, 3, 0, 1, 0, 0, 9, 0,
                0, 9, 0, 2, 0, 6, 0, 0, 7,
                4, 0, 0, 0, 0, 3, 0, 6, 1,
            }, new int[] {
                2, 4, 8, 3, 9, 5, 7, 1, 6,
                5, 7, 1, 6, 2, 8, 3, 4, 9,
                9, 3, 6, 7, 4, 1, 5, 8, 2,
                6, 8, 2, 5, 3, 9, 1, 7, 4,
                3, 5, 9, 1, 7, 4, 6, 2, 8,
                7, 1, 4, 8, 6, 2, 9, 5, 3,
                8, 6, 3, 4, 1, 7, 2, 9, 5,
                1, 9, 5, 2, 8, 6, 4, 3, 7,
                4, 2, 7, 9, 5, 3, 8, 6, 1,
            }},
        };
}