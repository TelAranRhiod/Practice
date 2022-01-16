using Godot;
using System;
using System.Collections;
using System.Drawing.Printing;

public class Player : KinematicBody2D
{
    [Export] public int Speed_Set = 250;
    [Export] public int Sprint_Set = 200;
    private  int moveSpeed = 0;
    private int sprintBonus = 0;
    [Export] public int speed = 0;
    [Export] public float interact_waittime = 1;

    

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

    
    [Signal] public delegate void Interact(Node node);

    [Signal] public delegate void Interact_hold(Node node);

    private Timer t = new Timer();
    private Boolean t_started = false;
    public override void _Ready()
    {
        moveSpeed = Speed_Set;
        sprintBonus = Sprint_Set;
        t.SetWaitTime(interact_waittime);
        t.SetOneShot(true);
        this.AddChild(t);
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("interact"))
        {
            GD.Print("pressed");
            t.Start();
            t_started = true;
        }

        if (t_started && t.GetTimeLeft() <= 0.9)
        {
            var areas = ((Area2D) GetNode("PlayerMagnetArea")).GetOverlappingAreas();
            foreach (var area in areas)
            {
                if(area is InterActOnly){moveSpeed = 0; sprintBonus = 0; break;}
            }
        }
        if (t.IsStopped()&&t_started)
        {
            t_started = false;
            GD.Print("held"); 
            EmitSignal(nameof(Interact_hold),this);
        }
        if (Input.IsActionJustReleased("interact"))
        {
            moveSpeed = Speed_Set;
            sprintBonus = Sprint_Set;
            if (t_started)
            {
                t_started = false;
                GD.Print("justpressed");
                EmitSignal(nameof(Interact), this);
                t.Stop();
                t.SetWaitTime(interact_waittime);
            }
        }
    }

    

    public void pickup(Area2D instance)
    {
        instance.QueueFree();
        GD.Print("Picked up "+ instance);
    }
    
}
