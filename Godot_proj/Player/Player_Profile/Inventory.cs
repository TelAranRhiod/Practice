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
    }

    

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("open_inventory"))
        {
            this.toggle_Visibility();
        }
    }

    public void toggle_Visibility()
    {
        GD.Print("toggled");
        Visible = !Visible;
        if (Visible)
        {
            (this.GetParent() as Player).player_Stop();
        }
        else
        {
            (this.GetParent() as Player).player_Recover();
        }
        GetParent()._Ready();
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
