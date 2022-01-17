using System;
using Godot;

public class EquipmentSlots : GridContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach (EquipSlot slot in GetChildren())
        {
            slot.Connect("gui_input",slot,"_on_gui_input");
        }
    }

    public Boolean is_Occupied()
    {
        return ( this.GetNodeOrNull("Item")!= null);
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
