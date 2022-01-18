using System;
using Godot;

public class Game : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void spawnItem(Item i, Vector2 position)
    {
        var Picksourse =(PackedScene) ResourceLoader.Load(
            "res://Entities/Interactive_Entity/Manual_Pickup/ManualPickup.tscn") ;
        ManualPickup Pickup = Picksourse.Instance() as ManualPickup;
        this.AddChild(Pickup);
        i.Position = new Vector2(0, 0);
        Pickup.AddChild(i);
        Pickup.Position = position;
        //instantPickup.Position = position as Pos;
    }

    public void spawnItem_instant(Item i, Vector2 position)
    {
        RandomNumberGenerator randi = new RandomNumberGenerator();
        var Picksourse =(PackedScene) ResourceLoader.Load(
            "res://Entities/Interactive_Entity/Instant_Pickup/InstantPickup.tscn") ;
        InstantPickup Pickup = Picksourse.Instance() as InstantPickup;
        this.AddChild(Pickup);
        i.Position = new Vector2(0, 0);
        
        Pickup.initial_speed =  new RandomNumberGenerator().RandiRange(0,2);
        Pickup.setInitialDirection(new Vector2(randi.RandiRange(-10, 10), randi.RandiRange(-10, 10)) );
        Pickup.AddChild(i);
        Pickup.Position = position;
    }

    public Vector2 getPlayerPosition()
    {
       return (GetNode("Player") as Player).Position;
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
