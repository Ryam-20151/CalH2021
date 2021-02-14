using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageGame : MonoBehaviour
{
    public Text displayTurn;
    public Text displayLogs;

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

                if (turn >= 8 && (turn % 4) == 0)
                {
                    //Spawn shreks
                    ggrid.spawnShrek();
                    //Spawn geese
                    ggrid.spawnGeese();
                }

                points = ggrid.pollGeeseSpawns();
            }
        }

    public void usebeaver()
    {
        turns--;
    }

    public void getLogs()
    {
        turns--;
        logs++;
        displayLogs.text = ""+logs;
    }
    
   
    void Start()
    {
            turn++;
            turns = totalTurns;
            displayTurn.text = "Turn: "+turn;
            displayLogs.text = ""+logs;
    }

}
