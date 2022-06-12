using System;
using Godot;
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
    private int Cost = 0;
    private int Worth = 0;
    private int damage = 0;
    public int usage = 0;
    public Boolean hitscan = false;
    public override void _Ready()
    {
        File files = new File();
        files.Open("res://Entities/Interactive_Entity/Items/Item_list.json",File.ModeFlags.Read);
        string text = files.GetAsText();
        var jsonFile =  JSON.Parse(text).Result;
        
        Dictionary ParsedData = jsonFile as Dictionary;
        Dictionary ItemInfo = (ParsedData[ID] as Dictionary);
        
        
        Item_Name = (String)ItemInfo["Item_Name"];
        Image_Directory = (String) ItemInfo["Item_Directory"];
        damage = ((String) ItemInfo["Item_Damage"]).ToInt();
        usage = ((String) ItemInfo["Item_usage"]).ToInt();
        if (((String) ItemInfo["hitscan"]).ToInt() == 1)
        {
            hitscan = true;
        }
        GD.Print(Item_Name+ "loaded with image" + Image_Directory);
        Node child = GetChild(0);
        
        if (child is Sprite)
        {
            Sprite s  = child as Sprite;
            s.Texture = GD.Load(Image_Directory) as Texture;
            var scale = new Vector2(0.07f, 0.07f);
            s.SetScale(scale);
        }

        files.Close();
    }

    public int getUsage()
    {
        return usage;
    }
    public int getDamage()
    {
        return damage;
    }
    public String getID()
    {
        return ID;
    }

    public void setID(String id)
    {
        ID = id;
        _Ready();
    }

    

    
    
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
