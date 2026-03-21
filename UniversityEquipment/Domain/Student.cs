namespace UniversityEquipment.Domain;

public class Student : User
{
    public Student(string FirstName, string LastName) : base(FirstName, LastName)
    {
        
    }

    public override int RentalLimit => 2;
}