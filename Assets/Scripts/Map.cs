using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Map : MonoBehaviour
{
    //The with and height for the map
    int width = 32, height = 32;
    public int Width
    {
        get { return width; }
    }
    public int Height
    {
        get { return height; }
    }

    public List<GameObject> Trees
    {
        get {return trees;}
    }

    //Holds the tile list for the map
    Dictionary<Tile, GameObject> map;
    //Holds the list of trees on the map
    [HideInInspector]
    List<GameObject> trees;
    //The number of the tree sprite used, used later for upgrades
    int treeSprite;
    //A bool to check if themother tree has been destoyed
    [HideInInspector]
    public bool lose = false;

    //Tile Sprite list
    public Sprite[] tileSprites;
    public Sprite[] treeSprites;
    public Sprite[] treeFarmSprites;
    public Sprite[] treeTowerSprites;
    public Sprite[] treeComboSprites;
    public Sprite[] motherTreeSprites;

    //Create the mapHolder object that all the tile will be parented to
    GameObject mapHolder;

    private void Start()
    {
        trees = new List<GameObject>();
    }

    private void Update()
    {
        for (int i = 0; i < trees.Count; i++)
        {
            if (trees[i].GetComponent<Tree>().Health <= 0)
            {
                if (trees[i].GetComponent<Tree>().IsMotherTree)
                {
                    Destroy(trees[i]);
                    trees.RemoveAt(i);
                    lose = true;
                }
                else
                { 
                Destroy(trees[i]);
                trees.RemoveAt(i);
            }
            }
        }
    }

    public void NewMap()
    {
        //Clear the old list of map tiles to prepare it for the new tile entries
        if (map != null) map.Clear();
        //Clear the tree list for the next game cycle.
        if (trees != null) trees.Clear();
        //Destroy the old map holder object and will also destroy thwe conected game objects form the game
        if (mapHolder != null) Destroy(mapHolder);
        //Create a new mapHolder object to be populated by the upcomming loop
        mapHolder = new GameObject();
        //The parent object for the tile and trees
        mapHolder.name = "MapHolder";

        map = new Dictionary<Tile, GameObject>();
        trees = new List<GameObject>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //Create the tile here
                //Set the position of the tile
                Vector2 pos = new Vector2(x, y);
                //Create a tile wit hte new position 
                Tile newTile = new Tile(pos);

                //Create the gameobject for the tile here
                GameObject tile_go = new GameObject();
                tile_go.name = "Grass (" + x + ", " + y + " )";

                //Add the sprite renderer to the object and asign a sprite
                tile_go.AddComponent<SpriteRenderer>().sprite = tileSprites[Random.Range(1, 6)];
                //Set the position of the tile_go
                tile_go.transform.position = new Vector2(x, y);
                //Set the parent of the tile_go
                tile_go.transform.SetParent(mapHolder.transform);
                //Adds the sorting layer for the tile game object
                tile_go.GetComponent<SpriteRenderer>().sortingLayerName = "Tiles";

                //Add the SO and GO to the dictionary
                map.Add(newTile, tile_go);
            }
        }

        AddTree(new Vector2(width / 2, height / 2), true);

        //Make the nine tiles the mother tree ocupies regestir the mother tree as thier tree


    }

    public void AddTree(Vector2 _pos, bool _isMotherTree)
    {
        //Create the gameobject for the tile here
        GameObject tree_go = new GameObject();
        tree_go.name = "Tree (" + _pos.x + ", " + _pos.y + " )";
        //Add the tree script to the tree object
        tree_go.AddComponent<Tree>();
        //Add the tree position to the tree script
        tree_go.GetComponent<Tree>().AddTree(_pos);
        //Add the sprite rendere for the tree
        SpriteRenderer tree_sr = tree_go.AddComponent<SpriteRenderer>();
        //Set the sprite for the tree and save it to the tree script
        treeSprite = Random.Range(1, 6);
        tree_sr.sprite = treeSprites[treeSprite];
        tree_go.GetComponent<Tree>().treeSprite = treeSprite;
        //Set the sorting layer for the tree
        tree_sr.sortingLayerName = "Trees";
        //Add the circle triger to the tree fir tower defence
        CircleCollider2D trigger = tree_go.AddComponent<CircleCollider2D>();
        //the raduis of the circle collider
        trigger.radius = 5;
        //Setbthe collider to a trigger
        trigger.isTrigger = false;
        //Add a rigidbody 2d to the tree to detect other object colliders
        Rigidbody2D rb2d = tree_go.AddComponent<Rigidbody2D>();
        //Set the rigibody to kenimaticas it will not have to move
        rb2d.isKinematic = true;
        //Set the offset so the circle collider is in the middle of the tree sprite
        //trigger.offset = new Vector2(-1, 1);

        if (_isMotherTree)
        {
            //Add a tag to the game object
            tree_go.transform.tag = "MotherTree";

            //Add the sprite renderer to the object and asign a sprite
            treeSprite = 0;
            tree_sr.sprite = motherTreeSprites[treeSprite];
            tree_go.GetComponent<Tree>().IsMotherTree = true;
            tree_go.GetComponent<Tree>().treeSprite = treeSprite;
            tree_go.GetComponent<Tree>().AddTower();
            tree_go.GetComponent<Tree>().AddFarm();

            Tile mTile = GetTileAtPos(_pos + new Vector2(1, -1));
            mTile.CanBuild = false;
            mTile = GetTileAtPos(_pos + new Vector2(1, 0));
            mTile.CanBuild = false;
            mTile = GetTileAtPos(_pos + new Vector2(1, 1));
            mTile.CanBuild = false;
            mTile = GetTileAtPos(_pos + new Vector2(0, -1));
            mTile.CanBuild = false;
            //mTile = GetTileAtPos(_pos);
            //mTile.HasTree = true;
            mTile = GetTileAtPos(_pos + new Vector2(0, 1));
            mTile.CanBuild = false;
            mTile = GetTileAtPos(_pos + new Vector2(-1, -1));
            mTile.CanBuild = false;
            mTile = GetTileAtPos(_pos + new Vector2(-1, 0));
            mTile.CanBuild = false;
            mTile = GetTileAtPos(_pos + new Vector2(-1, 1));
            mTile.CanBuild = false;
        }

        //Set the position of the tile_go
        tree_go.transform.position = new Vector2(_pos.x, _pos.y);
        //Set the parent of the tile_go
        tree_go.transform.SetParent(mapHolder.transform);

      //Add the tree bool on the tile
        Tile tempTile = GetTileAtPos(_pos);
        tempTile.HasTree = true;

        //Add the mother tree to the list when creating a new map
        trees.Add(tree_go);

  
    }

    public Tile GetTileAtPos(Vector2 _pos)
    {
        if ((_pos.x + _pos.y * height) > map.Count)
        {
            return null;
        }
        else
        {
            return map.ElementAt((int)_pos.x + (int)_pos.y * height).Key;
        }
    }

    public GameObject GetTileGO(Tile _tile)
    {
        return map[_tile];
    }

    public bool TileHasTree(Vector2 _pos)
    {
        Tile tempTile = GetTileAtPos(_pos);
        return tempTile.HasTree;
    }

    public Tree GetTreeScript(Vector2 _pos)
    {
        for (int i = 0; i < trees.Count; i++)
        {
            if (_pos == trees[i].GetComponent<Tree>().Pos) return trees[i].GetComponent<Tree>();
        }
        return null;
    }

    public GameObject GetTreeGO(Vector2 _pos)
    {
        for (int i = 0; i < trees.Count; i++)
        {
            if (_pos == trees[i].GetComponent<Tree>().Pos) return trees[i];
        }
        return null;
    }

    public bool TileInBounds(Vector2 _pos)
    {
        if (_pos.x > 0 && _pos.x < width && _pos.y > 0 && _pos.y < height) return true;
        else return false;
    }
}
