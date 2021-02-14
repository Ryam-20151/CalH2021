﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelNode : MonoBehaviour
{
    public ChannelNode[] _adjacent = new ChannelNode[4];
    HexCell[] _tiles = new HexCell[2];
    public bool _hasWater;
    public bool _isCanal;
    public bool _hasDam;
    public bool _visited = false;
    //-1 left, 0 flat, 1 right
    int _orientation;
    //0 not canal, 1 canal, 2 dam
    int _state = 0;

    public SpriteRenderer canalSprite;
    public Collider2D nodeCollider;

    public Sprite flatCanalWater;
    public Sprite flatCanalNoWater;
    public Sprite flatCanalDamWater;
    public Sprite flatCanalDamNoWater;
    public Sprite flatNoCanal;

    public Sprite leftCanalWater;
    public Sprite leftCanalNoWater;
    public Sprite leftCanalDamWater;
    public Sprite leftCanalDamNoWater;
    public Sprite leftNoCanal;

    public Sprite _prevSprite;
    public Sprite rightCanalWater;
    public Sprite rightCanalNoWater;
    public Sprite rightCanalDamWater;
    public Sprite rightCanalDamNoWater;
    public Sprite rightNoCanal;

    public void configureNode(HexCell[] tiles, int orientation, bool isCanal = false, bool hasWater = false, bool hasDam = false) {
        this._tiles = tiles;
        this._hasWater = hasWater;
        this._isCanal = isCanal;
        this._hasWater = hasWater;
        this._orientation = orientation;

        switch (_orientation){
            case -1:
                nodeCollider.transform.Rotate(0,0,-60f,Space.Self);
                if (!isCanal) {
                    canalSprite.sprite = leftNoCanal;
                } else if (isCanal && !hasWater && !hasDam) {
                    canalSprite.sprite = leftCanalNoWater;
                } else if (isCanal && hasWater && !hasDam) {
                    canalSprite.sprite = leftCanalWater;
                } else if (isCanal && !hasWater && hasDam) {
                    canalSprite.sprite = leftCanalDamNoWater;
                } else if (isCanal && hasWater && hasDam) {
                    canalSprite.sprite = leftCanalDamWater;
                }
                break;
            case 0:
                if (!isCanal) {
                    canalSprite.sprite = flatNoCanal;
                } else if (isCanal && !hasWater && !hasDam) {
                    canalSprite.sprite = flatCanalNoWater;
                } else if (isCanal && hasWater && !hasDam) {
                    canalSprite.sprite = flatCanalWater;
                } else if (isCanal && !hasWater && hasDam) {
                    canalSprite.sprite = flatCanalDamNoWater;
                } else if (isCanal && hasWater && hasDam) {
                    canalSprite.sprite = flatCanalDamWater;
                }
                break;
            case 1:
                nodeCollider.transform.Rotate(0,0,60f,Space.Self);
                if (!isCanal) {
                    canalSprite.sprite = rightNoCanal;
                } else if (isCanal && !hasWater && !hasDam) {
                    canalSprite.sprite = rightCanalNoWater;
                } else if (isCanal && hasWater && !hasDam) {
                    canalSprite.sprite = rightCanalWater;
                } else if (isCanal && !hasWater && hasDam) {
                    canalSprite.sprite = rightCanalDamNoWater;
                } else if (isCanal && hasWater && hasDam) {
                    canalSprite.sprite = rightCanalDamWater;
                }
                break;
        }
    }

    //Add edge to node
    public bool addEdge(ChannelNode first, ChannelNode second){
        if (this._adjacent[0] == null) {
            this._adjacent[0] = first;
            this._adjacent[1] = second;
        } else {
            this._adjacent[2] = first;
            this._adjacent[3] = second;
        }

        return true;
    }

    //Update state of node
    public bool updateState(){
        int waterSourcesFound = 0;
        int damsFound = 0;

        for (int i = 0; i < 4; i++) {
            if(_adjacent[i] && _adjacent[i]._hasWater){
                waterSourcesFound++;
            }
            if (_adjacent[i] && _adjacent[i]._hasDam)
                damsFound++;
        }

        Debug.Log("Dams: " + damsFound + "Water: " + waterSourcesFound);
        if (waterSourcesFound < 1 || waterSourcesFound <= damsFound )
            return false;

        switch (_state){
            case 0:
                _state++;
                _isCanal = true;

                break;
            case 1:
                _state++;
                _hasDam = true;
                break;
            default:
                return false;
        }
        return true;
    }

    // Recursively iterate through all nodes to update the water
    public void updateWater(ChannelNode curr, bool damIt = false) {
        //node has already been looked at
        if (curr._hasWater || (curr._visited && damIt))
            return; 

        curr._visited = true;
        //regular canal, no dam
        if (!damIt && !curr._hasDam)
        {
            switch(curr._orientation) {
                case -1:
                    curr.canalSprite.sprite = curr._prevSprite = curr.leftCanalWater;
                    break;
                case 0:
                    curr.canalSprite.sprite = curr._prevSprite = curr.flatCanalWater;
                    break;
                case 1:
                    curr.canalSprite.sprite = curr._prevSprite = curr.rightCanalWater;
                    break;
            }

            curr._hasWater = true;
        }
        //no dam before, but this one is a dam
        else if (!damIt && curr._hasDam)
        {
            //TODO: Update to pick correct orientation of dam w/ water
            switch(curr._orientation) {
                case -1:
                    curr.canalSprite.sprite = curr._prevSprite = curr.leftCanalDamWater;
                    break;
                case 0:
                    curr.canalSprite.sprite = curr._prevSprite = curr.flatCanalDamWater;
                    break;
                case 1:
                    curr.canalSprite.sprite = curr._prevSprite = curr.rightCanalDamWater;
                    break;
            }

            curr._hasWater = true;
            curr._tiles[0].flood();
            curr._tiles[1].flood();
        }
        //Dam before, and this one is a dam
        else if (damIt && curr._hasDam)
        {
            switch(curr._orientation) {
                case -1:
                    curr.canalSprite.sprite = curr._prevSprite = curr.leftCanalDamNoWater;
                    break;
                case 0:
                    curr.canalSprite.sprite = curr._prevSprite = curr.flatCanalDamNoWater;
                    break;
                case 1:
                    curr.canalSprite.sprite = curr._prevSprite = curr.rightCanalDamNoWater;
                    break;
            }
        }
        //Empty canal, no water
        else
        {
            switch(curr._orientation) {
                case -1:
                    curr.canalSprite.sprite = curr._prevSprite = curr.leftCanalNoWater;
                    break;
                case 0:
                    curr.canalSprite.sprite = curr._prevSprite = curr.flatCanalNoWater;
                    break;
                case 1:
                    curr.canalSprite.sprite = curr._prevSprite = curr.rightCanalNoWater;
                    break;
            }
        }

        //Recursively iterate through canals
        //If current is a dam, don't look at adjacent nodes
        for (int i = 0; i < 4; i++) {
            if(curr._adjacent[i] && curr._adjacent[i]._isCanal)
                updateWater(curr._adjacent[i], curr._hasDam || damIt);
        }
    }

    public void toggleAllWaterOff(ChannelNode curr) {
        //Don't care about nodes without water
        if (!curr._hasWater)
            return;

        curr._hasWater = false;
        curr._visited = false;

        for (int i = 0; i < 4; i++) {
            if(curr._adjacent[i] && curr._adjacent[i]._isCanal)
                toggleAllWaterOff(curr._adjacent[i]);
        }
    }

    //Returns new value of hasWater
    public bool setHasWater(bool val){
        this._hasWater = val;
        return val;
    }

    //Returns new value of hasDam
    public bool toggleHasDam(){
        this._hasDam = !this._hasDam;
        return this._hasDam;
    }

    public bool toggleHasCanal(){
        this._isCanal = !this._isCanal;
        return this._isCanal;
    }

    public int getOrientation(){
        return _orientation;
    }

    public int getState(){
        return _state;
    }
}
