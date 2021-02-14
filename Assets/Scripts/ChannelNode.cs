using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChannelNode : MonoBehaviour
{
    ChannelNode[] _adjacent;
    // TileNode[] tiles;
    public bool _hasWater;
    public bool _isCanal;
    public bool _hasDam;

    public ChannelNode(ChannelNode[] adjacent, bool hasWater, bool isCanal, bool hasDam) {
        _adjacent = adjacent;
        _hasWater = hasWater;
        _isCanal = isCanal;
        _hasWater = hasWater;
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
            //TODO: Update sprite here to have water
            curr._hasWater = true;
        }
        //no dam before, but this one is a dam
        else if (!damIt && curr._hasDam)
        {
            curr._hasWater = true;
            //TODO: Update sprite here to be a dam with water
        }
        //Dam before, and this one is a dam
        else if (damIt && curr._hasDam)
        {
            //TODO: Update sprite here to be a dam with no water
        }
        else
        {
            //TODO: Update sprite here to be canal with no water
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
