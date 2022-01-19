using Godot;
using System;
using System.Collections;
using System.Threading;
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
        Connect(nameof(crate_spawn_manual), GetTree().GetRoot().GetNode("Game"), "spawnItem_Manual");
        is_Opened = false;

        
        randi.Randomize();
        int item_number = randi.RandiRange(0,3);
         
         for (int i = 0; i< item_number;i++)
         {
             var Picksourse =(PackedScene) ResourceLoader.Load(
                 "res://Entities/Interactive_Entity/Items/Simple_Pickable_Item/Item.tscn") ;
             Item item = Picksourse.Instance() as Item;
             randi.Randomize();
              if (get_Pool(30))
              {
                  item.setID("101");
              }
              else
             {
                 item.setID(randi.RandiRange(1,3).ToString());
             } 
             itemlist +=  new Array(item);
         }
    }
    
    [Signal] public delegate void crate_spawn_item(Item i, Vector2 position);
    [Signal] public delegate void crate_spawn_manual(Item i, Vector2 position);
    public void act()
    {
        if (!is_Opened)
        {
            GD.Print("acting");
            foreach (Item item in itemlist)
            {
                Vector2 position = new Vector2();
                randi.Randomize();
                int x = randi.RandiRange(10, 10);
                randi.Randomize();
                int y = randi.RandiRange(10, 10);
                position = (GetParent() as InterActOnly).Position + new Vector2(x,y);
                
                if(item.ID.Length() < 3)
                { 
                    EmitSignal(nameof(crate_spawn_item),item.Duplicate() as Item, position); 
                }
                else if (item.ID.Length() <= 3)
                {
                    EmitSignal(nameof(crate_spawn_manual),item.Duplicate() as Item, position); 
                }
                
            }
            is_Opened = true;
            if (get_Pool(20))
            {
                this._Ready();
            }
        }
        
    }


    private Boolean get_Pool(float i)
    {
        randi.Randomize();
        float f = randi.RandfRange(0, 100);

        return (f < i);
    }
    
    
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
