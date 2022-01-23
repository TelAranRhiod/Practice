using Godot;
using System;

public class HolsterSlots_UI : GridContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private int Current_Slot;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Current_Slot = 0;
    }

    public override void _Process(float delta)
    {
        if (Current_Slot < 5 )
        {
            if (Input.IsActionJustReleased("scroll_up"))
            {
                Current_Slot++;
            }
        }

        if (Current_Slot > 0)
        {
            if (Input.IsActionJustReleased("scroll_down"))
            {
                Current_Slot--;
            }
        }
        GD.Print(Current_Slot.ToString());
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
