namespace RefactoringBookExamples;

public abstract class PerformanceCalculator
{
    protected readonly Performance _performance;

    public static PerformanceCalculator CreatePerformanceCalculator(Performance performance, Play playFor)
    {
        return playFor.Type switch
        {
            "tragedy" => new TragedyCalculator(performance, playFor),
            "comedy" => new ComedyCalculator(performance, playFor),
            _ => throw new Exception($"unknown type: ${playFor.Type}")
        };
    }

    protected PerformanceCalculator(Performance performance, Play play)
    {
        _performance = performance;
        Play = play;
    }

    public abstract int Amount { get; }

    public virtual int VolumeCredits
    {
        get
        {
            var result = Math.Max(_performance.Audience - 30, 0);
           
            return result;
        }
    }

    public Play Play { get; }
}