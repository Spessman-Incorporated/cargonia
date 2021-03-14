using System;
using System.Collections;
using System.Collections.Generic;
using Cargonia.Tile;
using Mirror;
using UnityEngine;

public class TileMap : NetworkBehaviour
{
    public static TileMap singleton { get; private set; }
    
    public GameObject tilePrefab;

    public List<TileStruct> tiles = new List<TileStruct>();

    private void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
        }
    }

    public Tile GetTile(Vector2 position)
    {
        int x = (int) position.x;
        int y = (int) position.y;

        //Debug.Log(x + " " + y);
        Tile tile = null;

        foreach (TileStruct listTile in tiles)
        {
            if (listTile.position == position)
                tile = listTile.tile;
        }

        if (tile == null)
            tile = CreateNewTile(position);
        
        return tile;
    }

    private Tile CreateNewTile(Vector2 position)
    {
        GameObject tileInstance = Instantiate(tilePrefab, position, Quaternion.identity, transform);
        Tile tile = tileInstance.GetComponent<Tile>();
        
        int x = (int) position.x;
        int y = (int) position.y;

        tileInstance.name = x + " | " + y;
        
        TileStruct newTile = new TileStruct(tile, position);
        
        tiles.Add(newTile);
        
        return tile;
    }
    
    public struct TileStruct
    {
        public Tile tile;
        public Vector2 position;

        public TileStruct(Tile tile, Vector2 position)
        {
            this.tile = tile;
            this.position = position;
        }
    }
}

