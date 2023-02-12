using RefactoringBookExamples;

namespace Chapter1Tests;

public class PlaysChargeCalculatorTests
{
    [Fact]
    public void Returns_Statement()
    {
        var sut = new PlaysChargeCalculator();

        var result = sut.statement(new Invoice
        {
         Customer   = "BigCo",
         Performances = new List<Performance>
         {
             new ()
             {
                 PlayID =  "hamlet",
                 Audience = 55
             },
             new ()
             {
                 PlayID =  "as-like",
                 Audience = 35
             },
             new ()
             {
                 PlayID =  "othello",
                 Audience = 40
             },
         }
        }, new Dictionary<string, Play>
        {
            { "hamlet", new Play { Name = "hamlet", Type = "tragedy" } },
            { "as-like", new Play { Name = "as-like", Type = "comedy" } },
            { "othello", new Play { Name = "othello", Type = "tragedy" } }
        });

        result.Should().Be("Statement for BigCo\n  " +
                           "hamlet: $\u00a3650.00 (55 seats)\n  " +
                           "as-like: $\u00a3580.00 (35 seats)\n  " +
                           "othello: $\u00a3500.00 (40 seats)\n" +
                           "Amount owed is \u00a31,730.00\n" +
                           "You earned 47 credits\n");
    }
}