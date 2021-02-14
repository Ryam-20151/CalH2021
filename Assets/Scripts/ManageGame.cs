using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageGame : MonoBehaviour
{
    public Text displayTurn;

    public int turn = 0;
    public int turns = 1;
    public int totalTurns = 1;

    public void endTurn()
        {
            turn++;
            turns = totalTurns;
            displayTurn.text = "Turn: "+turn;
        }

    public void usebeaver()
    {
        turns--;
    }
    
   
    void Start()
    {
            turn++;
            turns = totalTurns;
            displayTurn.text = "Turn: "+turn;
    }

}
