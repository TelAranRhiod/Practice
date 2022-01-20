using Godot;
using System;

public class Damaging_Entity : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]public int damage = 10;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("body_entered", this, nameof(on_body_entered));
    }

    public void on_body_entered(Node body)
    {
        if (body is Player)
        {
            (body as Player).receive_Damage(damage);
            GD.Print("receive damage");
            this.QueueFree();
        }
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
