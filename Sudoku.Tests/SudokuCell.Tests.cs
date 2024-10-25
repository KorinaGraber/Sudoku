using Bunit;
using Sudoku.Components.Pages;

public class SudokuTests
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
        using var ctx = new TestContext();
        int? valueOnParent = null;
        var cut = ctx.RenderComponent<SudokuCell>(parameters => parameters
            .Add(p => p.Cell, new Cell(0, 0))
            .Add(p => p.SetValue, x => valueOnParent = x));
        var cellElement = cut.Find(".cell");

        // Act
        cut.Find(".cell").KeyDown(numberEntry);

        // Assert
        Assert.Equal(numberValue, cut.Instance.Cell.Value);
        Assert.Equal(numberValue, valueOnParent);
    }

    [Theory]
    [InlineData("Delete")]
    [InlineData("Backspace")]
    public void Should_clear_the_cell_on_a_delete_or_backspace_keypress(string keypress)
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<SudokuCell>(parameters => parameters
            .Add(p => p.Cell, new Cell(0, 0)));
        var cellElement = cut.Find(".cell");

        // Act
        cut.Find(".cell").KeyDown(keypress);

        // Assert
        var cellText = cellElement.TextContent;
        cellText.MarkupMatches("");
        Assert.Null(cut.Instance.Cell.Value);
    }

    [Theory]
    [InlineData("ArrowUp")]
    [InlineData("ArrowDown")]
    [InlineData("ArrowLeft")]
    [InlineData("ArrowRight")]
    public void Should_send_a_change_focus_event_to_the_parent_on_an_arrow_key(string arrowKey)
    {
        // Arrange
        using var ctx = new TestContext();
        var cellOnParent = new Cell(0, 0);
        var changeEventOnParent = "";
        var cut = ctx.RenderComponent<SudokuCell>(parameters => parameters
            .Add(p => p.Cell, new Cell(0, 0))
            .Add(p => p.ChangeFocus, focusEvent => {
                cellOnParent = focusEvent.Cell;
                changeEventOnParent = focusEvent.ChangeEvent;
            }));
        var cellElement = cut.Find(".cell");

        // Act
        cut.Find(".cell").KeyDown(arrowKey);

        // Assert
        var cellText = cellElement.TextContent;
        cellText.MarkupMatches("");
        Assert.Equal(cut.Instance.Cell, cellOnParent);
        Assert.Equal(arrowKey, changeEventOnParent);
    }
}