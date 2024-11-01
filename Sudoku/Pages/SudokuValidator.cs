public interface ISudokuValidator
{
    public bool IsValidSudokuGrid(Cell[,] sudokuGrid);
    public bool IsValidCellGroup(Cell[] cellGroup);

    public Cell[] GetColumn(Cell[,] grid, int columnIndex);
    public Cell[] GetRow(Cell[,] grid, int rowIndex);
    public Cell[] GetBlock(Cell[,] grid, int blockIndex);
}

public class SudokuValidator : ISudokuValidator
{
    public bool IsValidSudokuGrid(Cell[,] sudokuGrid)
    {
        for(var columnIndex = 0; columnIndex < 9; columnIndex++) {
            if (IsValidCellGroup(GetColumn(sudokuGrid, columnIndex)) == false) {
                return false;
            }
        }

        for(var rowIndex = 0; rowIndex < 9; rowIndex++) {
            if (IsValidCellGroup(GetRow(sudokuGrid, rowIndex)) == false) {
                return false;
            }
        }

        for(var blockIndex = 0; blockIndex < 9; blockIndex++) {
            if (IsValidCellGroup(GetBlock(sudokuGrid, blockIndex)) == false) {
                return false;
            }
        }

        return true;
    }

    public bool IsValidCellGroup(Cell[] cellGroup)
    {
        var tracker = new BlockValidationTracker();

        foreach(var cell in cellGroup) {
            if (cell.Value == null || cell.Value <= 0 || cell.Value > 9) {
                return false;
            }

            if (tracker.ValueIsDuplicate((int)cell.Value)) {
                return false;
            }
        }

        return tracker.IsValidBlock();
    }

    public Cell[] GetColumn(Cell[,] grid, int columnIndex)
    {
        return Enumerable.Range(0, grid.GetLength(0))
            .Select(x => grid[columnIndex, x])
            .ToArray();
    }

    public Cell[] GetRow(Cell[,] grid, int rowIndex)
    {
        return Enumerable.Range(0, grid.GetLength(0))
            .Select(x => grid[x, rowIndex])
            .ToArray();
    }

    public Cell[] GetBlock(Cell[,] grid, int blockIndex)
    {
        var columnStart = (blockIndex % 3) * 3;
        var rowStart = (blockIndex / 3) * 3;
        var block = new Cell[9];
        for (var blockColumnIndex = 0; blockColumnIndex < 3; blockColumnIndex++) {
            for (var blockRowIndex = 0; blockRowIndex < 3; blockRowIndex++) {
                block[blockColumnIndex + (blockRowIndex * 3)] = grid[columnStart + blockColumnIndex, rowStart + blockRowIndex];
            }
        }

        return block;
    }
}

class BlockValidationTracker
{
    public Dictionary<int, bool> ValidationDictionary { get; set; } = new Dictionary<int, bool>()
    {
        { 1, false },
        { 2, false },
        { 3, false },
        { 4, false },
        { 5, false },
        { 6, false },
        { 7, false },
        { 8, false },
        { 9, false },
    };

    public bool ValueIsDuplicate(int value) {
        if (ValidationDictionary[value]) {
            return true;
        }

        ValidationDictionary[value] = true;
        return false;
    }

    public bool IsValidBlock()
    {
        return ValidationDictionary[1]
            && ValidationDictionary[2]
            && ValidationDictionary[3]
            && ValidationDictionary[4]
            && ValidationDictionary[5]
            && ValidationDictionary[6]
            && ValidationDictionary[7]
            && ValidationDictionary[8]
            && ValidationDictionary[9];
    }
}