using Godot;
using System;

public class ManualPickup : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export] public int magnet_speed_scaler = 10;
    private Boolean can_magnet = false;
    // Called when the node enters the scene tree for the first time.
    public override void _PhysicsProcess(float delta)
    {
        var getMagnet = false;
        var AreaArray = GetOverlappingAreas();
        foreach (var body in AreaArray)
        {
            if (((Area2D) body).Name == "PlayerMagnetArea"&&can_magnet)
            {
                getMagnet = true;
                if (can_magnet)
                {
                    Position += (((Node2D) GetParent().GetNode("Player")).Position - Position)/magnet_speed_scaler;
                }
                
            }
        }
    }

    public void _on_ManualPickup_body_entered(Node body)
    {
        GD.Print(body.Name +"body entered");
        if (can_magnet)
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
