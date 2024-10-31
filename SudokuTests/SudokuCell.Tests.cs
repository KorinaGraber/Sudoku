using Bunit;
using Sudoku.Pages;

public class SudokuCellTests
{
    [Theory]
    [InlineData("1", 1)]
    [InlineData("2", 2)]
    [InlineData("3", 3)]
    [InlineData("4", 4)]
    [InlineData("5", 5)]
    [InlineData("6", 6)]
    [InlineData("7", 7)]
    [InlineData("8", 8)]
    [InlineData("9", 9)]
    public void Should_set_the_cell_value_and_invoke_a_change_event_on_a_number_key(string numberEntry, int numberValue)
    {
        // Arrange
        using var context = new TestContext();
        int? valueOnParent = null;
        var cellComponent = context.RenderComponent<SudokuCell>(parameters => parameters
            .Add(p => p.Cell, new Cell(0, 0))
            .Add(p => p.SetValue, x => valueOnParent = x));

        // Act
        cellComponent.Find(".cell").KeyDown(numberEntry);

        // Assert
        Assert.Equal(numberValue, cellComponent.Instance.Cell.Value);
        Assert.Equal(numberValue, valueOnParent);
    }

    [Theory]
    [InlineData("Delete")]
    [InlineData("Backspace")]
    public void Should_clear_the_cell_on_a_delete_or_backspace_keypress(string keypress)
    {
        // Arrange
        using var context = new TestContext();
        var cellComponent = context.RenderComponent<SudokuCell>(parameters => parameters
            .Add(p => p.Cell, new Cell(0, 0)));
        var cellElement = cellComponent.Find(".cell");

        // Act
        cellComponent.Find(".cell").KeyDown(keypress);

        // Assert
        var cellText = cellElement.TextContent;
        cellText.MarkupMatches("");
        Assert.Null(cellComponent.Instance.Cell.Value);
    }

