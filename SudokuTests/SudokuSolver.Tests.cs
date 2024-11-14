using Moq;

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

    [Fact]
    public void Should_get_the_current_cell_from_the_grid_based_on_the_current_column_and_row()
    {
        // Arrange
        var random = new Random();
        var currentCell = new Cell(random.Next(), random.Next()) { Value = random.Next() };
        var currentRow = random.Next(1, 3);
        var currentColumn = random.Next(1, 3);
        var validatorMock = new Mock<SudokuValidator>();
        var solver = new SudokuSolver(validatorMock.Object);
        solver.Grid = new Cell[3, 3];
        solver.Grid[currentColumn, currentRow] = currentCell;
        solver.CurrentColumn = currentColumn;
        solver.CurrentRow = currentRow;

        // Act
        var result = solver.CurrentCell;

        // Assert
        Assert.Equal(currentCell, result);
    }

    [Fact]
    public void Should_reset_the_state_of_the_solver_to_prepare_for_a_new_solve_attempt()
    {
        // Arrange
        var random = new Random();
        var validatorMock = new Mock<SudokuValidator>();
        var solver = new SudokuSolver(validatorMock.Object);
        solver.CurrentColumn = random.Next();
        solver.CurrentRow = random.Next();
        solver.CurrentValue = random.Next();
        solver.SolutionOperations = new Stack<Cell>();
        solver.SolutionOperations.Push(new Cell(0, 0));

        // Act
        solver.ResetSolver();

        // Assert
        Assert.Equal(0, solver.CurrentColumn);
        Assert.Equal(0, solver.CurrentRow);
        Assert.Equal(1, solver.CurrentValue);
        Assert.Empty(solver.SolutionOperations);
    }

    [Theory]
    [InlineData(SudokuValidationState.Complete)]
    [InlineData(SudokuValidationState.Incomplete)]
    public void Should_mark_the_current_cell_and_value_as_a_valid_operation_if_the_validator_returns_a_complete_or_incomplete_result(SudokuValidationState validationResult)
    {
        // Arrange
        var random = new Random();
        var validatorMock = new Mock<SudokuValidator>();
        var solver = new Mock<SudokuSolver>(validatorMock.Object);
        var currentValue = random.Next();
        var currentCell = new Cell(0, 0);
        solver.Object.CurrentValue = currentValue;
        solver.Object.Grid = new Cell[0, 0];
        solver.Setup(x => x.TryOperation()).CallBase();
        solver.Setup(x => x.CurrentCell).Returns(currentCell);
        validatorMock.Setup(x => x.ValidateCellPlacement(It.IsAny<Cell[,]>(), It.IsAny<Cell>()))
            .Returns(validationResult);
        
        // Act
        solver.Object.TryOperation();

        // Assert
        Assert.Equal(currentValue, currentCell.Value);
        validatorMock.Verify(x => x.ValidateCellPlacement(solver.Object.Grid, currentCell), Times.Once());
        solver.Verify(x => x.MarkValidOperation(), Times.Once());
    }

    [Fact]
    public void Should_get_a_new_operation_if_the_validator_returns_an_invalid_result()
    {
        // Arrange
        var random = new Random();
        var validatorMock = new Mock<SudokuValidator>();
        var solver = new Mock<SudokuSolver>(validatorMock.Object);
        var currentValue = random.Next();
        var currentCell = new Cell(0, 0);
        solver.Object.CurrentValue = currentValue;
        solver.Object.Grid = new Cell[0, 0];
        solver.Setup(x => x.TryOperation()).CallBase();
        solver.Setup(x => x.CurrentCell).Returns(currentCell);
        validatorMock.Setup(x => x.ValidateCellPlacement(It.IsAny<Cell[,]>(), It.IsAny<Cell>()))
            .Returns(SudokuValidationState.Invalid);
        
        // Act
        solver.Object.TryOperation();

        // Assert
        Assert.Equal(currentValue, currentCell.Value);
        validatorMock.Verify(x => x.ValidateCellPlacement(solver.Object.Grid, currentCell), Times.Once());
        solver.Verify(x => x.GetNextOperation(), Times.Once());
    }

    [Fact]
    public void Should_advance_to_the_next_column_and_reset_to_row_0_and_value_1_if_the_current_row_is_8()
    {
        // Arrange
        var random = new Random();
        var validatorMock = new Mock<SudokuValidator>();
        var solver = new SudokuSolver(validatorMock.Object);
        var currentColumn = random.Next();
        solver.CurrentColumn = currentColumn;
        solver.CurrentRow = 8;
        solver.CurrentValue = random.Next();

        // Act
        solver.NextCell();

        // Assert
        Assert.Equal(currentColumn + 1, solver.CurrentColumn);
        Assert.Equal(0, solver.CurrentRow);
        Assert.Equal(1, solver.CurrentValue);
    }

    [Fact]
    public void Should_advance_to_the_next_row_and_reset_to_value_1_if_the_current_row_is_before_8()
    {
        // Arrange
        var random = new Random();
        var validatorMock = new Mock<SudokuValidator>();
        var solver = new SudokuSolver(validatorMock.Object);
        var currentColumn = random.Next();
        var currentRow = random.Next(0, 7);
        solver.CurrentColumn = currentColumn;
        solver.CurrentRow = currentRow;
        solver.CurrentValue = random.Next();

        // Act
        solver.NextCell();

        // Assert
        Assert.Equal(currentColumn, solver.CurrentColumn);
        Assert.Equal(currentRow + 1, solver.CurrentRow);
        Assert.Equal(1, solver.CurrentValue);
    }

    [Fact]
    public void Should_add_the_current_cell_to_the_stack_of_potentially_successful_operations_and_get_the_next_cell()
    {
        // Arrange
        var random = new Random();
        var validatorMock = new Mock<SudokuValidator>();
        var solver = new Mock<SudokuSolver>(validatorMock.Object);
        var currentCell = new Cell(0, 0);
        solver.Setup(x => x.MarkValidOperation()).CallBase();
        solver.Setup(x => x.CurrentCell).Returns(currentCell);
        solver.Object.SolutionOperations = new Stack<Cell>();
        
        // Act
        solver.Object.MarkValidOperation();

        // Assert
        Assert.NotEmpty(solver.Object.SolutionOperations);
        Assert.Equal(currentCell, solver.Object.SolutionOperations.Single());
        solver.Verify(x => x.NextCell(), Times.Once());
    }

    [Fact]
    public void Should_increment_the_current_value_if_it_is_less_than_9()
    {
        // Arrange
        var random = new Random();
        var validatorMock = new Mock<SudokuValidator>();
        var solver = new SudokuSolver(validatorMock.Object);
        var previousValue = random.Next(1, 8);
        solver.CurrentValue = previousValue;

        // Act
        solver.GetNextOperation();

        // Assert
        Assert.Equal(previousValue + 1, solver.CurrentValue);
    }

    [Fact]
    public void Should_get_the_latest_operation_from_the_solution_stack_and_retry_it_with_the_next_value_if_the_current_value_is_9()
    {
        // Arrange
        var random = new Random();
        var validatorMock = new Mock<SudokuValidator>();
        var solver = new Mock<SudokuSolver>(validatorMock.Object);
        var currentCell = new Cell(0, 0) { Value = 9 };
        var previousCell = new Cell(random.Next(), random.Next()) { Value = random.Next(1, 8) };
        solver.Setup(x => x.GetNextOperation()).CallBase();
        solver.Setup(x => x.CurrentCell).Returns(currentCell);
        solver.Object.CurrentColumn = random.Next();
        solver.Object.CurrentRow = random.Next();
        solver.Object.SolutionOperations.Push(previousCell);
        solver.Object.CurrentValue = 9;
        
        // Act
        solver.Object.GetNextOperation();

        // Assert
        Assert.Empty(solver.Object.SolutionOperations);
        Assert.Equal(0, currentCell.Value);
        Assert.Equal(previousCell.Column, solver.Object.CurrentColumn);
        Assert.Equal(previousCell.Row, solver.Object.CurrentRow);
        Assert.Equal(previousCell.Value + 1, solver.Object.CurrentValue);
        solver.Verify(x => x.GetNextOperation(), Times.Exactly(2));
    }

    [Fact]
    public void Should_retrieve_operations_from_the_solution_stack_until_we_get_one_with_a_value_below_9()
    {
        // Arrange
        var random = new Random();
        var validatorMock = new Mock<SudokuValidator>();
        var solver = new Mock<SudokuSolver>(validatorMock.Object);
        var currentCell = new Cell(0, 0) { Value = 9 };
        var previousCell1 = new Cell(random.Next(), random.Next()) { Value = 9 };
        var previousCell2 = new Cell(random.Next(), random.Next()) { Value = 9 };
        var validPreviousCell = new Cell(random.Next(), random.Next()) { Value = random.Next(1, 8) };
        solver.Setup(x => x.GetNextOperation()).CallBase();
        solver.Setup(x => x.CurrentCell).Returns(currentCell);
        solver.Object.CurrentColumn = random.Next();
        solver.Object.CurrentRow = random.Next();
        solver.Object.SolutionOperations.Push(validPreviousCell);
        solver.Object.SolutionOperations.Push(previousCell2);
        solver.Object.SolutionOperations.Push(previousCell1);
        solver.Object.CurrentValue = 9;
        
        // Act
        solver.Object.GetNextOperation();

        // Assert
        Assert.Empty(solver.Object.SolutionOperations);
        Assert.Equal(0, currentCell.Value);
        Assert.Equal(validPreviousCell.Column, solver.Object.CurrentColumn);
        Assert.Equal(validPreviousCell.Row, solver.Object.CurrentRow);
        Assert.Equal(validPreviousCell.Value + 1, solver.Object.CurrentValue);
        solver.Verify(x => x.GetNextOperation(), Times.Exactly(4));
    }
}