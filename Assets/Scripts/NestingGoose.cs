using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestingGoose
{
    private int _turnsAlive;
    private int _spawnThreshold;
    private int _spawnCoeff = 10;
    public bool spawned; 
    
    // Start is called before the first frame update
    void Start()
    {
        this._spawnThreshold = -1;
        this.spawned = false;
    }

    public void resolve(int roll) {
        this._turnsAlive++;
        if (this._turnsAlive > 3) {
            this._spawnThreshold = (this._turnsAlive) * this._spawnCoeff;
        }

        if (this._spawnThreshold > roll) {
            this.spawned = true;
            Debug.Log("Goose Spawn!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
