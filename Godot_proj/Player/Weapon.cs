using Godot;
using System;

public class Weapon : Sprite
{
    


    private void get_mouse()
    {
        LookAt(GetGlobalMousePosition());
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("fire_weapon"))
        {
            shoot();
        }
    }

    public override void _PhysicsProcess(float delta)
    {
        get_mouse();
    }
    

    private void shoot()
    {
        var b =(PackedScene) ResourceLoader.Load(
            "res://Entities/Damaging_Entity/Damaging_Particle.tscn") ;
        Damaging_Entity bullet = b.Instance() as Damaging_Entity;
        GetParent().Owner.AddChild(bullet);
        bullet.setVelocity(100.0f);
        GD.Print(bullet.GlobalPosition.x.ToString() + " " + bullet.GlobalPosition.y.ToString() + b.ToString());
        bullet.Transform = (GetNode("Muzzle") as Position2D).GlobalTransform;
    }
}
