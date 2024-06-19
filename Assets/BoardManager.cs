using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

    public static BoardManager Instance { get; private set; }


    [SerializeField]
    private int boardSize;

    [SerializeField]

    private GameObject boardTile;

    
    public Material blackColour;
    public Material whiteColour;

    private Transform _spawnLocation;

    private BoardTile[] tiles;

    private int yToFind;

    [SerializeField]
    private float _playingHeight;
    [SerializeField]
    private GameObject playerPieceParent;
    [SerializeField]
    private GameObject oppPieceParent;

    public List<GameObject> pieces = new List<GameObject>();

    public List<int> spawnOrder = new List<int>();


    // Start is called before the first frame update
    void Awake()
    {
        //singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;


        tiles = GetComponentsInChildren<BoardTile>();
        Debug.Log("found "+tiles.Length+" tiles on the board");
    }

    void Start(){
        //coinflip for colour (black == 1, white == 0)
        int rand = Random.Range(0,2);
        bool randBool = false;
        if(rand == 1){
        }
            randBool = true;


        //TESTING OVERRIDE
        //randBool = false;
        
        //instantiate all game pieces based on tile list

        //player pieces
        PlacePieces(true, randBool);

        //opponent pieces
        PlacePieces(false, !randBool);
    }

    public BoardTile GetTileFromLocation(Vector2Int _loc){
        //compensate for array listing left to right, bottom to top -> get BoardTile script of relevant tile
        yToFind = ((_loc.y -1) * 8);
        //Debug.Log("tile list probed for: "+_loc);
        return tiles[(yToFind+_loc.x)-1];
    }


    public float GetPlayingHeight(){
        return _playingHeight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlacePieces(bool _player, bool _colour){
        
        
        int i = 0;
        foreach (int spawn in spawnOrder){
            if(!_player){ //if opponent
                BoardTile _testTile = tiles[(tiles.Length)-i-1]; //starting point for placing
                Debug.Log("spawning "+pieces[spawn].name+" @ "+_testTile.GetLocation());
                GameObject piece =  Instantiate(pieces[spawn], oppPieceParent.transform);
                Piece pieceScript = piece.GetComponent<Piece>();
                pieceScript.SetColour(!_colour);



                if(!_colour){
                    if(pieces[spawn].name.Contains("queen")){
                        Debug.Log("queen shift");
                        //change testTile to next tile
                        _testTile = tiles[(tiles.Length)-i-1];
                    }
                    else if(pieces[spawn].name.Contains("king")){
                        Debug.Log("king shift");
                        //change testTile to prev tile
                        _testTile = tiles[(tiles.Length)-i-1];
                    }
                    //position as normal
                    piece.transform.position = new Vector3(_testTile.transform.position.x, _testTile.transform.position.y, _playingHeight);
                    pieceScript.SetCurrentTile(_testTile);

                }
                else
                {
                    piece.transform.position = new Vector3(_testTile.transform.position.x, _testTile.transform.position.y, _playingHeight);
                    pieceScript.SetCurrentTile(_testTile);
                }
               
            }
            else if(_player){ //if player
                BoardTile _testTile = tiles[i]; //starting point for placing
                Debug.Log("spawning "+pieces[spawn].name+" @ "+_testTile.GetLocation());
                GameObject piece =  Instantiate(pieces[spawn], playerPieceParent.transform);
                Piece pieceScript = piece.GetComponent<Piece>();
                pieceScript.SetColour(!_colour);
                
                if(_colour){
                    if(pieces[spawn].name.Contains("queen")){
                        Debug.Log("queen shift");
                        //position to next tile
                        _testTile = tiles[i+1];
                    }
                    else if(pieces[spawn].name.Contains("king")){
                        Debug.Log("king shift");
                        //position to prev tile
                        _testTile = tiles[i-1];
                    }
                    piece.transform.position = new Vector3(_testTile.transform.position.x, _testTile.transform.position.y, _playingHeight);
                    pieceScript.SetCurrentTile(_testTile);
                    
                }
                else
                {
                    piece.transform.position = new Vector3(_testTile.transform.position.x, _testTile.transform.position.y, _playingHeight);
                    pieceScript.SetCurrentTile(_testTile);
                }
                
            }
            else
                Debug.Log("Spawner couldn't find player parameter passed into PlacePieces function!");
            

            
            i++;
        }
        Debug.Log("finished Instantiation of "+i+" Pieces");
    }
}
