using Godot;
using System;

public class HolsterSlots : GridContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        foreach (HolsterSlot slot in GetChildren())
        {
            slot.Connect("gui_input",slot,"_on_gui_input");
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
