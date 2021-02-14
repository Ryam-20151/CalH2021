using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestingGoose : MonoBehaviour
{
    private int _turnsAlive;
    private int _spawnThreshold;
    public bool spawned; 
    
    // Start is called before the first frame update
    void Start()
    {
            
    }

    public void resolve(int roll) {
        this._turnsAlive++;
        if (this._turnsAlive > 3) {
            this._spawnThreshold = (this._turnsAlive - 1) * 2;
        }

        if (this._spawnThreshold > roll) {
            this.spawned = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
