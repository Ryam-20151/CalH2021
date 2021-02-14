﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ManageGame : MonoBehaviour
{
    public Text displayTurn;
    public Text displayLogs;
    public Text displayScore;

    public int turn = 0;
    public int turns = 1;
    public int points = 0;
    public int totalTurns = 1;

    public int logs = 0;

    public GameGrid ggrid;

    public void endTurn()
        {

            if(turn < 20){
                turn++;
                turns = totalTurns;
                displayTurn.text = "Turn: " + turn;

                if (turn >= 4 && (turn % 4) == 0)
                {
                    
                    
                }

                if (turn <= 16 && turn >= 4 && (turn % 4) == 0)
                {
                    //Spawn shreks
                    ggrid.spawnShrek();
                    //Spawn geese
                    ggrid.spawnGeese();
                }

                points = ggrid.pollGeeseSpawns();
            }else{
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            }
        }

    public void usebeaver()
    {
        turns--;
    }

    public void getLogs()
    {
        if(turns>0)
        {
          turns--;
          logs++;
          displayLogs.text = ""+logs;
        }
       
    }
    
   
    void Start()
    {
            turn++;
            turns = totalTurns;
            displayTurn.text = "Turn: "+turn;
            displayLogs.text = ""+logs;
            displayScore.text = "Score: "+points;
    }

}
