using Godot;
using System;
public class ItemSlot : PanelContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    

    public void _on_gui_input(InputEvent input)
    {
        if (Input.IsActionJustPressed("left_click"))
        {
            GD.Print(Name);
        }
    }
    public Boolean is_Occupied()
    {
        return ( this.GetNodeOrNull("Item")!= null);
    }
    public void addItem(String id)
    {
        var Itemsourse =(PackedScene) ResourceLoader.Load(
            "res://Entities/Interactive_Entity/Items/Simple_Pickable_Item/Item.tscn") ;
        Item item = Itemsourse.Instance() as Item;
        item.setID(id);
        AddChild(item);
        Vector2 adjust_position = new Vector2(43,44);
        item.Position += adjust_position;
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
