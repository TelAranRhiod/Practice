using Godot;
using System;
using System.Collections;
using Array = Godot.Collections.Array;

public class Crate : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    private Boolean is_Opened = false;
    private Array itemlist = new Array();
    private RandomNumberGenerator randi = new RandomNumberGenerator();
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect(nameof(crate_spawn_item), GetTree().GetRoot().GetNode("Game"), "spawnItem_instant");
        
        
         is_Opened = false;
         int item_number = randi.RandiRange(1,3);
         
         for (int i = 0; i<= item_number;i++)
         {
             var Picksourse =(PackedScene) ResourceLoader.Load(
                 "res://Entities/Interactive_Entity/Items/Simple_Pickable_Item/Item.tscn") ;
             Item item = Picksourse.Instance() as Item;
             item.setID((new RandomNumberGenerator().RandiRange(0, 90) / 30).ToString());
            itemlist +=  new Array(item);
         }
    }
    
    [Signal] public delegate void crate_spawn_item(Item i, Vector2 position);
    public void act()
    {
        GD.Print("acting");
        foreach (Item item in itemlist)
        {
            Vector2 position = new Vector2();
            position = (GetParent() as InterActOnly).Position + new Vector2(randi.RandiRange(-10,10), randi.RandiRange(-10,10));
            EmitSignal(nameof(crate_spawn_item),item.Duplicate() as Item, position);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
