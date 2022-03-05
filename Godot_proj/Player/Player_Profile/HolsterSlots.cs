using Godot;
using System;

public class HolsterSlots : GridContainer
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";\
    private int Current = 0;
    [Signal]
    public delegate void HoldCurrent(ItemSlot slot);
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("HoldCurrent",GetTree().GetRoot().GetNode("Game").GetNode("Player").GetNode("Holding"),"_holding_item");
        emitHolding();
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustReleased("scroll_down")&& Current<5)
        {
            Current++;
            emitHolding();
        }

        if (Input.IsActionJustReleased("scroll_up") && Current >0)
        {
            Current--;
            emitHolding();
        }
    }

    public void emitHolding()
    {
        var array = GetChildren();
        ItemSlot s = array[Current] as ItemSlot;
        EmitSignal(nameof(HoldCurrent), s);
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
