using Bunit;
using Sudoku.Components.Pages;

public class SudokuTests
{
    [Fact]
    public void Should_clear_the_cell_on_a_delete_keypress()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<SudokuCell>(parameters => parameters
            .Add(p => p.Cell, new Cell(0, 0)));
        var cellElement = cut.Find(".cell");

        // Act
        cut.Find(".cell").KeyDown("Delete");

        // Assert
        var cellText = cellElement.TextContent;
        cellText.MarkupMatches("");
        cut.Instance.Cell.Value.Equals(null);
    }

    [Fact]
    public void Should_clear_the_cell_on_a_backspace_keypress()
    {
        // Arrange
        using var ctx = new TestContext();
        var cut = ctx.RenderComponent<SudokuCell>(parameters => parameters
            .Add(p => p.Cell, new Cell(0, 0)));
        var cellElement = cut.Find(".cell");

        // Act
        cut.Find(".cell").KeyDown("Backspace");

        // Assert
        var cellText = cellElement.TextContent;
        cellText.MarkupMatches("");
        cut.Instance.Cell.Value.Equals(null);
    }
}