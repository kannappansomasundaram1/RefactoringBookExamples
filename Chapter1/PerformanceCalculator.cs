namespace RefactoringBookExamples;

public abstract class PerformanceCalculator
{
    protected readonly Performance _performance;
    protected readonly Play _play;

    public static PerformanceCalculator CreatePerformanceCalculator(Performance performance, Play playFor)
    {
        switch (playFor.Type)
        {
            case "tragedy": return new TragedyCalculator(performance, playFor);
            case "comedy": return new ComedyCalculator(performance, playFor);
            default:
                throw new Exception($"unknown type: ${playFor.Type}");
        }
    }

    protected PerformanceCalculator(Performance performance, Play play)
    {
        _performance = performance;
        _play = play;
    }

    public abstract int Amount { get; }

    public int VolumeCredits
    {
        get
        {
            var result = Math.Max(_performance.Audience - 30, 0);
            // add extra credit for every ten comedy attendees
            if ("comedy" == _play.Type)
                result += (int)Math.Floor((decimal)(_performance.Audience / 5));
            return result;
        }
    }
}