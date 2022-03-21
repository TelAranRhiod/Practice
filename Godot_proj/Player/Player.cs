using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Godot;
using Array = Godot.Collections.Array;
using Vector2 = Godot.Vector2;

public class Player : KinematicBody2D
{
    [Export] public int Speed_Set = 250;
    [Export] public int Sprint_Set = 200;
    [Export] public int Health = 100;
    private  int moveSpeed;
    private int sprintBonus;
    [Export] public int speed;
    [Export] public float interact_waittime = 1;
    private Timer Attaack_CountDown = new Timer();
    private double Attack_Interval;

    public Array expandable_Items = new Array();
    public List<int>  expandable_Items_quantity= new List<int>();

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
        if (t.GetParent() == null)
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
                //if (!((Inventory) GetNode("Inventory")).is_full())
                {
                    EmitSignal(nameof(Interact), this);
                }
                t.Stop();
                t.SetWaitTime(interact_waittime);
            }
        }

        if (Input.IsActionJustPressed("fire_weapon"))
        {
            Holding hold = GetNode("Holding") as Holding;
            hold.use();
        }
        
    }

    public Boolean fullIven()
    {
        return ((Inventory)GetParent().GetNode("UI").GetNode("Inventory")).is_full();
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
    [Signal] public delegate void Pick_Item(Item i);
    public void pickup(Area2D instance,Item i)
    {
        EmitSignal(nameof(Pick_Item),i);
        instance.QueueFree();
        GD.Print("TestSignal "+ instance + i);
    }
    
    public void get_instant(Area2D instance,Item i)
    {
        instance.QueueFree();
        GD.Print(i.ID);
        int indexing = 0;
        foreach (Item item in expandable_Items)
        {
            if (item.ID == i.ID)
            {
                indexing = expandable_Items.IndexOf(item);
                expandable_Items_quantity[indexing] += 1;
                GD.Print(expandable_Items.ToString());
                GD.Print(expandable_Items_quantity[0].ToString());
                return;
            }
        }
        expandable_Items.Add(i.Duplicate() as Item);
        expandable_Items_quantity.Add(1);
        GD.Print(expandable_Items.ToString());
        GD.Print(expandable_Items_quantity[0].ToString() );
        
    }
    public void receive_Damage(int damage)
    {
        if ( 100 > Health - damage && Health - damage > 0)
        {
            Health = Health - damage;
            GD.Print("Taken Damage" + Health.ToString());
        }
        else if(Health - damage < 0)
        {
            Health = 0;
            GD.Print("Player Health is 0");
        }
        else if (Health - damage > 100)
        {
            Health = 100;
            GD.Print("Player full health");
        }
        GD.Print(Health.ToString());
    }
    public int get_Health()
    {
        return Health;
    }
    
    public void _drop_Item(Item item)
    {
        ((Game) GetParent()).spawnItem(item,Position);
    }
}
