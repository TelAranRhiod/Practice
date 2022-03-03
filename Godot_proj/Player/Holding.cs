using Godot;
using System;

public class Holding : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //(GetNode("AimSight") as Position2D).Position = new Vector2(-10, 10);
       //this.Position = new Vector2(0, 0);
    }
    private void get_mouse()
    {
        LookAt(GetGlobalMousePosition());
    }

    public override void _PhysicsProcess(float delta)
    {
       get_mouse();
    }

    public void _holding_item(ItemSlot slot)
    {
        Item i = slot.GetNodeOrNull("Item") as Item;
        if (GetChildren().Count >1)
        {
            this.GetChild(1).QueueFree();
        }
        if (i != null)
        {
            AddChild(i.Duplicate());
        }
    }

    public void use()
    {
        use_item();
    }

    private void use_item()
    {
        var b =(PackedScene) ResourceLoader.Load(
            "res://Entities/Damaging_Entity/Damaging_Particle.tscn") ;
        Damaging_Entity bullet = b.Instance() as Damaging_Entity;
        
        bullet.SetOrigin(GetParent());
        bullet.setVelocity(100.0f);
        
        bullet.Transform = (GetNode("AimSight") as Position2D).GlobalTransform;
        GetTree().CurrentScene.AddChild(bullet);
        bullet.Position = new Vector2(bullet.Position.x - 25, bullet.Position.y - 15);
        GD.Print(bullet.Position.ToString());
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
