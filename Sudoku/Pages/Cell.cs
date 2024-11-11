public class Cell {
    public int Row { get; set; }
    public int Column { get; set; }
    public int? Value { get; set; }
    public bool Focused { get; set; }

    public int Block {
        get
        {
            var columnDifferential = Column / 3;
            var rowDifferential = Row / 3;
            return columnDifferential + (rowDifferential * 3);
        }
    }

    public Cell(int column, int row) {
        Row = row;
        Column = column;
    }
}