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
    public void updateWater(ChannelNode curr) {
        //node has already been looked at
        if (_hasWater)
            return; 

        curr._hasWater = true;
        
        for (int i = 0; i < 4; i++) {
        }
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
