using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameGrid : MonoBehaviour
{

    public GameObject hexCellPrototype; 
    public GameObject[,] tiles = new GameObject[7,7];
    public float k2;

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
                else if (i == 6 && j != 3)
                {
                    tiles[i,j] = null;
                }
                else
                {
                    float k = 1.4f;
                    float d_x = (float)(j - 3);
                    float d_y = (float)(i - 3);

                    Vector3 initial_postion;
                    initial_postion = new Vector3(k * d_x * (float)Math.Cos(Math.PI / 6), k * (d_y  + Math.Abs(d_x)*(float)Math.Sin(Math.PI / 6)), 0f);
                    GameObject hex = Instantiate(hexCellPrototype, initial_postion, Quaternion.identity);
                    tiles[i, j] = hex;
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
