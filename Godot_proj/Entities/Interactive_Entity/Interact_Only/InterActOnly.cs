using System;
using Godot;

public class InterActOnly : Area2D
{
    [Export] public int Delay = 2;
    public void Act()
    {
        if (GetChild(1) is Crate)
        {
            Crate crate = GetChild(1) as Crate;
            crate.act();
        }
    }
    
    public override void _Ready()
    {
        GetParent().GetNode("Player").Connect("Interact_hold", this, nameof(_on_Player_Interact_hold));
    }
    public void _on_Player_Interact_hold(Node node)
    {
        var AreaArray = GetOverlappingAreas();
        foreach (var body in AreaArray)
        {
            if (((Area2D) body).GetParent() == node)
            {
                GD.Print("interact");
                Act();
            }
        }
    }
}