    [Theory]
    [InlineData("ArrowUp")]
    [InlineData("ArrowDown")]
    [InlineData("ArrowLeft")]
    [InlineData("ArrowRight")]
    public void Should_send_a_change_focus_event_to_the_parent_on_an_arrow_key(string arrowKey)
    {
        // Arrange
        using var context = new TestContext();
        var cellOnParent = new Cell(0, 0);
        var changeEventOnParent = "";
        var cellComponent = context.RenderComponent<SudokuCell>(parameters => parameters
            .Add(p => p.Cell, new Cell(0, 0))
            .Add(p => p.ChangeFocus, focusEvent => {
                cellOnParent = focusEvent.Cell;
                changeEventOnParent = focusEvent.ChangeEvent;
            }));

        // Act
        cellComponent.Find(".cell").KeyDown(arrowKey);

        // Assert
        Assert.Equal(cellComponent.Instance.Cell, cellOnParent);
        Assert.Equal(arrowKey, changeEventOnParent);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 3)]
    [InlineData(0, 6)]
    [InlineData(3, 0)]
    [InlineData(3, 3)]
    [InlineData(3, 6)]
    [InlineData(6, 0)]
    [InlineData(6, 3)]
    [InlineData(6, 6)]
    public void Should_show_the_position_as_top_left_on_the_upper_left_cell_of_the_block(int column, int row)
    {
        // Arrange
        using var context = new TestContext();
        var cellComponent = context.RenderComponent<SudokuCell>();
        cellComponent.Instance.Cell.Column = column;
        cellComponent.Instance.Cell.Row = row;

        // Act
        var positionString = cellComponent.Instance.GetPositionInBlock();

        // Assert
        Assert.Equal("top left ", positionString);
    }

    [Theory]
    [InlineData(1, 0)]
    [InlineData(1, 3)]
    [InlineData(1, 6)]
    [InlineData(4, 0)]
    [InlineData(4, 3)]
    [InlineData(4, 6)]
    [InlineData(7, 0)]
    [InlineData(7, 3)]
    [InlineData(7, 6)]
    public void Should_show_the_position_as_top_on_the_upper_middle_cell_of_the_block(int column, int row)
    {
        // Arrange
        using var context = new TestContext();
        var cellComponent = context.RenderComponent<SudokuCell>();
        cellComponent.Instance.Cell.Column = column;
        cellComponent.Instance.Cell.Row = row;

        // Act
        var positionString = cellComponent.Instance.GetPositionInBlock();

        // Assert
        Assert.Equal("top ", positionString);
    }

    [Theory]
    [InlineData(2, 0)]
    [InlineData(2, 3)]
    [InlineData(2, 6)]
    [InlineData(5, 0)]
    [InlineData(5, 3)]
    [InlineData(5, 6)]
    [InlineData(8, 0)]
    [InlineData(8, 3)]
    [InlineData(8, 6)]
    public void Should_show_the_position_as_top_right_on_the_upper_right_cell_of_the_block(int column, int row)
    {
        // Arrange
        using var context = new TestContext();
        var cellComponent = context.RenderComponent<SudokuCell>();
        cellComponent.Instance.Cell.Column = column;
        cellComponent.Instance.Cell.Row = row;

        // Act
        var positionString = cellComponent.Instance.GetPositionInBlock();

        // Assert
        Assert.Equal("top right ", positionString);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(0, 4)]
    [InlineData(0, 7)]
    [InlineData(3, 1)]
    [InlineData(3, 4)]
    [InlineData(3, 7)]
    [InlineData(6, 1)]
    [InlineData(6, 4)]
    [InlineData(6, 7)]
    public void Should_show_the_position_as_left_on_the_left_middle_cell_of_the_block(int column, int row)
    {
        // Arrange
        using var context = new TestContext();
        var cellComponent = context.RenderComponent<SudokuCell>();
        cellComponent.Instance.Cell.Column = column;
        cellComponent.Instance.Cell.Row = row;

        // Act
        var positionString = cellComponent.Instance.GetPositionInBlock();

        // Assert
        Assert.Equal("left ", positionString);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(1, 4)]
    [InlineData(1, 7)]
    [InlineData(4, 1)]
    [InlineData(4, 4)]
    [InlineData(4, 7)]
    [InlineData(7, 1)]
    [InlineData(7, 4)]
    [InlineData(7, 7)]
    public void Should_show_the_position_as_empty_on_the_middle_cell_of_the_block(int column, int row)
    {
        // Arrange
        using var context = new TestContext();
        var cellComponent = context.RenderComponent<SudokuCell>();
        cellComponent.Instance.Cell.Column = column;
        cellComponent.Instance.Cell.Row = row;

        // Act
        var positionString = cellComponent.Instance.GetPositionInBlock();

        // Assert
        Assert.Empty(positionString);
    }

    [Theory]
    [InlineData(2, 1)]
    [InlineData(2, 4)]
    [InlineData(2, 7)]
    [InlineData(5, 1)]
    [InlineData(5, 4)]
    [InlineData(5, 7)]
    [InlineData(8, 1)]
    [InlineData(8, 4)]
    [InlineData(8, 7)]
    public void Should_show_the_position_as_right_on_the_middle_right_cell_of_the_block(int column, int row)
    {
        // Arrange
        using var context = new TestContext();
        var cellComponent = context.RenderComponent<SudokuCell>();
        cellComponent.Instance.Cell.Column = column;
        cellComponent.Instance.Cell.Row = row;

        // Act
        var positionString = cellComponent.Instance.GetPositionInBlock();

        // Assert
        Assert.Equal("right ", positionString);
    }

    [Theory]
    [InlineData(0, 2)]
    [InlineData(0, 5)]
    [InlineData(0, 8)]
    [InlineData(3, 2)]
    [InlineData(3, 5)]
    [InlineData(3, 8)]
    [InlineData(6, 2)]
    [InlineData(6, 5)]
    [InlineData(6, 8)]
    public void Should_show_the_position_as_bottom_left_on_the_lower_left_cell_of_the_block(int column, int row)
    {
        // Arrange
        using var context = new TestContext();
        var cellComponent = context.RenderComponent<SudokuCell>();
        cellComponent.Instance.Cell.Column = column;
        cellComponent.Instance.Cell.Row = row;

        // Act
        var positionString = cellComponent.Instance.GetPositionInBlock();

        // Assert
        Assert.Equal("bottom left ", positionString);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(1, 5)]
    [InlineData(1, 8)]
    [InlineData(4, 2)]
    [InlineData(4, 5)]
    [InlineData(4, 8)]
    [InlineData(7, 2)]
    [InlineData(7, 5)]
    [InlineData(7, 8)]
    public void Should_show_the_position_as_bottom_on_the_lower_middle_cell_of_the_block(int column, int row)
    {
        // Arrange
        using var context = new TestContext();
        var cellComponent = context.RenderComponent<SudokuCell>();
        cellComponent.Instance.Cell.Column = column;
        cellComponent.Instance.Cell.Row = row;

        // Act
        var positionString = cellComponent.Instance.GetPositionInBlock();

        // Assert
        Assert.Equal("bottom ", positionString);
    }

    [Theory]
    [InlineData(2, 2)]
    [InlineData(2, 5)]
    [InlineData(2, 8)]
    [InlineData(5, 2)]
    [InlineData(5, 5)]
    [InlineData(5, 8)]
    [InlineData(8, 2)]
    [InlineData(8, 5)]
    [InlineData(8, 8)]
    public void Should_show_the_position_as_bottom_right_on_the_lower_right_cell_of_the_block(int column, int row)
    {
        // Arrange
        using var context = new TestContext();
        var cellComponent = context.RenderComponent<SudokuCell>();
        cellComponent.Instance.Cell.Column = column;
        cellComponent.Instance.Cell.Row = row;

        // Act
        var positionString = cellComponent.Instance.GetPositionInBlock();

        // Assert
        Assert.Equal("bottom right ", positionString);
    }
}