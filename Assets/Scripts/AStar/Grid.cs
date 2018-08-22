using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{

    public bool moveDiagonaly = false;

    //2D vector of nodes
    [HideInInspector]
    public Node[,] mapGrid;

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (!moveDiagonaly)
                {
                    if ((x == 0 && y == 0) || (x == 1 && y == 1) || (x == -1 && y == -1) || (x == 1 && y == -1) || (x == -1 && y == 1))
                        continue;
                }
                else
                {
                    if (x == 0 && y == 0)
                        continue;
                }

                int checkX = (int)node.position.x + x;
                int checkY = (int)node.position.y + y;

                if (checkX >= 0 && checkX < mapGrid.GetLength(0) && checkY >= 0 && checkY < mapGrid.GetLength(1))
                {
                    neighbours.Add(mapGrid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    public Node NodeFromWorldPoint(Vector2 worldPosition)
    {
        return mapGrid[(int)worldPosition.x, (int)worldPosition.y];
    }

    public List<Node> path;

}
