using System;
using System.Collections.Generic;
using System.Linq;

// Q1 - Online Exam Portal

class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Category { get; set; }
    public string Topic { get; set; }
    public string Type { get; set; } // MCQ or Paragraph
}

class MCQQuestion : Question
{
    public List<string> Options { get; set; }
    public string CorrectAnswer { get; set; }

    public MCQQuestion()
    {
        Type = "MCQ";
        Options = new List<string>();
    }
}

class ParagraphQuestion : Question
{
    public int WordLimit { get; set; }

    public ParagraphQuestion()
    {
        Type = "Paragraph";
    }
}

class ExamPortal
{
    private List<Question> questions = new List<Question>();

    public void AddQuestion(Question q)
    {
        questions.Add(q);
    }

    public int TotalQuestions()
    {
        return questions.Count;
    }

    public List<Question> GetByTopic(string topic)
    {
        return questions.Where(q => q.Topic.Equals(topic, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    public List<Question> GetByTopicAndCategory(string topic, string category)
    {
        return questions.Where(q =>
            q.Topic.Equals(topic, StringComparison.OrdinalIgnoreCase) &&
            q.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Q1: Online Exam Portal ===\n");

        var portal = new ExamPortal();

        portal.AddQuestion(new MCQQuestion
        {
            Id = 1, Text = "What is polymorphism?",
            Category = "OOP", Topic = "C#",
            Options = new List<string> { "A", "B", "C", "D" }, CorrectAnswer = "A"
        });

        portal.AddQuestion(new MCQQuestion
        {
            Id = 2, Text = "What is a delegate?",
            Category = "Advanced", Topic = "C#",
            Options = new List<string> { "A", "B", "C", "D" }, CorrectAnswer = "B"
        });

        portal.AddQuestion(new ParagraphQuestion
        {
            Id = 3, Text = "Explain SOLID principles.",
            Category = "OOP", Topic = "C#", WordLimit = 300
        });

        portal.AddQuestion(new MCQQuestion
        {
            Id = 4, Text = "What is a primary key?",
            Category = "Basics", Topic = "SQL",
            Options = new List<string> { "A", "B", "C", "D" }, CorrectAnswer = "C"
        });

        portal.AddQuestion(new ParagraphQuestion
        {
            Id = 5, Text = "Explain normalization.",
            Category = "Advanced", Topic = "SQL", WordLimit = 200
        });

        Console.WriteLine($"Total Questions: {portal.TotalQuestions()}\n");

        Console.WriteLine("Questions in Topic 'C#':");
        foreach (var q in portal.GetByTopic("C#"))
            Console.WriteLine($"  [{q.Type}] {q.Text} | Category: {q.Category}");

        Console.WriteLine("\nQuestions in Topic 'C#' and Category 'OOP':");
        foreach (var q in portal.GetByTopicAndCategory("C#", "OOP"))
            Console.WriteLine($"  [{q.Type}] {q.Text}");
    }
}
