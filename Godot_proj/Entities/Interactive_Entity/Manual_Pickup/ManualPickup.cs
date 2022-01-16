using Godot;
using System;
using System.Collections;
using System.Threading;

public class ManualPickup : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export] public int magnet_speed_scaler = 20;
    
    private Boolean getMagnet = false;
    private string target = "";
    public override void _PhysicsProcess(float delta)
    {
        if (getMagnet)
        {
            Position += (((Node2D) GetParent().GetNode(target)).Position - Position) / magnet_speed_scaler;
        }
        
    }
    public override void _Ready()
    {
        GetParent().GetNode("Player").Connect("Interact", this, nameof(_on_Player_Interact));
    }
    public void _on_Player_Interact(Node node)
    {
        var AreaArray = GetOverlappingAreas();
        foreach (var body in AreaArray)
        {
            if (((Area2D) body).GetParent() == node)
            {
                target = node.Name;
                getMagnet = true;
                
                
                foreach (Node bod in GetOverlappingBodies())
                {
                    if (target == bod.Name )
                    {
                        ((Player) node).pickup(this);
                    }
                }
                
            }
        }
        
    }
    public void _on_ManualPickup_body_entered(Node body)
    {
        if (getMagnet)
        {
            ((Player) body).pickup(this);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
