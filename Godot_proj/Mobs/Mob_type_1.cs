using Godot;
using System;
using System.Threading.Tasks;
using Array = Godot.Collections.Array;

public class Mob_type_1 : KinematicBody2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Vector2 motion = new Vector2();
    [Export] public int move_speed = 100;
    [Export] public int aware_Length;
    [Export] public int attack_interval = 1;

    [Export] public int health = 100;
    [Export] public int[] containsLoot_IDs = null;
    private double attack_Reach = 100;
    private Player target;
    public Timer attack_Timer;
    private Boolean attack_ready = false;
    private Boolean target_attackable = false;
    
    public Timer Awareness_CountDown;
    [Signal] public delegate void mob_spawn_manual(Item i, Vector2 position);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        attack_Timer = new Timer();
        AddChild(attack_Timer);
        attack_Timer.SetWaitTime(attack_interval);
        attack_Timer.Start();
        attack_Timer.Connect("timeout", this, "attack_timeout");
        Connect(nameof(mob_spawn_manual), GetTree().GetRoot().GetNode("Game"), "spawnItem_Manual");
    }


    public override void _Process(float delta)
    {
        if (target_attackable && attack_ready)
        {
            target.receive_Damage(10);
            attack_ready = false;
            attack_Timer.SetWaitTime(attack_interval);
            attack_Timer.Start();
        }
    }

    public void receive_damage(int d)
    {
        if (d < health)
        {
            health -= d;
        }
        else
        {
            dies();
        }
    }

    private void dies()
    {
        QueueFree();
    }

    public void attack_timeout()
    {
        GD.Print("ready attack");
        attack_ready = true;
        attack_Timer.Stop();
        attack_Timer.SetWaitTime(attack_interval);
    }

    public void _on_attact_radius_body_entered(Node p)
    {
        if (p == target)
        {
            target_attackable = true;
        }
    }

    public void _on_attact_radius_body_exited(Node p)
    {
        if (p == target)
        {
            target_attackable = false;
        }
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
    
    //TODO!
    //public void drop_loot(){}

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
