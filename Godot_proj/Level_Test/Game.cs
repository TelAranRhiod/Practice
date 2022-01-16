using Godot;
using System;

public class Game : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void spawnItem(String ID, Vector2 position)
    {
        var InstantPicksourse =(PackedScene) ResourceLoader.Load(
            "res://Entities/Interactive_Entity/Instant_Pickup/InstantPickup.tscn") ;
        InstantPickup instantPickup = InstantPicksourse.Instance() as InstantPickup;
        var Itemsourse =(PackedScene) ResourceLoader.Load(
            "res://Entities/Interactive_Entity/Items/Simple_Pickable_Item/Item.tscn") ;
        Item item = Itemsourse.Instance() as Item;
        item.setID(ID);
        instantPickup.AddChild(item);
        this.AddChild(instantPickup);
        instantPickup.Position = position;
        //instantPickup.Position = position as Pos;
    }

    public Vector2 getPlayerPosition()
    {
       return (this.GetNode("Player") as Player).Position;
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
