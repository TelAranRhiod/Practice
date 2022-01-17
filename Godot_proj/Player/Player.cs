using System;
using Godot;

public class Player : KinematicBody2D
{
    [Export] public int Speed_Set = 250;
    [Export] public int Sprint_Set = 200;
    private  int moveSpeed;
    private int sprintBonus;
    [Export] public int speed;
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
    private Boolean t_started;

    public override void _Ready()
    {
        moveSpeed = Speed_Set;
        sprintBonus = Sprint_Set;
        t.SetWaitTime(interact_waittime);
        t.SetOneShot(true);
        if (t.GetParent()==null)
        {
            AddChild(t);
        }
    }

    public override void _Process(float delta)
    {
        
        if (Input.IsActionJustPressed("interact"))
        {
            GD.Print("pressed");
            t.Start();
            t_started = true;
            //((Game) GetParent()).spawnItem("1",Position);
        }

        if (t_started && t.GetTimeLeft() <= 0.9)
        {
            var areas = ((Area2D) GetNode("PlayerMagnetArea")).GetOverlappingAreas();
            foreach (var area in areas)
            {
                if(area is InterActOnly){player_Stop(); break;}
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
            player_Recover();
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

    public void player_Stop()
    {
        Speed_Set = 0; 
        Sprint_Set = 0;
        _Ready();
    }

    public void player_Recover()
    {
        Speed_Set = 250; 
        Sprint_Set = 200;
        _Ready();
    }
    [Signal] public delegate void Pick_Item(String ID);
    public void pickup(Area2D instance,String ID)
    {
        instance.QueueFree();
        EmitSignal(nameof(Pick_Item),ID);
        
        GD.Print("Picked up "+ instance + ID);
    }
    
}
