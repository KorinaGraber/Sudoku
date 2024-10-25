public class Cell {
    public int Row { get; set; }
    public int Column { get; set; }
    public int? Value { get; set; }
    public bool Focused { get; set; }

    public Cell(int column, int row) {
        Row = row;
        Column = column;
    }
}