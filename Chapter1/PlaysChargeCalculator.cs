
namespace RefactoringBookExamples;

public class PlaysChargeCalculator
{
    public string statement (Invoice invoice, Dictionary<string, Play> plays) {
        var result = $"Statement for {invoice.Customer}\n";
        foreach (var perf in invoice.Performances)
        {
            // print line for this order
            result += $"  {PlayFor(perf).Name}: ${ToUSD(amountFor(perf))} ({perf.Audience} seats)\n";
        }
        result += $"Amount owed is {ToUSD(GetTotalAmount())}\n";
        result += $"You earned {TotalVolumeCredits()} credits\n";
        return result;
        
        Play PlayFor(Performance performance)
        {
            return plays[performance.PlayID];
        }
        string ToUSD(int thisAmount)
        {
            return (thisAmount/100).ToString("C");
        }
        
        int amountFor(Performance aPerformance)
        {
            int result;
            switch (PlayFor(aPerformance).Type)
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
                throw new Exception($"unknown Type: {PlayFor(aPerformance).Type}");
            }

            return result;
        }
        
        int VolumeCreditsFor(Performance aPerformance)
        {
            int result;
            result = Math.Max(aPerformance.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == PlayFor(aPerformance).Type)
                result += (int)Math.Floor((decimal)(aPerformance.Audience / 5));
            return result;
        }

        int TotalVolumeCredits()
        {
            var volumeCredits = 0;
            foreach (var perf in invoice.Performances)
            {
                // add volume credits
                volumeCredits += VolumeCreditsFor(perf);
            }

            return volumeCredits;
        }

        int GetTotalAmount()
        {
            var totalAmount = 0;
            foreach (var perf in invoice.Performances)
            {
                totalAmount += amountFor(perf);
            }

            return totalAmount;
        }
    }


}