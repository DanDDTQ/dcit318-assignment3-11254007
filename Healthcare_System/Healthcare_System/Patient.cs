using System;

namespace HealthcareSystem;

// Patient class
public class Patient
{
    public int Id { get; }
    public string Name { get; }
    public int Age { get; }
    public string Gender { get; }

    public Patient(int id, string name, int age, string gender)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name required", nameof(name));
        if (age < 0) throw new ArgumentOutOfRangeException(nameof(age), "Age cannot be negative");
        if (string.IsNullOrWhiteSpace(gender)) throw new ArgumentException("Gender required", nameof(gender));

        Id = id;
        Name = name;
        Age = age;
        Gender = gender;
    }

    public override string ToString() => $"Patient(Id={Id}, Name={Name}, Age={Age}, Gender={Gender})";
}

// Prescription class
public class Prescription
{
    public int Id { get; }
    public int PatientId { get; }
    public string MedicationName { get; }
    public DateTime DateIssued { get; }

    public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
    {
        if (string.IsNullOrWhiteSpace(medicationName)) throw new ArgumentException("Medication name required", nameof(medicationName));

        Id = id;
        PatientId = patientId;
        MedicationName = medicationName;
        DateIssued = dateIssued;
    }

    public override string ToString() => $"Prescription(Id={Id}, PatientId={PatientId}, Medication={MedicationName}, Date={DateIssued:yyyy-MM-dd})";
}