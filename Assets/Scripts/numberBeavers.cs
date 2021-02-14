using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class numberBeavers : MonoBehaviour
{

    public int turns;
    public int totalTurns;

    public Image[] beavers;
    public Sprite fullBeaver;
    public Sprite usedBeaver;

    void Update(){
        for(int i = 0; i<beavers.Length; i++){
            if(i<turns){
                beavers[i].sprite = fullBeaver;
            } else {
                beavers[i].sprite = usedBeaver;
            }

            if(i < totalTurns){
                beavers[i].enabled = true;
            } else {
                beavers[i].enabled = false;
            }
        }
    }
}
