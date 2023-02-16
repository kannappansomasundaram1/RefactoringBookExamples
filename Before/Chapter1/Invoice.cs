namespace RefactoringBookExamples;

public class Invoice
{
    public IEnumerable<Performance> Performances { get; init; }
    public string Customer { get; init; }
}