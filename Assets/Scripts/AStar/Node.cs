using UnityEngine;
using System;
using System.Collections;

public class Node : IHeapItem<Node>
{

    //The type of tile that will be laid in a specific position.
    public enum TileType
    {
        None,
        Normal,
        Strong,
        Weak,
    }

    //The pointer to a function to be called when the tile type has changed
    Action<Node> cbTileTypeChanged;

    //Bools to check against if the re are items of certain tyoes of on the tile
    public bool walkable, containsExit, containsItem, containsPlayer, containsEnemy;
    /* NOTE needs work as we are giving walkable bool value a public acess above but it needs to activate a function when changed, will check later but have other things to do before that can happen.
    public bool WalkAble
    {
        get
        {
            return walkable;
        }
        set
        {
            walkable = value;
            //Here we can do the same as with tile type changed and check if it is of type weak and if it was and it was set back ty walkable = false that the tile is destroyed or something like that 
        }
    }
    */

    public int gCost;
    public int hCost;

    //Extra cost for calculating the moving cost is the tile is water or muddy (Inserted for later use also mot added to calculation yet)
    int nodeTypeCost;

    //The position of the node
    public Vector2 position;

    //Used to store the parent node before this on, used for path finding
    public Node parent;

    int heapIndex;

    TileType type;

    public TileType Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;

            if (cbTileTypeChanged != null)
            {
                cbTileTypeChanged(this);
            }
            //If the tile gets set to type none it will not be walkable but it it gets set to anything else the tile will be walkable
            if (type != TileType.None)
                walkable = true;
            else
                walkable = false;
        }
    }

    public Node(bool _walkable, Vector2 _position, int _nodeCost)
    {
        walkable = _walkable;
        position = _position;
        nodeTypeCost = _nodeCost;
        type = TileType.None;
        containsExit = false;
        containsItem = false;
        containsPlayer = false;
            containsEnemy = false;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public int HeapIndex
    {
        get { return heapIndex; }
        set { heapIndex = value; }
    }

    public int CompareTo(Node nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }

    public void RegestirTileTypeChangedCallBack(Action<Node> callback)
    {
        cbTileTypeChanged = callback;
    }

}
