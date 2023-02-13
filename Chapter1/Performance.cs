﻿namespace RefactoringBookExamples;

public record Performance
{
    public string PlayID { get; set; }
    public Play Play { get; set; }
    public int Audience { get; set; }
    public int Amount { get; set; }
}