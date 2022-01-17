using Godot;

public class HeadsUp : Sprite
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Visible = false;
    }

    public override void _Process(float delta)
    {
        Area2D detect = (Area2D)GetParent();
        var list = detect.GetOverlappingAreas();
        if (list.Count > 0 && Visible == false)
        {
            foreach (var area in list)
            {
                if ((area is ManualPickup || area is InterActOnly))
                {
                    Visible = true;
                    break;
                }
                
            }
        }
        else if(list.Count == 0)
        {
            Visible = false;
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
