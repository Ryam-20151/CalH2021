using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameGrid : MonoBehaviour
{

    public GameObject hexCellPrototype;
    public GameObject channelNodePrototype;
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
                    tiles[i, j] = null;
                }
                else if (i == 5 && (j < 2 || j > 4))
                {
                    tiles[i, j] = null;
                }
                else if (i == 6 && j != 3)
                {
                    tiles[i, j] = null;
                }
                else
                {
                    float k = 1.4f;
                    float d_x = (float)(j - 3);
                    float d_y = (float)(i - 3);

                    Vector3 initial_postion;
                    initial_postion = new Vector3(k * d_x * (float)Math.Cos(Math.PI / 6), k * (d_y + Math.Abs(d_x) * (float)Math.Sin(Math.PI / 6)), 0f);
                    GameObject hex = Instantiate(hexCellPrototype, initial_postion, Quaternion.identity);
                    tiles[i, j] = hex;
                }
            }
        }

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (tiles[i, j] != null)
                {
                    HexCell curr_cell = tiles[i, j].GetComponent<HexCell>();

                    HexCell pos_0_cell = null;

                    if (i + 1 < 7)
                    {
                        if (tiles[i + 1, j] != null)
                        {
                            pos_0_cell = tiles[i + 1, j].GetComponent<HexCell>();
                        }
                    }

                    HexCell pos_1_cell = null;
                    HexCell pos_2_cell = null;

                    if (j < 3)
                    {

                        if (i < 6)
                        {
                            if (tiles[i + 1, j + 1] != null)
                            {
                                pos_1_cell = tiles[i + 1, j + 1].GetComponent<HexCell>();
                            }
                        }

                        if (tiles[i, j] != null)
                        {
                            pos_2_cell = tiles[i, j + 1].GetComponent<HexCell>();
                        }
                    }
                    else
                    {
                        if (j < 6)
                        {
                            if (tiles[i, j + 1] != null)
                            {
                                pos_1_cell = tiles[i, j + 1].GetComponent<HexCell>();
                            }
                            if (i > 0)
                            {
                                if (tiles[i - 1, j + 1] != null)
                                {
                                    pos_2_cell = tiles[i - 1, j + 1].GetComponent<HexCell>();
                                }
                            }
                        }
                    }

                    HexCell pos_3_cell = null;
                    if (i > 0)
                    {
                        if (tiles[i - 1, j] != null)
                        {
                            pos_3_cell = tiles[i - 1, j].GetComponent<HexCell>();
                        }
                    }

                    HexCell pos_4_cell = null;
                    HexCell pos_5_cell = null;

                    if (j > 3)
                    {
                        if (tiles[i, j - 1] != null)
                        {
                            pos_4_cell = tiles[i, j - 1].GetComponent<HexCell>();
                        }

                        if (i < 6)
                        {
                            if (tiles[i + 1, j - 1] != null)
                            {
                                pos_5_cell = tiles[i + 1, j - 1].GetComponent<HexCell>();
                            }
                        }
                    }
                    else
                    {
                        if (j > 0)
                        {
                            if (i > 0)
                            {
                                if (tiles[i - 1, j - 1] != null)
                                {
                                    pos_4_cell = tiles[i - 1, j - 1].GetComponent<HexCell>();
                                }
                            }
                            if (tiles[i, j - 1] != null)
                            {
                                pos_5_cell = tiles[i, j - 1].GetComponent<HexCell>();
                            }
                        }
                    }

                    // Position 0
                    if (curr_cell.nodes[0] == null)
                    {
                        HexCell[] p0list = { curr_cell, pos_0_cell };
                        //ChannelNode pos_0_node = new ChannelNode(p0list, 0);

                        GameObject pos_0_node = Instantiate(channelNodePrototype((p0list, -1)));
                        curr_cell.nodes[0] = pos_0_node;
                    }

                    // Position 1
                    if (curr_cell.nodes[1] == null)
                    {
                        HexCell[] p1list = { curr_cell, pos_1_cell };
                        ChannelNode pos_1_node = new ChannelNode(p1list, -1);
                        curr_cell.nodes[1] = pos_1_node;
                    }
                    // Position 2
                    if (curr_cell.nodes[2] == null)
                    {
                        HexCell[] p2list = { curr_cell, pos_2_cell };
                        ChannelNode pos_2_node = new ChannelNode(p2list, 1);
                        curr_cell.nodes[2] = pos_2_node;
                    }

                    // Position 3
                    if (curr_cell.nodes[3] == null)
                    {
                        HexCell[] p3list = { curr_cell, pos_3_cell };
                        ChannelNode pos_3_node = new ChannelNode(p3list, -1);
                        curr_cell.nodes[3] = pos_3_node;
                    }
                    // Position 4
                    if (curr_cell.nodes[4] == null)
                    {
                        HexCell[] p4list = { curr_cell, pos_4_cell };
                        ChannelNode pos_4_node = new ChannelNode(p4list, 1);
                        curr_cell.nodes[4] = pos_4_node;
                    }
                    // Position 5
                    if (curr_cell.nodes[5] == null)
                    {
                        HexCell[] p5list = { curr_cell, pos_5_cell };
                        ChannelNode pos_5_node = new ChannelNode(p5list, 0);
                        curr_cell.nodes[5] = pos_5_node;
                    }
                }
            }
        }

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                for (int pos = 0; pos < 6; pos++)
                {
                    //HexCell curr_cell = tiles[i, j].GetComponent<HexCell>();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
