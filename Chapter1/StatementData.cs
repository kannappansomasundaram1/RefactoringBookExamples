namespace RefactoringBookExamples;

public class StatementData
{
    public string Customer { get; set; }
    public IEnumerable<EnrichedPerformance> EnrichedPerformances { get; set; }
    public int TotalAmount { get; set; }
    public int TotalVolumeCredits { get; set; }
}