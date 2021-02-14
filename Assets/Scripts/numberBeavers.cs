using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numberBeavers : MonoBehaviour
{
    public ManageGame new_game;
    

    public Image[] beavers;
    public Sprite fullBeaver;
    public Sprite usedBeaver;

    void Update(){
        for(int i = 0; i<beavers.Length; i++){
            if(i<new_game.turns){
                beavers[i].sprite = fullBeaver;
            } else {
                beavers[i].sprite = usedBeaver;
            }

            if(i < new_game.totalTurns){
                beavers[i].enabled = true;
            } else {
                beavers[i].enabled = false;
            }
        }
    }
}
