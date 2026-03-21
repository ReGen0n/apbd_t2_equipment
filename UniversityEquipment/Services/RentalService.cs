using System;
using System.Collections.Generic;
using System.Linq;
using UniversityEquipment.Domain;

namespace UniversityEquipment.Services;

public class RentalService
{
    private readonly List<User> _users = new();
    private readonly List<Equipment> _equipment = new();
    private readonly List<Rental> _rentals = new();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void AddEquipment(Equipment equipment)
    {
        _equipment.Add(equipment);
    }

    public void ShowAllEquipment()
    {
        Console.WriteLine("\n=== ALL EQUIPMENT ===");
        foreach (var item in _equipment)
        {
            Console.WriteLine($"{item.Id}: {item.Name} | Available: {item.IsAvailable}");
        }
    }

    public void ShowAvailableEquipment()
    {
        Console.WriteLine("\n=== AVAILABLE EQUIPMENT ===");
        foreach (var item in _equipment.Where(e => e.IsAvailable))
        {
            Console.WriteLine($"{item.Id}: {item.Name}");
        }
    }

    public void RentEquipment(int userId, int equipmentId, int days)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        var equipment = _equipment.FirstOrDefault(e => e.Id == equipmentId);

        if (user == null)
        {
            Console.WriteLine("User not found.");
            return;
        }

        if (equipment == null)
        {
            Console.WriteLine("Equipment not found.");
            return;
        }

        if (!equipment.IsAvailable)
        {
            Console.WriteLine("Equipment is unavailable.");
            return;
        }

        int activeRentals = _rentals.Count(r => r.User.Id == userId && r.IsActive);

        if (activeRentals >= user.RentalLimit)
        {
            Console.WriteLine("User exceeded rental limit.");
            return;
        }

        var rental = new Rental(user, equipment, days);
        _rentals.Add(rental);
        equipment.IsAvailable = false;

        Console.WriteLine($"Rental created: {user.FirstName} {user.LastName} rented {equipment.Name}");
    }

    public void ReturnEquipment(int userId, int equipmentId, DateTime? returnDate = null)
    {
        var rental = _rentals.FirstOrDefault(r =>
            r.User.Id == userId &&
            r.Equipment.Id == equipmentId &&
            r.IsActive);

        if (rental == null)
        {
            Console.WriteLine("Active rental not found.");
            return;
        }

        DateTime actualReturn = returnDate ?? DateTime.Today;
        rental.ReturnDate = actualReturn;
        rental.Equipment.IsAvailable = true;

        if (actualReturn > rental.DueDate)
        {
            int lateDays = (actualReturn - rental.DueDate).Days;
            rental.Penalty = lateDays * 10m;
            Console.WriteLine($"Returned late. Penalty: {rental.Penalty}");
        }
        else
        {
            rental.Penalty = 0;
            Console.WriteLine("Returned on time.");
        }
    }

    public void MarkEquipmentUnavailable(int equipmentId)
    {
        var equipment = _equipment.FirstOrDefault(e => e.Id == equipmentId);

        if (equipment == null)
        {
            Console.WriteLine("Equipment not found.");
            return;
        }

        equipment.IsAvailable = false;
        Console.WriteLine($"{equipment.Name} marked as unavailable.");
    }

    public void ShowActiveRentalsForUser(int userId)
    {
        Console.WriteLine("\n=== ACTIVE RENTALS FOR USER ===");

        var rentals = _rentals.Where(r => r.User.Id == userId && r.IsActive).ToList();

        if (!rentals.Any())
        {
            Console.WriteLine("No active rentals.");
            return;
        }

        foreach (var rental in rentals)
        {
            Console.WriteLine($"{rental.User.FirstName} {rental.User.LastName} -> {rental.Equipment.Name}, due {rental.DueDate:d}");
        }
    }

    public void ShowOverdueRentals()
    {
        Console.WriteLine("\n=== OVERDUE RENTALS ===");

        var overdue = _rentals.Where(r => r.IsActive && DateTime.Today > r.DueDate).ToList();

        if (!overdue.Any())
        {
            Console.WriteLine("No overdue rentals.");
            return;
        }

        foreach (var rental in overdue)
        {
            Console.WriteLine($"{rental.User.FirstName} {rental.User.LastName} -> {rental.Equipment.Name}, due {rental.DueDate:d}");
        }
    }

    public void GenerateSummaryReport()
    {
        Console.WriteLine("\n=== SUMMARY REPORT ===");
        Console.WriteLine($"Users: {_users.Count}");
        Console.WriteLine($"Equipment items: {_equipment.Count}");
        Console.WriteLine($"Available equipment: {_equipment.Count(e => e.IsAvailable)}");
        Console.WriteLine($"Active rentals: {_rentals.Count(r => r.IsActive)}");
        Console.WriteLine($"Overdue rentals: {_rentals.Count(r => r.IsActive && DateTime.Today > r.DueDate)}");
        Console.WriteLine($"Total penalties: {_rentals.Sum(r => r.Penalty)}");
    }
}