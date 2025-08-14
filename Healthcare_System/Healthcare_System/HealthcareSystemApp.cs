using System;

namespace HealthcareSystem;

public class HealthSystemApp
{
    private readonly Repository<Patient> _patientRepo = new();
    private readonly Repository<Prescription> _prescriptionRepo = new();
    private readonly Dictionary<int, List<Prescription>> _prescriptionMap = new();

    // Seed sample patients and prescriptions
    public void SeedData()
    {
        // Add 2-3 patients
        _patientRepo.Add(new Patient(1, "Alice Johnson", 34, "Female"));
        _patientRepo.Add(new Patient(2, "Bob Mensah", 45, "Male"));
        _patientRepo.Add(new Patient(3, "Clara Osei", 29, "Female"));

        // Add 4-5 prescriptions (ensure valid PatientIds)
        _prescriptionRepo.Add(new Prescription(1, 1, "Amoxicillin 500mg", DateTime.UtcNow.AddDays(-10)));
        _prescriptionRepo.Add(new Prescription(2, 1, "Paracetamol 500mg", DateTime.UtcNow.AddDays(-3)));
        _prescriptionRepo.Add(new Prescription(3, 2, "Lisinopril 10mg", DateTime.UtcNow.AddDays(-30)));
        _prescriptionRepo.Add(new Prescription(4, 3, "Metformin 500mg", DateTime.UtcNow.AddDays(-7)));
        _prescriptionRepo.Add(new Prescription(5, 2, "Atorvastatin 20mg", DateTime.UtcNow.AddDays(-1)));
    }

    // Build dictionary mapping PatientId -> List<Prescription>
    public void BuildPrescriptionMap()
    {
        _prescriptionMap.Clear();

        foreach (var pres in _prescriptionRepo.GetAll())
        {
            if (!_prescriptionMap.TryGetValue(pres.PatientId, out var list))
            {
                list = new List<Prescription>();
                _prescriptionMap[pres.PatientId] = list;
            }
            list.Add(pres);
        }
    }

    // Retrieve prescriptions by patient id (returns empty list if none found)
    public List<Prescription> GetPrescriptionsByPatientId(int patientId)
    {
        if (_prescriptionMap.TryGetValue(patientId, out var list))
        {
            // return a copy to protect internal state
            return new List<Prescription>(list);
        }
        return new List<Prescription>();
    }

    // Print all patients stored in repository
    public void PrintAllPatients()
    {
        var patients = _patientRepo.GetAll();
        Console.WriteLine("Patients:");
        foreach (var p in patients)
        {
            Console.WriteLine($" - {p}");
        }
    }

    // Print prescriptions for a specific patient using the map
    public void PrintPrescriptionsForPatient(int patientId)
    {
        var patient = _patientRepo.GetById(p => p.Id == patientId);
        if (patient == null)
        {
            Console.WriteLine($"No patient found with Id {patientId}");
            return;
        }

        var prescriptions = GetPrescriptionsByPatientId(patientId);
        Console.WriteLine($"\nPrescriptions for {patient.Name} (Id={patient.Id}):");

        if (!prescriptions.Any())
        {
            Console.WriteLine(" - (no prescriptions found)");
            return;
        }

        foreach (var pres in prescriptions.OrderByDescending(p => p.DateIssued))
        {
            Console.WriteLine($" - {pres}");
        }
    }
}
