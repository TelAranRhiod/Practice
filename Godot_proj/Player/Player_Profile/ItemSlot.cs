using Godot;
using System;
public class ItemSlot : PanelContainer
{
    
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    

   
    public Boolean is_Occupied()
    {
        return ( this.GetNodeOrNull("Item")!= null);
    }
    public void addItem(Item i)
    {
        Item item = i.Duplicate() as Item;
        AddChild(item);
        item._Ready();
        Vector2 adjust_position = new Vector2(30,30);
        item.Position += adjust_position;
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
