using Godot;
using System;

public class Player : KinematicBody2D
{

    [Export] public int moveSpeed = 250;
    [Export] public int sprintBonus = 150;
    [Export] public int speed = 0;
    
    public override void _PhysicsProcess(float delta)
    {
        Vector2 motion = new Vector2();
        motion.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        motion.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        speed = moveSpeed;
        if (Input.GetActionStrength("sprint")!=0)
        {
            speed = moveSpeed + sprintBonus;
            GD.Print("sprinting");
        }
        MoveAndCollide(motion.Normalized() * speed * delta);
    }

    public void _on_Body_entered(Node body)
    {
        GD.Print("calling signal enter a interactive object");
    }

    public int getPlayerSpeed()
    {
        return speed;
    }
}
