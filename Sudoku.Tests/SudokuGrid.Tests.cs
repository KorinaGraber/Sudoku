using Bunit;
using Sudoku.Components.Pages;

public class SudokuGridTests
{
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
        using var context = new TestContext();
        var random = new Random();
        var gridComponent = context.RenderComponent<SudokuGrid>();
        var currentColumn = random.Next(0, 8);
        var currentCell = gridComponent.Instance.Grid[currentColumn, currentRow];
        var nextCell = gridComponent.Instance.Grid[currentColumn, currentRow + 1];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowDown",
        };

        // Act
        gridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Equal(nextCell, gridComponent.Instance.FocusedCell);
        Assert.True(nextCell.Focused);
    }

    [Fact]
    public void Should_ignore_the_down_arrow_when_row_8_is_selected()
    {
        // Arrange
        using var context = new TestContext();
        var random = new Random();
        var gridComponent = context.RenderComponent<SudokuGrid>();
        var currentColumn = random.Next(0, 8);
        var currentCell = gridComponent.Instance.Grid[currentColumn, 8];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowDown",
        };

        // Act
        gridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Null(gridComponent.Instance.FocusedCell);
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
        using var context = new TestContext();
        var random = new Random();
        var gridComponent = context.RenderComponent<SudokuGrid>();
        var currentColumn = random.Next(0, 8);
        var currentCell = gridComponent.Instance.Grid[currentColumn, currentRow];
        var previousCell = gridComponent.Instance.Grid[currentColumn, currentRow - 1];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowUp",
        };

        // Act
        gridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Equal(previousCell, gridComponent.Instance.FocusedCell);
        Assert.True(previousCell.Focused);
    }
    
    [Fact]
    public void Should_ignore_the_up_arrow_when_row_0_is_selected()
    {
        // Arrange
        using var context = new TestContext();
        var random = new Random();
        var gridComponent = context.RenderComponent<SudokuGrid>();
        var currentColumn = random.Next(0, 8);
        var currentCell = gridComponent.Instance.Grid[currentColumn, 0];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowUp",
        };

        // Act
        gridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Null(gridComponent.Instance.FocusedCell);
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
        using var context = new TestContext();
        var random = new Random();
        var gridComponent = context.RenderComponent<SudokuGrid>();
        var currentRow = random.Next(0, 8);
        var currentCell = gridComponent.Instance.Grid[currentColumn, currentRow];
        var nextCell = gridComponent.Instance.Grid[currentColumn + 1, currentRow];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowRight",
        };

        // Act
        gridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Equal(nextCell, gridComponent.Instance.FocusedCell);
        Assert.True(nextCell.Focused);
    }
    
    [Fact]
    public void Should_ignore_the_right_arrow_when_column_8_is_selected()
    {
        // Arrange
        using var context = new TestContext();
        var random = new Random();
        var gridComponent = context.RenderComponent<SudokuGrid>();
        var currentRow = random.Next(0, 8);
        var currentCell = gridComponent.Instance.Grid[8, currentRow];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowRight",
        };

        // Act
        gridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Null(gridComponent.Instance.FocusedCell);
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
        using var context = new TestContext();
        var random = new Random();
        var gridComponent = context.RenderComponent<SudokuGrid>();
        var currentRow = random.Next(0, 8);
        var currentCell = gridComponent.Instance.Grid[currentColumn, currentRow];
        var previousCell = gridComponent.Instance.Grid[currentColumn - 1, currentRow];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowLeft",
        };

        // Act
        gridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Equal(previousCell, gridComponent.Instance.FocusedCell);
        Assert.True(previousCell.Focused);
    }
    
    [Fact]
    public void Should_ignore_the_left_arrow_when_column_0_is_selected()
    {
        // Arrange
        using var context = new TestContext();
        var random = new Random();
        var gridComponent = context.RenderComponent<SudokuGrid>();
        var currentRow = random.Next(0, 8);
        var currentCell = gridComponent.Instance.Grid[0, currentRow];
        var focusEvent = new CellFocusEvent
        {
            Cell = currentCell,
            ChangeEvent = "ArrowLeft",
        };

        // Act
        gridComponent.Instance.ChangeFocus(focusEvent);

        // Assert
        Assert.Null(gridComponent.Instance.FocusedCell);
    }
}