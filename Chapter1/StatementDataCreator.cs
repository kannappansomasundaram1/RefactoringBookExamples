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
            var enrich = new Performance();
            enrich.Audience = performance.Audience;
            enrich.Play = PlayFor(performance);
            enrich.Amount = amountFor(enrich);
            enrich.VolumeCredits = VolumeCreditsFor(enrich);
            return enrich;
        }

        int TotalVolumeCredits(StatementData data) => data.Performances.Sum(perf => perf.VolumeCredits);

        int GetTotalAmount(StatementData data) => data.Performances.Sum(perf => perf.Amount);

        int VolumeCreditsFor(Performance aPerformance)
        {
            int result;
            result = Math.Max(aPerformance.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == aPerformance.Play.Type)
                result += (int)Math.Floor((decimal)(aPerformance.Audience / 5));
            return result;
        }

        int amountFor(Performance aPerformance)
        {
            int result;
            switch (aPerformance.Play.Type)
            {
                case "tragedy":
                    result = 40000;
                    if (aPerformance.Audience > 30)
                    {
                        result += 1000 * (aPerformance.Audience - 30);
                    }

                    break;
                case "comedy":
                    result = 30000;
                    if (aPerformance.Audience > 20)
                    {
                        result += 10000 + 500 * (aPerformance.Audience - 20);
                    }

                    result += 300 * aPerformance.Audience;
                    break;
                default:
                    throw new Exception($"unknown Type: {aPerformance.Play.Type}");
            }

            return result;
        }


        Play PlayFor(Performance performance)
        {
            return plays[performance.PlayID];
        }
    }
}