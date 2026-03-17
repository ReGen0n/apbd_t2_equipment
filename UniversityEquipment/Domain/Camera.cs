namespace UniversityEquipment.Domain;

public class Camera : Equipment
{
    public int MegaPixels { get; set; }
    public bool HasFlash {get; set;}
    
    public Camera(string name, int megaPixels, bool hasFlash) : base(name)
        {
        MegaPixels = megaPixels;
        HasFlash = hasFlash;
        }
}