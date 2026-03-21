namespace UniversityEquipment.Domain;

public class Rental
{
    public User User { get; set; }
    public Equipment Equipment { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal Penalty { get; set; }

    public Rental(User user, Equipment equipment, int days)
    {
        User = user;
        Equipment = equipment;
        RentalDate = DateTime.Today;
        DueDate = RentalDate.AddDays(days);
    }

    public bool IsActive => ReturnDate == null;

}