namespace RefactoringBookExamples;

public class StatementDataCreator
{
    public StatementData CreateStatementData(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statementData = new StatementData();
        statementData.Performances = invoice.Performances.Select(Enrich).ToList();
        statementData.Customer = invoice.Customer;
        statementData.TotalAmount = GetTotalAmount(statementData);
        statementData.TotalVolumeCredits = TotalVolumeCredits(statementData);
        return statementData;

        Performance Enrich(Performance performance)
        {
            var calculator = PerformanceCalculator.CreatePerformanceCalculator(performance, PlayFor(performance));
            var enrich = new Performance();
            enrich.Audience = performance.Audience;
            enrich.Play = calculator.Play;
            enrich.Amount = calculator.Amount;
            enrich.VolumeCredits = calculator.VolumeCredits;
            return enrich;
        }

        int TotalVolumeCredits(StatementData data) => data.Performances.Sum(perf => perf.VolumeCredits);

        int GetTotalAmount(StatementData data) => data.Performances.Sum(perf => perf.Amount);

        Play PlayFor(Performance performance)
        {
            return plays[performance.PlayID];
        }
    }
}