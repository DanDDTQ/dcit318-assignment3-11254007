using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthcareSystem;
    public static class Program
    {
        public static void Main()
        {
            var app = new HealthSystemApp();

            // i. Seed data
            app.SeedData();

            // ii. Build prescription dictionary (grouping)
            app.BuildPrescriptionMap();

            // iii. Print all patients
            app.PrintAllPatients();

            // iv. Select one PatientId and print all prescriptions for that patient
            // (we pick PatientId = 2 as an example)
            int selectedPatientId = 2;
            app.PrintPrescriptionsForPatient(selectedPatientId);

            //Console.WriteLine("\nPress any key to exit...");
            //Console.ReadKey();
        }
    }

