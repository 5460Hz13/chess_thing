using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Random;

public class Board : MonoBehaviour
{
    [SerializeField]
    private int boardSize;

    [SerializeField]

    private GameObject boardTile;

    [SerializeField]
    private Material blackColour;
    [SerializeField]
    private Material whiteColour;

    private Transform _spawnLocation;

    private BoardTile[] tiles;

    private int yToFind;

    [SerializeField]
    private float _playingHeight;
    [SerializeField]
    private GameObject pieceParent;

    public List<GameObject> pieces = new List<GameObject>();


    // Start is called before the first frame update
    void Awake()
    {
        tiles = GetComponentsInChildren<BoardTile>();
        Debug.Log("found "+tiles.Length+" tiles on the board");
    }

    void Start(){
        //coinflip for colour
        int rand = Mathf.Random(0,1);

        //instantiate all game pieces based on tile list
    }

    public BoardTile GetTileFromLocation(Vector2Int _loc){
        //supply coordinates, get BoardTile script of relevant tile
        Debug.Log("tile list probed for: "+_loc);
        yToFind = ((_loc.y -1) * 8);
        Debug.Log("which is meant to be at: "+(yToFind+_loc.x-1));
        return tiles[(yToFind+_loc.x)-1];
    }


    public float GetPlayingHeight(){
        return _playingHeight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
