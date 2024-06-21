using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private GameObject[] mapTiles = new GameObject[9];


    [SerializeField] private Sprite[] mapTextures;
    [SerializeField] private int lvlTreshold;
    private int currentTexture = 0;

    private Vector2[] posOffsets =
    {
        new Vector2(    0, 100),
        new Vector2(  100, 100),
        new Vector2(  100,   0),
        new Vector2(  100,-100),
        new Vector2(    0,-100),
        new Vector2( -100,-100),
        new Vector2( -100,   0),
        new Vector2( -100, 100)
    };

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            mapTiles[i] = transform.GetChild(i).gameObject;
            transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = mapTextures[0];
        }
    }

    public void SetMap(GameObject currentTile)
    {
        int curTile = 0;
        for(int i = 0; i < transform.childCount; i++)
        {
            if (currentTile == transform.GetChild(i).gameObject)
                continue;

            transform.GetChild(i).transform.position = (Vector2)currentTile.transform.position + posOffsets[curTile++];
        }

    }


    public void SetMapTexture(int playerLvl)
    {
        if (currentTexture == mapTextures.Length - 1) return;

        if (playerLvl % lvlTreshold != 0) return;
            currentTexture++;

        foreach (GameObject tile in mapTiles)
            tile.GetComponent<SpriteRenderer>().sprite = mapTextures[currentTexture];


        GameObject.FindObjectOfType<EnemySpawner>().currentGameStage++;
    }

}
