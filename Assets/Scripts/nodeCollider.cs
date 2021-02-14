using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeCollider : MonoBehaviour
{
    public ChannelNode channelNodeScript;
    private GameGrid gameGridScript;
    private ManageGame manageGameScript;

    public Sprite leftWhiteCanal;
    public Sprite flatWhiteCanal;
    public Sprite rightWhiteCanal;
    public Sprite leftWhiteDam;
    public Sprite flatWhiteDam;
    public Sprite rightWhiteDam;

    void Start(){
        gameGridScript = GameObject.Find("GameGrid").GetComponent<GameGrid>();
        manageGameScript = GameObject.Find("ManageGame").GetComponent<ManageGame>();
    }

    void OnMouseEnter(){
        int waterSourcesFound = 0;
        int damsFound = 0;

        for (int i = 0; i < 4; i++) {
            if(channelNodeScript._adjacent[i] && channelNodeScript._adjacent[i]._hasWater){
                waterSourcesFound++;
            }
            if (channelNodeScript._adjacent[i] && channelNodeScript._adjacent[i]._hasDam)
                damsFound++;
        }

        channelNodeScript._prevSprite = channelNodeScript.canalSprite.sprite;
        
        if (manageGameScript.turns == 0 || 
            (channelNodeScript.getState() == 1 && manageGameScript.logs < 2) ||
            (waterSourcesFound < 1 || waterSourcesFound <= damsFound))
            return;

        if (channelNodeScript.getState() == 0) {
            switch (channelNodeScript.getOrientation()) {
                case -1:
                    channelNodeScript.canalSprite.sprite = leftWhiteCanal;
                    break;
                case 0:
                    channelNodeScript.canalSprite.sprite = flatWhiteCanal;
                    break;
                case 1:
                    channelNodeScript.canalSprite.sprite = rightWhiteCanal;
                    break;
            }
        } else if (channelNodeScript.getState() == 1) {
            switch (channelNodeScript.getOrientation()) {
                case -1:
                    channelNodeScript.canalSprite.sprite = leftWhiteDam;
                    break;
                case 0:
                    channelNodeScript.canalSprite.sprite = flatWhiteDam;
                    break;
                case 1:
                    channelNodeScript.canalSprite.sprite = rightWhiteDam;
                    break;
            }
        }
    }

    void OnMouseExit(){
        channelNodeScript.canalSprite.sprite = channelNodeScript._prevSprite;
    }

    void OnMouseDown(){
        channelNodeScript.getState();
        //do nothing if no turns
        if (manageGameScript.turns == 0)
            return;
        if (channelNodeScript.getState() == 1 && manageGameScript.logs < 2) {
            return;
        }

        if (channelNodeScript.updateState()){
            gameGridScript.defloodAll();
            channelNodeScript.toggleAllWaterOff(GameGrid.godNode.GetComponent<ChannelNode>());
            channelNodeScript.updateWater(GameGrid.godNode.GetComponent<ChannelNode>());
            manageGameScript.turns--;
            if (channelNodeScript.getState() == 2)
                manageGameScript.logs -= 2;
                manageGameScript.displayLogs.text = ""+manageGameScript.logs;
        }
    }
}
