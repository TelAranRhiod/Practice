using System;
using Godot;

public class ManualPickup : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export] public int magnet_speed_scaler = 20;
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
        if (getMagnet)
        {
            Position += (((Node2D) GetParent().GetNode(target)).Position - Position) / magnet_speed_scaler;
        }
        else
        {
            Position += vec/10 * initial_speed;
            
            initial_speed = (initial_speed - 0) / 10;
        }
        
    }
    public override void _Ready()
    {
        GetParent().GetNode("Player").Connect("Interact", this, nameof(_on_Player_Interact));
        Connect("body_entered", this, nameof(_on_ManualPickup_body_entered));
    }
    public void _on_Player_Interact(Node node)
    {
        var AreaArray = GetOverlappingAreas();
        foreach (var body in AreaArray)
        {
            if (((Area2D) body).GetParent() == node)
            {
                target = node.Name;
                getMagnet = true;
                
                
                foreach (Node bod in GetOverlappingBodies())
                {
                    if (target == bod.Name )
                    {
                        ((Player) node).pickup(this,((Item)GetNode("Item")));
                    }
                }
                
            }
        }
        
    }
    public void _on_ManualPickup_body_entered(Node body)
    {
        if (getMagnet)
        {
            ((Player)body).pickup(this, ((Item)GetNode("Item")));
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
