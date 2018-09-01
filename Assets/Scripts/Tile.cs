using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile
{
    //Just holds the information for the tiles to be made in the map

    Vector2 pos;

    bool hasTree = false;
    bool canBuild = true;

    public bool HasTree
    {
        get { return hasTree; }
        set { hasTree = value; }
    }

    public bool CanBuild
    {
        get {return canBuild;}
        set{canBuild = value;}
    }

    public Tile(Vector2 _pos)
    {
        pos = _pos;
    }
}
