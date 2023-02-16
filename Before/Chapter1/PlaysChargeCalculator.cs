
using System.Globalization;

namespace RefactoringBookExamples;

public class PlaysChargeCalculator
{
    public string statement (Invoice invoice, Dictionary<string, Play> plays) {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = $"Statement for {invoice.Customer}\n";
        
        foreach (var perf in invoice.Performances) {
            var play = plays[perf.PlayID];
            var thisAmount = 0;

            switch (play.Type) {
                case "tragedy":
                    thisAmount = 40000;
                    if (perf.Audience > 30) {
                        thisAmount += 1000 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    thisAmount = 30000;
                    if (perf.Audience > 20) {
                        thisAmount += 10000 + 500 * (perf.Audience - 20);
                    }
                    thisAmount += 300 * perf.Audience;
                    break;
                default:
                    throw new Exception($"unknown Type: {play.Type}");
            }

            // add volume credits
            volumeCredits += Math.Max(perf.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == play.Type) 
                volumeCredits += (int)Math.Floor((decimal)(perf.Audience / 5));

            // print line for this order
            result += $"  {play.Name}: {format(thisAmount/100)} ({perf.Audience} seats)\n";
            totalAmount += thisAmount;
        }
        result += $"Amount owed is {format(totalAmount/100)}\n";
        result += $"You earned {volumeCredits} credits\n";
        return result;
    }

    private string format(int thisAmount)
    {
        return thisAmount.ToString("C", new CultureInfo("en-US"));
    }
}