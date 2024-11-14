public interface ISudokuSolver
{
    public Cell[,] SolveGrid(Cell[,] grid);
}

public class SudokuSolver : ISudokuSolver
{
    ISudokuValidator SudokuValidator;
    public int CurrentColumn;
    public int CurrentRow;
    public int CurrentValue;
    public Cell[,] Grid = new Cell[0, 0];
    public Stack<Cell> SolutionOperations = new Stack<Cell>();

    public virtual Cell CurrentCell { get => Grid[CurrentColumn, CurrentRow]; }

    public SudokuSolver(ISudokuValidator sudokuValidator)
    {
        SudokuValidator = sudokuValidator;
    }
    
    /*
        Sudoku solver algorithm, using backtracking method https://en.wikipedia.org/wiki/Backtracking

        In this method, we iterate through each cell in order, and try a value to determine if it will be valid
        For each cell that does not have a preset value
            For each value 1-9
                Validate the grid with this value
                if the result is invalid
                    abandon this run and continue the loop
                If the result is valid
                    The solver is complete
                If the result is incomplete
                    add this 'operation' to the solver stack as a potential step toward the solution
                    break out of this loop and move on to the next cell
            If we reach the end of the number loop and every result was invalid, this means we need to backtrack to the previous cell
            Pop the last operation from the solver stack
            Re-enter the 1-9 loop for that cell, and increment the value by 1

        At the end of the pseudo nested loop, we should end up with a completed puzzle unless we are given bad presets (such as if the presets already make an invalid puzzle)
    */
    public Cell[,] SolveGrid(Cell[,] grid)
    {
        Grid = grid;
        ResetSolver();

        while (CurrentColumn < 8 || CurrentRow < 8)
        {
            if (CurrentCell.Editable == false) {
                NextCell();
            } else {
                TryOperation();
            }
        }
        return Grid;
    }

    public void ResetSolver()
    {
        CurrentColumn = 0;
        CurrentRow = 0;
        CurrentValue = 1;
        SolutionOperations = new Stack<Cell>();
    }

    public virtual void TryOperation()
    {
        CurrentCell.Value = CurrentValue;
        var validationCheck = SudokuValidator.ValidateCellPlacement(Grid, CurrentCell);

        if (validationCheck == SudokuValidationState.Complete) {
            MarkValidOperation();
        } else if (validationCheck == SudokuValidationState.Incomplete) {
            MarkValidOperation();
        } else {
            GetNextOperation();
        }
    }

    public virtual void NextCell()
    {
        if (CurrentRow == 8) {
            CurrentColumn += 1;
            CurrentRow = 0;
        } else {
            CurrentRow += 1;
        }
        CurrentValue = 1;
    }

    public virtual void MarkValidOperation()
    {
        SolutionOperations.Push(CurrentCell);
        NextCell();
    }

    public virtual void GetNextOperation()
    {
        if (CurrentValue == 9) {
            CurrentCell.Value = 0;
            var previousCell = SolutionOperations.Pop();
            CurrentColumn = previousCell.Column;
            CurrentRow = previousCell.Row;
            CurrentValue = previousCell.Value ?? 1;
            GetNextOperation();
        } else {
            CurrentValue += 1;
        }
    }
}
