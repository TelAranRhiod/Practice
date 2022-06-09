using Godot;
using System;

public class Mob_type_1 : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Vector2 motion = new Vector2();
    [Export]
    public int move_speed = 100;
    [Export]
    public int aware_Length;
    [Export] 
    public int attack_interval;
    private double attack_Reach;
    private Player target;

    public Timer Awareness_CountDown;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void obtainTarget(Player p)
    {
        target = p;
    }
    private void loseTarget(Player p)
    {
        target = null;
    }
    public override void _PhysicsProcess(float delta)
    {
        if (target != null)
        {
            Vector2 dir = target.Position - Position;
            motion.x = (float) (dir.x / Math.Sqrt(Math.Pow(dir.x, 2) + Math.Pow(dir.y, 2)));
            motion.y = (float) (dir.y / Math.Sqrt(Math.Pow(dir.x, 2) + Math.Pow(dir.y, 2)));
            MoveAndCollide(motion.Normalized() * move_speed * delta);
        }
    }

    public void _on_detection_circle_body_entered(Node n)
    {
        if (n is Player && n != target)
        {
            obtainTarget(n as Player);
            move_speed = 100;
        }
        GD.Print(target);
        GD.Print(motion.ToString());
    }
    public void _on_detection_circle_body_exited(Node n)
    {
        
        if (n is Player && n == target)
        {
            loseTarget(n as Player);
            move_speed = 0;
        }
    }
    
    



    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
