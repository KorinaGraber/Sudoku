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
}