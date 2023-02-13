namespace RefactoringBookExamples;

public class Invoice
{
    public IEnumerable<Performance> Performances { get; set; }
    public string Customer { get; set; }
}