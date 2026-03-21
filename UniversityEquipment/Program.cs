using System;
using UniversityEquipment.Domain;
using UniversityEquipment.Services;

namespace UniversityEquipment;

class Program
{
    static void Main(string[] args)
    {
        var service = new RentalService();

        var student1 = new Student("Anna", "Nowak");
        var student2 = new Student("Jan", "Kowalski");
        var employee1 = new Employee("Maria", "Wisniewska");

        service.AddUser(student1);
        service.AddUser(student2);
        service.AddUser(employee1);

        var laptop1 = new Laptop("Dell XPS", "Intel i7", 16);
        var laptop2 = new Laptop("Lenovo ThinkPad", "Intel i5", 8);
        var projector1 = new Projector("Epson Projector", 4000, "Full HD");
        var camera1 = new Camera("Canon EOS", 24, true);

        service.AddEquipment(laptop1);
        service.AddEquipment(laptop2);
        service.AddEquipment(projector1);
        service.AddEquipment(camera1);

        service.ShowAllEquipment();
        service.ShowAvailableEquipment();

        Console.WriteLine("\n=== CORRECT RENTAL ===");
        service.RentEquipment(student1.Id, laptop1.Id, 3);

        Console.WriteLine("\n=== INVALID RENTAL ===");
        service.RentEquipment(student2.Id, laptop1.Id, 2);

        Console.WriteLine("\n=== ON-TIME RETURN ===");
        service.ReturnEquipment(student1.Id, laptop1.Id, DateTime.Today.AddDays(2));

        Console.WriteLine("\n=== DELAYED RETURN WITH PENALTY ===");
        service.RentEquipment(employee1.Id, camera1.Id, 1);
        service.ReturnEquipment(employee1.Id, camera1.Id, DateTime.Today.AddDays(4));

        Console.WriteLine("\n=== MARK EQUIPMENT UNAVAILABLE ===");
        service.MarkEquipmentUnavailable(projector1.Id);

        Console.WriteLine("\n=== ACTIVE RENTALS FOR EMPLOYEE ===");
        service.ShowActiveRentalsForUser(employee1.Id);

        Console.WriteLine("\n=== OVERDUE RENTALS ===");
        service.ShowOverdueRentals();

        service.GenerateSummaryReport();
    }
}