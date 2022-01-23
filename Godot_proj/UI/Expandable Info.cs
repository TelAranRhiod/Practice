using Godot;
using System;

public class Expandable_Info : MarginContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    
    
    public void add_Item(Item i, int quantity )
    {
       HBoxContainer box = GetNode("Top").GetNode("Top_Box") as HBoxContainer;

       Item item = i.Duplicate() as Item;
       box.AddChild(item);
       Label label = new Label();
       label.SetText(quantity.ToString());
       box.AddChild(label);
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
