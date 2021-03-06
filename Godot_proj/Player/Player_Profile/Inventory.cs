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
    
    [Signal] 
    public delegate void DropItem(Item i);

    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Visible = false; 
        GetTree().GetRoot().GetNode("Game").GetNode("Player").
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
        Connect("DropItem", GetTree().GetRoot().GetNode("Game").GetNode("Player"),"_drop_Item");
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
            GD.Print(slot.Name + " Item is" + slot.GetNodeOrNull("Item"));
            var child = slot.GetNodeOrNull("Item") as Item;
            if (slot.is_Occupied()&& ChosenItem == null)
            {
                GD.Print("have chosen");
                
                ChosenItem = child.Duplicate() as Item;
                ChosenItem.Position = GetLocalMousePosition();
                this.AddChild(ChosenItem);
                child.QueueFree();
            }
            else if (!slot.is_Occupied() && ChosenItem != null)
            {
                var i = ChosenItem.Duplicate() as Item;
                i.Position = new Vector2(30, 30);
                slot.AddChild(i);
                ChosenItem.QueueFree();
                ChosenItem = null;
                
            }
            else if (slot.is_Occupied() && ChosenItem != null)
            {
                RemoveChild(ChosenItem);
                slot.RemoveChild(child);
                var item = ChosenItem.Duplicate() as Item;
                item.Position = new Vector2(30, 30);
                slot.AddChild(item);
                ChosenItem = child.Duplicate() as Item;
                ChosenItem.Position = GetLocalMousePosition();
                AddChild(ChosenItem);
                child.QueueFree();
            }
        }
        else if (input is InputEventMouseButton && input.IsPressed() && (input as InputEventMouseButton).ButtonIndex == 2)
        {
            var child = slot.GetNodeOrNull("Item") as Item;
            if (Input.IsActionPressed("sprint"))
            {
                if (slot.is_Occupied())
                {
                    EmitSignal(nameof(DropItem), child.Duplicate() as Item );
                    child.QueueFree();
                }
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
                refreshHolster();
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

    private void refreshHolster()
    {
        (GetNode("Holster_Panel").GetNode("HolsterSlots") as HolsterSlots).emitHolding();
    }
    public Boolean is_full()
    {
        foreach (var node in GetNode("Inventory_Panel/InventorySlots").GetChildren())
        {
            ItemSlot slot = node as ItemSlot;
            if (!slot.is_Occupied())
            {
                return false;
            }
        }
        foreach (var node in GetNode("Holster_Panel/HolsterSlots").GetChildren())
        {
            ItemSlot slot = node as ItemSlot;
            if (!slot.is_Occupied())
            {
                return false;
            }
        }
        return true;
    }
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("open_inventory"))
        {
            toggle_Visibility();
           refreshHolster();
        }
    }

    public void toggle_Visibility()
    {
        Player player = GetTree().GetRoot().GetNode("Game").GetNode("Player") as Player;
        GD.Print("toggled");
        Visible = !Visible;
        if (Visible)
        {
            player.player_Stop();
            player.SetProcess(false);
        }
        else
        {
            if (ChosenItem != null)
            {
                EmitSignal(nameof(DropItem), ChosenItem.Duplicate() as Item);
                ChosenItem.QueueFree();
                ChosenItem = null;
            }
            player.player_Recover();
            player.SetProcess(true);
        }
        player._Ready();
    }
    
}
