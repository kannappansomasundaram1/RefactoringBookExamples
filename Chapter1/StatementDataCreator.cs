namespace RefactoringBookExamples;

public class StatementDataCreator
{
    public StatementData CreateStatementData(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statementData = new StatementData();
        statementData.EnrichedPerformances = invoice.Performances.Select(Enrich).ToList();
        statementData.Customer = invoice.Customer;
        statementData.TotalAmount = GetTotalAmount(statementData.EnrichedPerformances);
        statementData.TotalVolumeCredits = TotalVolumeCredits(statementData.EnrichedPerformances);
        return statementData;

        EnrichedPerformance Enrich(Performance performance)
        {
            var calculator = PerformanceCalculator.CreatePerformanceCalculator(performance, PlayFor(performance));
            var enrich = new EnrichedPerformance();
            enrich.Audience = performance.Audience;
            enrich.Play = calculator.Play;
            enrich.Amount = calculator.Amount;
            enrich.VolumeCredits = calculator.VolumeCredits;
            return enrich;
        }

        int TotalVolumeCredits(IEnumerable<EnrichedPerformance> performances) => performances.Sum(perf => perf.VolumeCredits);

        int GetTotalAmount(IEnumerable<EnrichedPerformance> performances) => performances.Sum(perf => perf.Amount);

        Play PlayFor(Performance performance)
        {
            return plays[performance.PlayID];
        }
    }
}