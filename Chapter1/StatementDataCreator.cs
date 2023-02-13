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
            var calculator = new PerformanceCalculator(performance, PlayFor(performance));
            var enrich = new Performance();
            enrich.Audience = performance.Audience;
            enrich.Play = PlayFor(performance);
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

public class PerformanceCalculator
{
    private readonly Performance _performance;
    private readonly Play _playFor;

    public PerformanceCalculator(Performance performance, Play playFor)
    {
        _performance = performance;
        _playFor = playFor;
    }

    public int Amount
    {
        get
        {
            int result;
            switch (_performance.Play.Type)
            {
                case "tragedy":
                    result = 40000;
                    if (_performance.Audience > 30)
                    {
                        result += 1000 * (_performance.Audience - 30);
                    }

                    break;
                case "comedy":
                    result = 30000;
                    if (_performance.Audience > 20)
                    {
                        result += 10000 + 500 * (_performance.Audience - 20);
                    }

                    result += 300 * _performance.Audience;
                    break;
                default:
                    throw new Exception($"unknown Type: {_performance.Play.Type}");
            }

            return result;
        }
    }

    public int VolumeCredits
    {
        get
        {
            var result = Math.Max(_performance.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == _performance.Play.Type)
                result += (int)Math.Floor((decimal)(_performance.Audience / 5));
            return result;
        }
    }
}