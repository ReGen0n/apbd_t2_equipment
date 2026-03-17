namespace UniversityEquipment.Domain;
public abstract class Equipment
{
    private static int idCounter = 1;

    public int Id { get;}
    public string Name {get; set;}
    public bool IsAvailable {get; set;}

    public Equipment(string name)
    {
        Id = idCounter++;
        Name = name;
        IsAvailable = true;
    }
}