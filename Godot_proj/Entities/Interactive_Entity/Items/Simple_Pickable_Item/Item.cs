using Godot;
using System;
using Godot.Collections;

public class Item : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";
    [Export]
    public string ID = "1";
    private String Item_Name;
    private String Image_Directory ;

    
    public override void _Ready()
    {
        Godot.File files = new Godot.File();
        files.Open("res://Entities/Interactive_Entity/Items/Item_list.json",Godot.File.ModeFlags.Read);
        string text = files.GetAsText();
        var jsonFile =  JSON.Parse(text).Result;
        
        Dictionary ParsedData = jsonFile as Dictionary;
        Dictionary ItemInfo = (ParsedData[ID] as Dictionary);
        
        
        Item_Name = (String)ItemInfo["Item_Name"];
        Image_Directory = (String) ItemInfo["Item_Directory"];
        GD.Print(Item_Name+ "loaded with image" + Image_Directory);
        Node child = this.GetChild(0);
        
        if (child is TextureRect)
        {
            TextureRect texture = child as TextureRect;
            texture.Texture = GD.Load(Image_Directory) as Texture;
        }
    }

    public String getID()
    {
        return ID;
    }

    public void setID(String id)
    {
        ID = id;
        this._Ready();
    }

    

    
    
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
