﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nodeCollider : MonoBehaviour
{
    Sprite _prevSprite;

    public ChannelNode channelNodeScript;

    public Sprite leftWhiteCanal;
    public Sprite flatWhiteCanal;
    public Sprite rightWhiteCanal;
    public Sprite leftWhiteDam;
    public Sprite flatWhiteDam;
    public Sprite rightWhiteDam;

    void OnMouseEnter(){
        Debug.Log("MOUSE DETECTED");
        _prevSprite = channelNodeScript.canalSprite.sprite;
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
        Debug.Log("BYE MOUSE ");
        channelNodeScript.canalSprite.sprite = _prevSprite;
    }

    void OnMouseDown(){
        if (channelNodeScript.updateState()){
            Debug.Log(channelNodeScript.getState() + " and click!");
            Debug.Log("Adjacent Nodes: " + "pee");
            channelNodeScript.updateWater(GameGrid.godNode.GetComponent<ChannelNode>());
        }
    }
}
