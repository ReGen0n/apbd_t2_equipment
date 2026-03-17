namespace UniversityEquipment.Domain;

public class Laptop : Equipment
{
    public string  Processor {get; set;}
    public int RAMgb {get; set;}

    public Laptop(string name, string processor, int ramgb) : base(name)
    {
        Processor = processor;
        RAMgb = ramgb;
    }
}