using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelNode : MonoBehaviour
{
    ChannelNode[] _adjacent = new ChannelNode[4];
    HexCell[] _tiles = new HexCell[2];
    bool _hasWater;
    bool _isCanal;
    bool _hasDam;
    //-1 left, 0 flat, 1 right
    int _orientation;
    //0 not canal, 1 canal, 2 dam
    int _state = 0;

    public SpriteRenderer canalSprite;

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

    public Sprite rightCanalWater;
    public Sprite rightCanalNoWater;
    public Sprite rightCanalDamWater;
    public Sprite rightCanalDamNoWater;
    public Sprite rightNoCanal;

    public ChannelNode(HexCell[] tiles, int orientation, bool hasWater = false, bool isCanal = true, bool hasDam = false) {
        _tiles = tiles;
        _hasWater = hasWater;
        _isCanal = isCanal;
        _hasWater = hasWater;
        _orientation = orientation;
    }

    //Add edge to node
    public bool addEdge(ChannelNode first, ChannelNode second){
        if (this._adjacent.Length >= 4 || first._adjacent.Length >= 4 || second._adjacent.Length >= 4) {
            return false;
        }

        if (this._adjacent[0] == null) {
            this._adjacent[0] = first.getRef();
            this._adjacent[1] = second.getRef();
        } else {
            this._adjacent[2] = first.getRef();
            this._adjacent[3] = second.getRef();
        }

        return true;
    }

    // Recursively iterate through all nodes to update the water
    public void updateWater(ChannelNode curr, bool damIt) {
        //node has already been looked at
        if (_hasWater)
            return; 

        //regular canal, no dam
        if (!damIt)
        {
            switch(this._orientation) {
                case -1:
                    canalSprite.sprite = leftCanalWater;
                    break;
                case 0:
                    canalSprite.sprite = flatCanalWater;
                    break;
                case 1:
                    canalSprite.sprite = rightCanalWater;
                    break;
            }

            curr._hasWater = true;
        }
        //no dam before, but this one is a dam
        else if (!damIt && curr._hasDam)
        {
            //TODO: Update to pick correct orientation of dam w/ water
            switch(this._orientation) {
                case -1:
                    canalSprite.sprite = leftCanalDamWater;
                    break;
                case 0:
                    canalSprite.sprite = flatCanalDamWater;
                    break;
                case 1:
                    canalSprite.sprite = rightCanalDamWater;
                    break;
            }

            curr._hasWater = true;
        }
        //Dam before, and this one is a dam
        else if (damIt && curr._hasDam)
        {
            switch(this._orientation) {
                case -1:
                    canalSprite.sprite = leftCanalDamNoWater;
                    break;
                case 0:
                    canalSprite.sprite = flatCanalDamNoWater;
                    break;
                case 1:
                    canalSprite.sprite = rightCanalDamNoWater;
                    break;
            }
        }
        //Empty canal, no water
        else
        {
            switch(this._orientation) {
                case -1:
                    canalSprite.sprite = leftCanalNoWater;
                    break;
                case 0:
                    canalSprite.sprite = flatCanalNoWater;
                    break;
                case 1:
                    canalSprite.sprite = rightCanalNoWater;
                    break;
            }
        }

        //Recursively iterate through canals
        //If current is a dam, don't look at adjacent nodes
        for (int i = 0; i < 4; i++) {
            if(curr._adjacent[i]._isCanal)
                updateWater(curr._adjacent[i], curr._hasDam || damIt);
        }
    }

    void onMouseEnter(){

    }

    void onMouseLeave(){

    }

    void onMouseDown(){
        //Something something make dam
        //
    }

    //Returns new value of hasWater
    public bool toggleHasWater(){
        this._hasWater = !this._hasWater;
        return this._hasWater;
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

    public ChannelNode getRef() {
        return this;
    }
}
