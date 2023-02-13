namespace RefactoringBookExamples;

public class PlainTextStatement
{
    private readonly StatementDataCreator _statementDataCreator = new();

    public string statement(Invoice invoice, Dictionary<string, Play> plays)
    {
        var statementData = _statementDataCreator.CreateStatementData(invoice, plays);
        return renderPlainText(statementData);
    }
    private string renderPlainText(StatementData data)
    {
        var result = $"Statement for {data.Customer}\n";
        foreach (var perf in data.Performances)
        {
            // print line for this order
            result += $"  {perf.Play.Name}: ${ToUSD(perf.Amount)} ({perf.Audience} seats)\n";
        }

        result += $"Amount owed is {ToUSD(data.TotalAmount)}\n";
        result += $"You earned {data.TotalVolumeCredits} credits\n";
        return result;
    }
    
    private string ToUSD(int thisAmount)
    {
        return (thisAmount / 100).ToString("C");
    }
}