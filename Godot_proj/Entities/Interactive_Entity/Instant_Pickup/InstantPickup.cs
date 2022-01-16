using Godot;
using System;

public class InstantPickup : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export] public int magnet_speed_scaler = 20;
    [Export] public float magnet_threshold = 50f;
    [Export] public float initial_speed = 0;
    private Boolean getMagnet = false;
    private string target = "";
    private  Vector2 vec = new Vector2();
    public override void _PhysicsProcess(float delta)
    {
        
        if (!getMagnet)
        {
            Position += vec * initial_speed;
            var AreaArray = GetOverlappingAreas();
                 foreach (var body in AreaArray)
                 {
                     if (((Area2D) body).Name == "PlayerMagnetArea"){getMagnet = true;target = ((Area2D) body).GetParent().Name;}
                     else{return;}
                 }
        }else {
            vec =  ((Node2D)GetParent().GetNode(target)).Position - Position;
            if (vec.Length() >= magnet_threshold)
            {
                getMagnet = false;
                target = "";
                return;
            }
            Position += (((Node2D) GetParent().GetNode(target)).Position - Position)/magnet_speed_scaler;
            initial_speed =  (vec.Length() / magnet_speed_scaler)/100f;
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
