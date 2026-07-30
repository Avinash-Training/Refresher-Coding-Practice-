using System;
using System.Collections.Generic;
using System.Linq;

// Q5 - Surgeon Management System

class Hospital
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
}

class Ward
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Hospital Hospital { get; set; }
}

class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Ward Ward { get; set; }
}

class Surgeon
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; } // Senior, Non-Senior
    public List<Hospital> Hospitals { get; set; } = new List<Hospital>();
}

class Operation
{
    public int Id { get; set; }
    public Surgeon Surgeon { get; set; }
    public Patient Patient { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; }
}

class SurgeonManagement
{
    private List<Operation> operations = new List<Operation>();

    public void AddOperation(Operation op)
    {
        operations.Add(op);
    }

    public int TotalPatientsOperated()
    {
        return operations.Select(o => o.Patient.Id).Distinct().Count();
    }

    public List<Patient> GetPatientsBySurgeon(int surgeonId)
    {
        return operations.Where(o => o.Surgeon.Id == surgeonId)
                         .Select(o => o.Patient)
                         .ToList();
    }

    public List<Patient> GetPatientsByWard(int wardId)
    {
        return operations.Where(o => o.Patient.Ward.Id == wardId)
                         .Select(o => o.Patient)
                         .Distinct()
                         .ToList();
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Q5: Surgeon Management System ===\n");

        var hospital1 = new Hospital { Id = 1, Name = "Apollo", Location = "Chennai" };
        var hospital2 = new Hospital { Id = 2, Name = "Fortis", Location = "Bangalore" };

        var ward1 = new Ward { Id = 1, Name = "Cardiology Ward", Hospital = hospital1 };
        var ward2 = new Ward { Id = 2, Name = "Neurology Ward", Hospital = hospital2 };

        var patient1 = new Patient { Id = 1, Name = "Kumar", Age = 45, Ward = ward1 };
        var patient2 = new Patient { Id = 2, Name = "Meena", Age = 60, Ward = ward1 };
        var patient3 = new Patient { Id = 3, Name = "Ravi", Age = 38, Ward = ward2 };
        var patient4 = new Patient { Id = 4, Name = "Sunita", Age = 52, Ward = ward2 };

        var surgeon1 = new Surgeon { Id = 1, Name = "Dr. Sharma", Type = "Senior", Hospitals = new List<Hospital> { hospital1, hospital2 } };
        var surgeon2 = new Surgeon { Id = 2, Name = "Dr. Patel", Type = "Non-Senior", Hospitals = new List<Hospital> { hospital1 } };

        var mgmt = new SurgeonManagement();

        mgmt.AddOperation(new Operation { Id = 1, Surgeon = surgeon1, Patient = patient1, Date = DateTime.Today, Description = "Bypass surgery" });
        mgmt.AddOperation(new Operation { Id = 2, Surgeon = surgeon1, Patient = patient3, Date = DateTime.Today, Description = "Brain tumor removal" });
        mgmt.AddOperation(new Operation { Id = 3, Surgeon = surgeon2, Patient = patient2, Date = DateTime.Today, Description = "Angioplasty" });
        mgmt.AddOperation(new Operation { Id = 4, Surgeon = surgeon2, Patient = patient4, Date = DateTime.Today, Description = "Valve replacement" });

        Console.WriteLine($"Total patients operated: {mgmt.TotalPatientsOperated()}\n");

        Console.WriteLine($"Patients operated by '{surgeon1.Name}':");
        foreach (var p in mgmt.GetPatientsBySurgeon(surgeon1.Id))
            Console.WriteLine($"  {p.Name} | Age: {p.Age} | Ward: {p.Ward.Name}");

        Console.WriteLine($"\nPatients in '{ward1.Name}':");
        foreach (var p in mgmt.GetPatientsByWard(ward1.Id))
            Console.WriteLine($"  {p.Name} | Age: {p.Age}");
    }
}
