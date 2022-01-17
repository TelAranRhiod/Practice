using Godot;
using System;
public class Inventory : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Visible = false; 
        GetParent().
        Connect("Pick_Item", this,nameof(_on_pick_up));
    }

    
    public void _on_pick_up(String id)
    {
        foreach (HolsterSlot holsterSlot in GetNode("Holster_Panel").GetNode("HolsterSlots").GetChildren())
        {
            if (!holsterSlot.is_Occupied())
            {
                GD.Print("1");
                holsterSlot.addItem(id);
                return;
                //
            }
        }
        foreach (ItemSlot itemSlot in GetNode("Inventory_Panel").GetNode("InventorySlots").GetChildren())
        {
            if (!itemSlot.is_Occupied())
            {
                GD.Print("1");
                itemSlot.addItem(id);
                return;
            }
        }
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("open_inventory"))
        {
            toggle_Visibility();
        }
    }

    public void toggle_Visibility()
    {
        GD.Print("toggled");
        Visible = !Visible;
        if (Visible)
        {
            (GetParent() as Player).player_Stop();
        }
        else
        {
            (GetParent() as Player).player_Recover();
        }
        GetParent()._Ready();
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
