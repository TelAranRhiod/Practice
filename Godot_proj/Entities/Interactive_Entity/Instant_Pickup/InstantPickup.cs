using Godot;
using System;

public class InstantPickup : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export] public int magnet_speed_scaler = 20;
    
    private Boolean getMagnet = false;
    private string target = "";
    public override void _PhysicsProcess(float delta)
    {
        
        if (!getMagnet)
        {
            var AreaArray = GetOverlappingAreas();
                 foreach (var body in AreaArray)
                 {
                     if (((Area2D) body).Name == "PlayerMagnetArea"){getMagnet = true;target = ((Area2D) body).GetParent().Name;}
                     else{return;}
                 }
            //other physical process
        }else {
            Position += (((Node2D) GetParent().GetNode(target)).Position - Position)/magnet_speed_scaler;
        }
    }

    public void _on_InstantPickup_body_entered(Node body)
    {
        ((Player)body).pickup(this);
    }
    
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
