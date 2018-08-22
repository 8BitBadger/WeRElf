//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//public class PathFinding : MonoBehaviour
//{

//    Grid grid;
//    //If true then the tile type will be concidered in the f cost of the tile
//    public bool conciderTileType = false;

//    List<Node> path = new List<Node>();

//    void Awake()
//    {
//        UpdateGrid();
//    }

//    public void UpdateGrid()
//    {
//        grid = GameManager.instance.mainGrid;
//    }

//    public bool FindPath(Vector2 startPos, Vector2 targetPos)
//    {
//        Node startNode = grid.NodeFromWorldPoint(startPos);
//        Node targetNode = grid.NodeFromWorldPoint(targetPos);

//        //Make sure that the grid.mapGrid.Length returns the total amount of tiles in the map eg. 100X100
//        Heap<Node> openSet = new Heap<Node>(grid.mapGrid.Length);
//        HashSet<Node> closedSet = new HashSet<Node>();

//        openSet.Add(startNode);

//        //int checkedTiles = 0;

//        while (openSet.Count > 0)
//        {

//            Node currentNode = openSet.RemoveFirst();

//            closedSet.Add(currentNode);

//            if (currentNode == targetNode)
//            {
//                RetracePath(startNode, targetNode);
//                return true;
//            }
//            // else if (currentNode != targetNode && checkedTiles > 0 && openSet.Count == 0) {
//            //	print ("Can't find path forcing exit");
//            //	return false;
//            //}

//            foreach (Node neighbour in grid.GetNeighbours(currentNode))
//            {
//                if (!neighbour.walkable || closedSet.Contains(neighbour))
//                {
//                    continue;
//                }

//                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
//                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
//                {
//                    neighbour.gCost = newMovementCostToNeighbour;
//                    neighbour.hCost = GetDistance(neighbour, targetNode);

//                    neighbour.parent = currentNode;

//                    if (!openSet.Contains(neighbour))
//                    {
//                        openSet.Add(neighbour);
//                    }
//                }
//            }
//            //checkedTiles++;
//        }
//        //print ("Cannot find path exiting find path");
//        return false;
//    }

//    public bool FindPath(Node startNode, Node targetNode)
//    {
//        //Make sure that the grid.mapGrid.Length returns the total amount of tiles in the map eg. 100X100
//        Heap<Node> openSet = new Heap<Node>(grid.mapGrid.Length);
//        HashSet<Node> closedSet = new HashSet<Node>();

//        openSet.Add(startNode);

//        //int checkedTiles = 0;

//        while (openSet.Count > 0)
//        {

//            Node currentNode = openSet.RemoveFirst();

//            closedSet.Add(currentNode);

//            if (currentNode == targetNode)
//            {
//                RetracePath(startNode, targetNode);
//                return true;
//            }
//            // else if (currentNode != targetNode && checkedTiles > 0 && openSet.Count == 0) {
//            //	print ("Can't find path forcing exit");
//            //	return false;
//            //}

//            foreach (Node neighbour in grid.GetNeighbours(currentNode))
//            {
//                if (!neighbour.walkable || closedSet.Contains(neighbour))
//                {
//                    continue;
//                }

//                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
//                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
//                {
//                    neighbour.gCost = newMovementCostToNeighbour;
//                    neighbour.hCost = GetDistance(neighbour, targetNode);

//                    neighbour.parent = currentNode;

//                    if (!openSet.Contains(neighbour))
//                    {
//                        openSet.Add(neighbour);
//                    }
//                }
//            }
//            //checkedTiles++;
//        }
//        //print ("Cannot find path exiting find path");
//        return false;
//    }

//    void RetracePath(Node startNode, Node endNode)
//    {
//        path.Clear();
//        Node currentNode = endNode;

//        while (currentNode != startNode)
//        {
//            path.Add(currentNode);
//            currentNode = currentNode.parent;
//        }
//        path.Reverse();

//        grid.path = path;
//    }

//    public List<Node> GetPath()
//    {
//        return path;
//    }

//    int GetDistance(Node nodeA, Node nodeB)
//    {
//        int dstX = (int)Mathf.Abs(nodeA.position.x - nodeB.position.x);
//        int dstY = (int)Mathf.Abs(nodeA.position.y - nodeB.position.y);

//        if (!grid.moveDiagonaly)
//        {
//            if (dstX > dstY)
//            {
//                return dstY + 10 * (dstX - dstY);
//            }
//            else
//            {
//                return dstX + 10 * (dstY - dstX);
//            }
//        }
//        else
//        {
//            if (dstX > dstY)
//            {
//                return 14 * dstY + 10 * (dstX - dstY);
//            }
//            else
//            {
//                return 14 * dstX + 10 * (dstY - dstX);
//            }
//        }
//    }
//}
