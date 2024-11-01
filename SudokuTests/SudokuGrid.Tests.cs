using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Sudoku.Pages;

public class SudokuGridTests : IDisposable
{
    TestContext Context;
    Random Random;
    Mock<ISudokuValidator> MockValidator;
    IRenderedComponent<SudokuGrid> GridComponent;
    
    public SudokuGridTests()
    {
        Context = new TestContext();
        Random = new Random();
        MockValidator = new Mock<ISudokuValidator>();
        Context.Services.AddSingleton(MockValidator.Object);
        GridComponent = Context.RenderComponent<SudokuGrid>();
    }

    public void Dispose()
    {
        Context.Dispose();
    }

    [Fact]
    public void Should_focus_the_designated_cell_when_no_change_event_is_provided()
    {
        // Arrange
        var currentColumn = Random.Next(0, 8);
        var currentRow = Random.Next(0, 8);
        var cellToFocus = GridComponent.Instance.Grid[currentColumn, currentRow];
        var focusEvent = new CellFocusEvent
        {
            Cell = cellToFocus,
            ChangeEvent = null,
        };

        // Act
        GridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Equal(cellToFocus, GridComponent.Instance.FocusedCell);
        Assert.True(cellToFocus.Focused);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void Should_focus_the_next_row_when_down_arrow_is_selected(int currentRow)
    {
        // Arrange
        var currentColumn = Random.Next(0, 8);
        var currentCell = GridComponent.Instance.Grid[currentColumn, currentRow];
        var nextCell = GridComponent.Instance.Grid[currentColumn, currentRow + 1];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowDown",
        };

        // Act
        GridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Equal(nextCell, GridComponent.Instance.FocusedCell);
        Assert.True(nextCell.Focused);
    }

    [Fact]
    public void Should_ignore_the_down_arrow_when_row_8_is_selected()
    {
        // Arrange
        var currentColumn = Random.Next(0, 8);
        var currentCell = GridComponent.Instance.Grid[currentColumn, 8];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowDown",
        };

        // Act
        GridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Null(GridComponent.Instance.FocusedCell);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public void Should_focus_the_previous_row_when_up_arrow_is_selected(int currentRow)
    {
        // Arrange
        var currentColumn = Random.Next(0, 8);
        var currentCell = GridComponent.Instance.Grid[currentColumn, currentRow];
        var previousCell = GridComponent.Instance.Grid[currentColumn, currentRow - 1];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowUp",
        };

        // Act
        GridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Equal(previousCell, GridComponent.Instance.FocusedCell);
        Assert.True(previousCell.Focused);
    }
    
    [Fact]
    public void Should_ignore_the_up_arrow_when_row_0_is_selected()
    {
        // Arrange
        var currentColumn = Random.Next(0, 8);
        var currentCell = GridComponent.Instance.Grid[currentColumn, 0];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowUp",
        };

        // Act
        GridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Null(GridComponent.Instance.FocusedCell);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void Should_focus_the_next_column_when_right_arrow_is_selected(int currentColumn)
    {
        // Arrange
        var currentRow = Random.Next(0, 8);
        var currentCell = GridComponent.Instance.Grid[currentColumn, currentRow];
        var nextCell = GridComponent.Instance.Grid[currentColumn + 1, currentRow];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowRight",
        };

        // Act
        GridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Equal(nextCell, GridComponent.Instance.FocusedCell);
        Assert.True(nextCell.Focused);
    }
    
    [Fact]
    public void Should_ignore_the_right_arrow_when_column_8_is_selected()
    {
        // Arrange
        var currentRow = Random.Next(0, 8);
        var currentCell = GridComponent.Instance.Grid[8, currentRow];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowRight",
        };

        // Act
        GridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Null(GridComponent.Instance.FocusedCell);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    public void Should_focus_the_previous_column_when_left_arrow_is_selected(int currentColumn)
    {
        // Arrange
        var currentRow = Random.Next(0, 8);
        var currentCell = GridComponent.Instance.Grid[currentColumn, currentRow];
        var previousCell = GridComponent.Instance.Grid[currentColumn - 1, currentRow];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowLeft",
        };

        // Act
        GridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Equal(previousCell, GridComponent.Instance.FocusedCell);
        Assert.True(previousCell.Focused);
    }
    
    [Fact]
    public void Should_ignore_the_left_arrow_when_column_0_is_selected()
    {
        // Arrange
        var currentRow = Random.Next(0, 8);
        var currentCell = GridComponent.Instance.Grid[0, currentRow];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowLeft",
        };

        // Act
        GridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Null(GridComponent.Instance.FocusedCell);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("5")]
    [InlineData("6")]
    [InlineData("7")]
    [InlineData("8")]
    [InlineData("9")]
    public void Should_update_the_value_of_the_focused_cell_when_a_number_is_entered(string numberAsString)
    {
        // Arrange
        var currentColumn = Random.Next(0, 8);
        var currentRow = Random.Next(0, 8);
        var expectedValue = int.Parse(numberAsString);
        var focusedCell = GridComponent.Instance.Grid[currentColumn, currentRow];
        GridComponent.Instance.FocusedCell = focusedCell;

        // Act
        GridComponent.Instance.NumberEntered(numberAsString);

        // Assert
        Assert.Equal(expectedValue, GridComponent.Instance.FocusedCell.Value);
    }

    [Theory]
    [InlineData("0")]
    [InlineData("10")]
    public void Should_ignore_an_update_if_the_value_is_out_of_range(string numberAsString)
    {
        // Arrange
        var currentColumn = Random.Next(0, 8);
        var currentRow = Random.Next(0, 8);
        var expectedValue = int.Parse(numberAsString);
        var oldValue = Random.Next();
        var focusedCell = GridComponent.Instance.Grid[currentColumn, currentRow];
        focusedCell.Value = oldValue;
        GridComponent.Instance.FocusedCell = focusedCell;

        // Act
        GridComponent.Instance.NumberEntered(numberAsString);

        // Assert
        Assert.Equal(oldValue, GridComponent.Instance.FocusedCell.Value);
    }

    [Fact]
    public void Should_delete_the_value_of_the_focused_cell()
    {
        // Arrange
        var currentColumn = Random.Next(0, 8);
        var currentRow = Random.Next(0, 8);
        var oldValue = Random.Next();
        var focusedCell = GridComponent.Instance.Grid[currentColumn, currentRow];
        focusedCell.Value = oldValue;
        GridComponent.Instance.FocusedCell = focusedCell;

        // Act
        GridComponent.Instance.Delete();

        // Assert
        Assert.Null(GridComponent.Instance.FocusedCell.Value);
    }

    [Fact]
    public void Should_mark_the_grid_as_finished_if_the_validator_returns_true()
    {
        // Arrange
        MockValidator.Setup(x => x.IsValidSudokuGrid(It.IsAny<Cell[,]>()))
            .Returns(true);
        GridComponent.Instance.IsFinished = false;

        // Act
        GridComponent.Instance.CheckFinished();

        // Assert
        Assert.True(GridComponent.Instance.IsFinished);
    }

    [Fact]
    public void Should_not_mark_the_grid_as_finished_if_the_validator_returns_false()
    {
        // Arrange
        MockValidator.Setup(x => x.IsValidSudokuGrid(It.IsAny<Cell[,]>()))
            .Returns(false);
        GridComponent.Instance.IsFinished = false;

        // Act
        GridComponent.Instance.CheckFinished();

        // Assert
        Assert.False(GridComponent.Instance.IsFinished);
    }
}