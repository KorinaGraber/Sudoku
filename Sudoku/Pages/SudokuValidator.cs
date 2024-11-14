public enum SudokuValidationState
{
    Complete,
    Incomplete,
    Invalid,
}

public interface ISudokuValidator
{
    public SudokuValidationState ValidateSudokuGrid(Cell[,] sudokuGrid);
    public SudokuValidationState ValidateCellPlacement(Cell[,] grid, Cell cell);
    public SudokuValidationState ValidateCellGroup(Cell[] cellGroup);

    public Cell[] GetColumn(Cell[,] grid, int columnIndex);
    public Cell[] GetRow(Cell[,] grid, int rowIndex);
    public Cell[] GetBlock(Cell[,] grid, int blockIndex);
}

public class SudokuValidator : ISudokuValidator
{
    public SudokuValidationState ValidateSudokuGrid(Cell[,] sudokuGrid)
    {
        var hasIncompleteBlock = false;

        for(var columnIndex = 0; columnIndex < 9; columnIndex++) {
            switch (ValidateCellGroup(GetColumn(sudokuGrid, columnIndex))) {
                case SudokuValidationState.Invalid:
                    return SudokuValidationState.Invalid;
                case SudokuValidationState.Incomplete:
                    hasIncompleteBlock = true;
                    break;
            }
        }

        for(var rowIndex = 0; rowIndex < 9; rowIndex++) {
            switch (ValidateCellGroup(GetRow(sudokuGrid, rowIndex))) {
                case SudokuValidationState.Invalid:
                    return SudokuValidationState.Invalid;
                case SudokuValidationState.Incomplete:
                    hasIncompleteBlock = true;
                    break;
            }
        }

        for(var blockIndex = 0; blockIndex < 9; blockIndex++) {
            switch (ValidateCellGroup(GetBlock(sudokuGrid, blockIndex))) {
                case SudokuValidationState.Invalid:
                    return SudokuValidationState.Invalid;
                case SudokuValidationState.Incomplete:
                    hasIncompleteBlock = true;
                    break;
            }
        }

        return hasIncompleteBlock
            ? SudokuValidationState.Incomplete
            : SudokuValidationState.Complete;
    }

    public virtual SudokuValidationState ValidateCellPlacement(Cell[,] grid, Cell cell)
    {
        grid[cell.Column, cell.Row].Value = cell.Value;
        var hasIncompleteBlock = false;

        switch (ValidateCellGroup(GetColumn(grid, cell.Column))) {
            case SudokuValidationState.Invalid:
                return SudokuValidationState.Invalid;
            case SudokuValidationState.Incomplete:
                hasIncompleteBlock = true;
                break;
        }

        switch (ValidateCellGroup(GetRow(grid, cell.Row))) {
            case SudokuValidationState.Invalid:
                return SudokuValidationState.Invalid;
            case SudokuValidationState.Incomplete:
                hasIncompleteBlock = true;
                break;
        }

        switch (ValidateCellGroup(GetBlock(grid, cell.Block))) {
            case SudokuValidationState.Invalid:
                return SudokuValidationState.Invalid;
            case SudokuValidationState.Incomplete:
                hasIncompleteBlock = true;
                break;
        }

        return hasIncompleteBlock
            ? SudokuValidationState.Incomplete
            : SudokuValidationState.Complete;
    }

    public SudokuValidationState ValidateCellGroup(Cell[] cellGroup)
    {
        var tracker = new BlockValidationTracker();

        foreach(var cell in cellGroup) {
            if (cell.Value != null && cell.Value > 0 && cell.Value <= 9) {
                if (tracker.ValueIsDuplicate((int)cell.Value)) {
                    return SudokuValidationState.Invalid;
                }
            }
        }

        return tracker.IsCompleteBlock()
            ? SudokuValidationState.Complete
            : SudokuValidationState.Incomplete;
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

    public bool IsCompleteBlock()
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