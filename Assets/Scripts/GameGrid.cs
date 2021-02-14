using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{

    public HexCell hexTilePrototype;
    public HexCell[,] tiles = new HexCell[7,7];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (i == 4 && (j < 1 || j > 5))
                {
                    tiles[i,j] = null;
                }
                else if (i == 5 && (j < 2 || j > 4))
                {
                    tiles[i,j] = null;
                }
                else if (i == 6 && j != 4)
                {
                    tiles[i,j] = null;
                }
                else
                {
                    tiles[i,j] = Instantiate(hexTilePrototype);
                }
            }
        }    
        

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
