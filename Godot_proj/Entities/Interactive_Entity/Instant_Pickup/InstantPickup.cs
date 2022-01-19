using System;
using Godot;

public class InstantPickup : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    [Export] public int magnet_speed_scaler = 20;
    [Export] public float magnet_threshold = 80f;
    [Export] public float initial_speed;
    private Boolean getMagnet;
    private string target = "";
    private  Vector2 vec;

    public void setInitialDirection(Vector2 direction)
    {
        vec = direction;
    }
    public override void _PhysicsProcess(float delta)
    {
        
        if (!getMagnet)
        {
            Position += vec/10 * initial_speed;
            var AreaArray = GetOverlappingAreas();
                 foreach (var body in AreaArray)
                 {
                     if (((Area2D) body).Name == "PlayerMagnetArea"){getMagnet = true;target = ((Area2D) body).GetParent().Name;}
                     else{return;}
                 }

                 initial_speed = (initial_speed - 0) / 10;
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

    public override void _Ready()
    {
        Connect("body_entered",this,nameof(_on_InstantPickup_body_entered));
    }

    public void _on_InstantPickup_body_entered(Node body)
    {
        
        ((Player)body).pickup(this, ((Item)GetNode("Item")));
    }

    
    public void setItem(String id)
    {
        Item item = new Item();
        item.setID(id);
        AddChild(item);
        item._Ready();
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
