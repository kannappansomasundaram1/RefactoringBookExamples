
namespace RefactoringBookExamples;

public class PlaysChargeCalculator
{
    public string statement(Invoice invoice, Dictionary<string, Play> plays)
    {
        StatementData statementData = new StatementData();
        statementData.Performances = invoice.Performances.Select(Enrich).ToList();
        statementData.Customer = invoice.Customer;
        statementData.TotalAmount = GetTotalAmount(statementData);
        statementData.TotalVolumeCredits = TotalVolumeCredits(statementData);

        return renderPlainText(statementData);
        
        Performance Enrich(Performance performance)
        {
            var enrich = new Performance();
            enrich.Audience = performance.Audience;
            enrich.Play = PlayFor(performance);
            enrich.Amount = amountFor(enrich);
            enrich.VolumeCredits = VolumeCreditsFor(enrich);
            return enrich;
        }
        
        int TotalVolumeCredits(StatementData data)
        {
            var volumeCredits = 0;
            foreach (var perf in data.Performances)
            {
                // add volume credits
                volumeCredits += perf.VolumeCredits;
            }

            return volumeCredits;
        }

        int GetTotalAmount(StatementData data)
        {
            var totalAmount = 0;
            foreach (var perf in data.Performances)
            {
                totalAmount += perf.Amount;
            }

            return totalAmount;
        }
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

    

    private string renderPlainText(StatementData data) {
        var result = $"Statement for {data.Customer}\n";
        foreach (var perf in data.Performances)
        {
            // print line for this order
            result += $"  {perf.Play.Name}: ${ToUSD(perf.Amount)} ({perf.Audience} seats)\n";
        }
        result += $"Amount owed is {ToUSD(data.TotalAmount)}\n";
        result += $"You earned {data.TotalVolumeCredits} credits\n";
        return result;

        string ToUSD(int thisAmount)
        {
            return (thisAmount/100).ToString("C");
        }
    }
}

public class StatementData
{
    public string Customer { get; set; }
    public IEnumerable<Performance> Performances { get; set; }
    public int TotalAmount { get; set; }
    public int TotalVolumeCredits { get; set; }
}