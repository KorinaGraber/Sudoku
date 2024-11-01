public interface ISudokuValidator
{
    public bool IsValidCellGroup(Cell[] cellGroup);
}

public class SudokuValidator : ISudokuValidator
{
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