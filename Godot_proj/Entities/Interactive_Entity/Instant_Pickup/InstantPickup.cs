using Godot;
using System;

public class InstantPickup : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export] public int magnet_speed_scaler = 10;
    [Export] public Boolean can_magnet = true;
    public override void _PhysicsProcess(float delta)
    {
        var getMagnet = false;
        if (getMagnet = false)
        {
            //other physical process
        }

        var AreaArray = GetOverlappingAreas();
        foreach (var body in AreaArray)
        {
            if (((Area2D) body).Name == "PlayerMagnetArea"&&can_magnet)
            {
                getMagnet = true;
                Position += (((Node2D) GetParent().GetNode("Player")).Position - Position)/magnet_speed_scaler;
            }
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
