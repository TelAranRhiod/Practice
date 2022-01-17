using Godot;
using System;
using Godot.Collections;
using Object = Godot.Object;

public class Inventory : Control
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Item ChosenItem = null;

    private Item temp = null;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Visible = false; 
        GetParent().
        Connect("Pick_Item", this,nameof(_on_pick_up));

        foreach (var node in GetNode("Inventory_Panel/InventorySlots").GetChildren())
        {
            ItemSlot slot = node as ItemSlot;
            slot.Connect("gui_input", this, "_on_gui_input", new Godot.Collections.Array() {slot});
        }
        foreach (var node in GetNode("Holster_Panel/HolsterSlots").GetChildren())
        {
            ItemSlot slot = node as ItemSlot;
            slot.Connect("gui_input", this, "_on_gui_input", new Godot.Collections.Array() {slot});
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (ChosenItem != null)
        {
            ChosenItem.Position = GetLocalMousePosition();
        }
    }

    public void _on_gui_input(InputEvent input, ItemSlot slot)
    {
        if (input is InputEventMouseButton && input.IsPressed() && (input as InputEventMouseButton).ButtonIndex == 1)
        {
            GD.Print(slot.Name);
            if (slot.is_Occupied()&& ChosenItem == null)
            {
                GD.Print("have chosen");
                var item = slot.GetNodeOrNull("Item") ;
                ChosenItem = item.Duplicate() as Item;
                ChosenItem.Position = GetLocalMousePosition();
                this.AddChild(ChosenItem);
                item.QueueFree();
            }
            else if (!slot.is_Occupied() && ChosenItem != null)
            {
                var i = ChosenItem.Duplicate() as Item;
                i.Position = new Vector2(30, 30);
                slot.AddChild(i);
                ChosenItem.QueueFree();
                ChosenItem = null;
                
            }
            
        }
    }
    
    public void _on_pick_up(Item i)
    {
        pickup(i);
    }

    private void pickup(Item i)
    {
        foreach (ItemSlot itemSlot in GetNode("Holster_Panel").GetNode("HolsterSlots").GetChildren())
        { if (!itemSlot.is_Occupied())
            {
                GD.Print("1"); 
                itemSlot.addItem(i);
                return;
            }
        }
        foreach (ItemSlot itemSlot in GetNode("Inventory_Panel").GetNode("InventorySlots").GetChildren())
        {
            if (!itemSlot.is_Occupied())
            {
                GD.Print("1"); 
                itemSlot.addItem(i); 
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
            (GetParent() as Player).SetProcess(false);
        }
        else
        {
            (GetParent() as Player).player_Recover();
            (GetParent() as Player).SetProcess(true);
        }
        GetParent()._Ready();
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
