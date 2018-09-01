using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    //The position of the tree on the map
    Vector2 pos;

    //The sprite used for the tree
    public int treeSprite;

    //The bool for schecking if the tree ha sa tower in it
    bool hasTower = false;
    //If the tree is a mother tree
    bool isMotherTree = false;
    //Normally only one tower per tre exept for the mother tree
    int noOfTowers = 0;
    //If the tree is a farm or not;
    bool hasFarm = false;
    //How may farms the tree has normaly only for the mother tree that can have more than one
    int noOfFarms = 0;
    //The max farms and towers the tree can have
    int maxSlots = 3;
    //The health of the tre
    int health = 5;

    //Attacking info
    public int interval = 2;
    float lastAttackTime = 0;
    public float lastFoodTime = 0;


    public int Health
    {
        get { return health; }
    }

    public Vector2 Pos
    {
        get { return pos; }
    }

    //Before we return the the has tower we check if the mother tree max slots are full and if they are then we return the has tower
    public bool HasTower
    {
        get { return hasTower; }
    }

    public bool IsMotherTree
    {
        get { return isMotherTree; }
        set { isMotherTree = value; }
    }

    public bool HasFarm
    {
        get { return hasFarm; }
    }

    public int NoOfFarms
    {
        get{return noOfFarms;}
    }

    public void AddTree(Vector2 _pos)
    {
        pos = _pos;
    }

    public void AddTower()
    {
        if (isMotherTree)
        {
            if (noOfTowers < maxSlots)
            {
                hasTower = false;
                noOfTowers += 1;

                if (noOfTowers == maxSlots)
                {
                    hasTower = true;
                }

            }

        }
        else
        {
            if (hasTower)
            {
                return;
            }
            else
            {
                hasTower = true;
                noOfTowers = 1;
            }
        }
    }

    public void AddFarm()
    {
        if (isMotherTree)
        {
            if (noOfFarms < maxSlots)
            {
                hasFarm = false;
                noOfFarms += 1;

                if (noOfFarms == maxSlots)
                {
                    hasFarm = true;
                }
            }
        }
        else
        {
            if (hasFarm)
            {
                return;
            }
            else
            {
                hasFarm = true;
                noOfFarms = 1;
            }
        }
    }

    public void TakeDamage()
    {
        health -= 1;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //If there are no towers built then we just return out of the function
        if (noOfTowers <= 0) return;

        if ((Time.time - lastAttackTime) > interval)
        {
            //once the function is run we set the last attack time to the current time to reset thwe timer
            lastAttackTime = Time.time;
            if (col.tag == "Orc")
            {
                col.GetComponent<Orc>().TakeDamage();
                col.GetComponent<Orc>().SetTarget(transform);
            }
        }
    }
}
