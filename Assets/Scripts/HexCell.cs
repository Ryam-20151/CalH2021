﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{

    public bool isPlains = false;
    public bool isFlooded = false;
    public bool hasBeaverSpawn = false;
    public NestingGoose nestingGoose = null;
    public bool adjacentNodes = false;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer gooseRenderer;
    public Sprite grass_sprite;
    public Sprite shrek_sprite;
    public Sprite water_sprite;
    public Sprite goose_sprite; 

    // Start is called before the first frame update
    void Start()
    {

        
        //gooseRenderer.enabled = false;

    }

    public void flood()
    {
        spriteRenderer.sprite = water_sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
