namespace RefactoringBookExamples;

public class StatementDataCreator
{
    public StatementData CreateStatementData(Invoice invoice, Dictionary<string, Play> plays)
    {
        var enrichedPerformances = invoice.Performances
            .Select(performance => Enrich(performance, PlayFor(performance)))
            .ToList();

        var statementData = new StatementData
        {
            Customer = invoice.Customer,
            TotalAmount = GetTotalAmount(enrichedPerformances),
            TotalVolumeCredits = TotalVolumeCredits(enrichedPerformances),
            EnrichedPerformances = enrichedPerformances,
        };
        return statementData;

        Play PlayFor(Performance performance)
        {
            return plays[performance.PlayID];
        }
    }

    EnrichedPerformance Enrich(Performance performance, Play play)
    {
        var calculator = PerformanceCalculator.CreatePerformanceCalculator(performance, play);
        return new EnrichedPerformance
        {
            Audience = performance.Audience,
            Play = calculator.Play,
            Amount = calculator.Amount,
            VolumeCredits = calculator.VolumeCredits
        };
    }

    int TotalVolumeCredits(IEnumerable<EnrichedPerformance> performances) =>
        performances.Sum(perf => perf.VolumeCredits);

    int GetTotalAmount(IEnumerable<EnrichedPerformance> performances) => performances.Sum(perf => perf.Amount);
}