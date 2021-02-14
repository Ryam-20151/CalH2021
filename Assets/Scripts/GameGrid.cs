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

    public static GameObject godNode;

    private System.Random _randomManager;

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

        this._randomManager = new System.Random();

        for (int seeder = 0; seeder < 10; seeder++)
        {
            int gen_y = _randomManager.Next(0, 7);
            int gen_x = _randomManager.Next(0, 7);

            if (tiles[gen_y, gen_x] != null)
            {
                tiles[gen_y, gen_x].GetComponent<HexCell>().setPlains();
            }
            else {
                seeder--;
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
                        

                        float k = 1.4f;
                        float d_x = (float)(j - 3);
                        float d_y = (float)(i - 3);

                        Vector3 initial_postion;
                        initial_postion = new Vector3(k * d_x * (float)Math.Cos(Math.PI / 6), k * (d_y + 0.5f + Math.Abs(d_x) * (float)Math.Sin(Math.PI / 6)), 0f);

                        GameObject pos_0_node = Instantiate(channelNodePrototype, initial_postion, Quaternion.identity);
                        pos_0_node.GetComponent<ChannelNode>().configureNode(p0list, 0);
                        curr_cell.nodes[0] = pos_0_node;
                        if (pos_0_cell != null) {
                            tiles[i + 1, j].GetComponent<HexCell>().nodes[3] = pos_0_node;
                        }
                    }

                    // Position 1
                    if (curr_cell.nodes[1] == null)
                    {
                        HexCell[] p1list = { curr_cell, pos_1_cell };

                        float k = 1.4f;
                        float d_x = (float)(j - 3);
                        float d_y = (float)(i - 3);

                        Vector3 initial_postion;
                        initial_postion = new Vector3(k * (d_x + 0.5f)* (float)Math.Cos(Math.PI / 6), k * (d_y + 0.25f + Math.Abs((d_x)) * (float)Math.Sin(Math.PI / 6)), 0f);

                        GameObject pos_1_node = Instantiate(channelNodePrototype, initial_postion, Quaternion.identity);
                        pos_1_node.GetComponent<ChannelNode>().configureNode(p1list, -1);
                        curr_cell.nodes[1] = pos_1_node;
                        if (pos_1_cell != null)
                        {
                            pos_1_cell.GetComponent<HexCell>().nodes[4] = pos_1_node;
                        }

                        if (i == 6 && j == 3)
                        {
                            godNode = pos_1_node;
                        }
                    }
                    // Position 2
                    if (curr_cell.nodes[2] == null)
                    {
                        HexCell[] p2list = { curr_cell, pos_2_cell };
                        float k = 1.4f;
                        float d_x = (float)(j - 3);
                        float d_y = (float)(i - 3);

                        Vector3 initial_postion;
                        initial_postion = new Vector3(k * (d_x + 0.5f) * (float)Math.Cos(Math.PI / 6), k * (d_y - 0.25f + Math.Abs((d_x)) * (float)Math.Sin(Math.PI / 6)), 0f);

                        GameObject pos_2_node = Instantiate(channelNodePrototype, initial_postion, Quaternion.identity);
                        pos_2_node.GetComponent<ChannelNode>().configureNode(p2list, 1);
                        curr_cell.nodes[2] = pos_2_node;
                        if (pos_2_cell != null)
                        {
                            pos_2_cell.GetComponent<HexCell>().nodes[5] = pos_2_node;
                        }
                    }

                    // Position 3
                    if (curr_cell.nodes[3] == null)
                    {
                        HexCell[] p3list = { curr_cell, pos_3_cell };

                        float k = 1.4f;
                        float d_x = (float)(j - 3);
                        float d_y = (float)(i - 3);

                        Vector3 initial_postion;
                        initial_postion = new Vector3(k * d_x * (float)Math.Cos(Math.PI / 6), k * (d_y - 0.5f + Math.Abs(d_x) * (float)Math.Sin(Math.PI / 6)), 0f);

                        GameObject pos_3_node = Instantiate(channelNodePrototype, initial_postion, Quaternion.identity);
                        pos_3_node.GetComponent<ChannelNode>().configureNode(p3list, 0);
                        curr_cell.nodes[3] = pos_3_node;
                        if (pos_3_cell != null)
                        {
                            tiles[i + 1, j].GetComponent<HexCell>().nodes[0] = pos_3_node;
                        }
                    }
                    // Position 4
                    if (curr_cell.nodes[4] == null)
                    {
                        HexCell[] p4list = { curr_cell, pos_4_cell };
                        float k = 1.4f;
                        float d_x = (float)(j - 3);
                        float d_y = (float)(i - 3);

                        Vector3 initial_postion;
                        initial_postion = new Vector3(k * (d_x - 0.5f) * (float)Math.Cos(Math.PI / 6), k * (d_y - 0.25f + Math.Abs((d_x)) * (float)Math.Sin(Math.PI / 6)), 0f);

                        GameObject pos_4_node = Instantiate(channelNodePrototype, initial_postion, Quaternion.identity);
                        pos_4_node.GetComponent<ChannelNode>().configureNode(p4list, -1); 

                        curr_cell.nodes[4] = pos_4_node;

                        if (pos_4_cell != null)
                        {
                            pos_4_cell.GetComponent<HexCell>().nodes[1] = pos_4_node;
                        }
                    }
                    // Position 5
                    if (curr_cell.nodes[5] == null)
                    {
                        HexCell[] p5list = { curr_cell, pos_5_cell };
                        HexCell[] p1list = { curr_cell, pos_1_cell };

                        float k = 1.4f;
                        float d_x = (float)(j - 3);
                        float d_y = (float)(i - 3);

                        Vector3 initial_postion;
                        initial_postion = new Vector3(k * (d_x - 0.5f) * (float)Math.Cos(Math.PI / 6), k * (d_y + 0.25f + Math.Abs((d_x)) * (float)Math.Sin(Math.PI / 6)), 0f);

                        GameObject pos_5_node = Instantiate(channelNodePrototype, initial_postion, Quaternion.identity);
                        pos_5_node.GetComponent<ChannelNode>().configureNode(p5list, 1);
                        
                        curr_cell.nodes[5] = pos_5_node;

                        if (pos_5_cell != null)
                        {
                            pos_5_cell.GetComponent<HexCell>().nodes[2] = pos_5_node;
                        }


                    }

                    
                }
            }
        }

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (tiles[i, j] != null)
                {
                    for (int pos = 0; pos < 6; pos++)
                    {
                        HexCell curr_cell = tiles[i, j].GetComponent<HexCell>();
                        ChannelNode target_node = curr_cell.nodes[pos].GetComponent<ChannelNode>();

                        int mod1 = (pos + 1);
                        int mod2 = (pos - 1);

                        if (mod1 == 6) {
                            mod1 = 0;
                        }

                        if (mod2 == -1) {
                            mod2 = 5;
                        }

                        Debug.Log(mod1);
                        Debug.Log(mod2);

                        target_node.addEdge(curr_cell.nodes[mod1].GetComponent<ChannelNode>(),
                            curr_cell.nodes[mod2].GetComponent<ChannelNode>());

                    }
                }
            }
        }

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {

            }
        }

        this.makeRiver();
    }

    private void makeRiver() {
        for (int i = 0; i < 7; i++)
        {
            tiles[i, 3].GetComponent<HexCell>().nodes[1].GetComponent<ChannelNode>().toggleHasCanal();
            tiles[i, 3].GetComponent<HexCell>().nodes[2].GetComponent<ChannelNode>().toggleHasCanal();
        }
    }

    public void defloodAll() {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (tiles[gen_y, gen_x] != null)
                {
                    tiles[j, i].GetComponent<HexCell>().unflood();
                }
            }
        }
    }


    public void spawnShrek() {
        for (int seeder = 0; seeder < 1; seeder++)
        {
            int gen_y = _randomManager.Next(0, 7);
            int gen_x = _randomManager.Next(0, 7);
            bool continue_flag = false;

            if (tiles[gen_y, gen_x] != null) { 
                if (
                !(tiles[gen_y, gen_x].GetComponent<HexCell>().isPlains) &&
                !(tiles[gen_y, gen_x].GetComponent<HexCell>().isFlooded) &&
                !(tiles[gen_y, gen_x].GetComponent<HexCell>().hasShrek)) 
                {
                    tiles[gen_y, gen_x].GetComponent<HexCell>().setShrek();
                    continue_flag = true;
                }
            }

            if (!continue_flag) {
                seeder--;
            }
        }
    }

    public void spawnGeese()
    {

        for (int seeder = 0; seeder < 1; seeder++)
        {
            int gen_y = _randomManager.Next(0, 7);
            int gen_x = _randomManager.Next(0, 7);
            bool continue_flag = false;

            if (tiles[gen_y, gen_x] != null)
            {
                if (
                (tiles[gen_y, gen_x].GetComponent<HexCell>().isPlains) &&
                !(tiles[gen_y, gen_x].GetComponent<HexCell>().isFlooded) &&
                !(tiles[gen_y, gen_x].GetComponent<HexCell>().hasGoose))
                {
                    tiles[gen_y, gen_x].GetComponent<HexCell>().setGoose();
                    continue_flag = true;
                }
            }

            if (!continue_flag)
            {
                seeder--;
            }
        }

        
    }

    public int pollGeeseSpawns() {
        int points = 0;

        int roll = this._randomManager.Next(0, 100);
        Debug.Log("Roll:");
        Debug.Log(roll);

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (tiles[j, i] != null)
                {
                    if (tiles[j, i].GetComponent<HexCell>().hasGoose)
                    {
                        tiles[j, i].GetComponent<HexCell>().resolveGoose(roll);
                    }

                    if (tiles[j, i].GetComponent<HexCell>().killedGeese != 0)
                    {
                        points += tiles[j, i].GetComponent<HexCell>().killedGeese;
                        tiles[j, i].GetComponent<HexCell>().killedGeese = 0;
                    }


                }
            }
        }

        return points;
    }

    public int pollShreks() {
        int shreks = 0;

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (tiles[j, i] != null)
                {

                    if (tiles[j, i].GetComponent<HexCell>().spawnedShrek != 0)
                    {
                        shreks += tiles[j, i].GetComponent<HexCell>().spawnedShrek;
                        tiles[j, i].GetComponent<HexCell>().spawnedShrek = 0;
                    }
                }
            }
        }

        return shreks;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
