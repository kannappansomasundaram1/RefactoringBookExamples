namespace RefactoringBookExamples;

public class TragedyCalculator : PerformanceCalculator
{
    public TragedyCalculator(Performance performance, Play play) : base(performance, play)
    {
    }

    public override int Amount
    {
        get
        {
            int result = 40000;
            if (_performance.Audience > 30)
            {
                result += 1000 * (_performance.Audience - 30);
            }
            return result;
        }
    }
}