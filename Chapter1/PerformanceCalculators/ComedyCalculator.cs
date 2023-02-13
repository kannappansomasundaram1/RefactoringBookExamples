namespace RefactoringBookExamples;

public class ComedyCalculator : PerformanceCalculator
{
    public ComedyCalculator(Performance performance, Play play) : base(performance, play)
    {
    }

    public override int Amount
    {
        get
        {
            var result = 30000;
            if (_performance.Audience > 20)
            {
                result += 10000 + 500 * (_performance.Audience - 20);
            }

            result += 300 * _performance.Audience;
            return result;
        }
    }

    public override int VolumeCredits => base.VolumeCredits + (int)Math.Floor((decimal)(_performance.Audience / 5));
}