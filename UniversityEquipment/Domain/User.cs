namespace UniversityEquipment.Domain;

public abstract class User
{
    private static int idCounter = 1;
    
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    protected User(string firstName, string lastName)
        {
            Id = idCounter++;
        FirstName = firstName;
        LastName = lastName;
        }
}