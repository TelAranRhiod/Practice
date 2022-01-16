using Godot;
using System;
using System.Runtime.CompilerServices;

public class InterActOnly : Area2D
{
    [Export] public int Delay = 2;
    public void Act()
    {
        try
        {
            GD.Print("acting on Interact only");
        }
        catch (InvalidCastException e)
        {
            GD.Print(e);
        }
    }
    
    
    public void _on_Player_Interact_hold(Node node)
    {
        var AreaArray = GetOverlappingAreas();
        foreach (var body in AreaArray)
        {
            if (((Area2D) body).GetParent() == node)
            {
                GD.Print("interact");
                this.Act();
            }
        }
    }
}
