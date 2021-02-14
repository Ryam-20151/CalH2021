using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : MonoBehaviour
{

    public bool isPlains = false;
    public bool isFlooded = false;
    public bool hasShrek = false;
    public bool hasGoose = false;
    public NestingGoose nestingGoose = null;
    public bool adjacentNodes = false;

    public int killedGeese;
    public int spawnedShrek;

    public SpriteRenderer tileRenderer;
    public SpriteRenderer spriteRenderer;
    public Sprite grass_sprite;
    public Sprite shrek_sprite;
    public Sprite forest_sprite;
    public Sprite water_sprite;

    public Sprite goose_sprite;
    public Sprite beaver_sprite;

    public GameObject[] nodes = { null, null, null, null, null, null };

    // Start is called before the first frame update
    void Start()
    {

        this.killedGeese = 0;
        this.spawnedShrek = 0;
        spriteRenderer.enabled = false;
        tileRenderer.enabled = true;

    }

    public void flood()
    {
        this.isFlooded = true;
        tileRenderer.sprite = water_sprite;
        spriteRenderer.enabled = false;

        if (this.hasShrek) {
            this.hasShrek = false;
            this.spawnedShrek = 1;
        }

        if (this.hasGoose)
        {
            this.killedGeese++;
            this.hasGoose = false;    
            this.nestingGoose = null;
        }
    }

    public void unflood() {
        this.isFlooded = false;

        if (this.isPlains)
        {
            tileRenderer.sprite = grass_sprite;
        }
        else {
            if (this.hasShrek)
            {
                tileRenderer.sprite = shrek_sprite;
            }
            else
            {
                tileRenderer.sprite = forest_sprite;
            }
        }
        
    }

    public void setPlains() {
        this.isPlains = true;
        tileRenderer.sprite = grass_sprite;
    }

    public void setShrek() {
        this.hasShrek = true;
        tileRenderer.sprite = shrek_sprite;

        spriteRenderer.sprite = beaver_sprite;
        spriteRenderer.transform.localScale = new Vector3(2.5f,2.5f,1f);
        spriteRenderer.enabled = true;
    }

    public void setGoose() {
        this.hasGoose = true;
        this.nestingGoose = new NestingGoose();

        spriteRenderer.sprite = goose_sprite;
        spriteRenderer.enabled = true;
    }

    public bool resolveGoose(int roll)
    {
        this.nestingGoose.resolve(roll);

        if (this.nestingGoose.spawned) {
            this.hasGoose = false;
            spriteRenderer.enabled = false;
            this.nestingGoose = null;
            Debug.Log("A goose has escaped!");
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
