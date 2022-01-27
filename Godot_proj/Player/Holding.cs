using Godot;
using System;

public class Holding : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    public void _holding_item(ItemSlot slot)
    {
        Item i = slot.GetNodeOrNull("Item") as Item;
        if (GetChildren().Count >0)
        {
            this.GetChild(0).QueueFree();
        }
        if (i != null)
        {
            AddChild(i.Duplicate());
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
