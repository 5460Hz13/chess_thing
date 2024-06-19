using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]
    public List<Vector2Int> allowedMoves = new List<Vector2Int>();
    [SerializeField]
    private Vector2Int _currentLoc;
    public BoardManager board;
    private bool isPawn;
    private float _playingHeight;
    private BoxCollider2D _collider;
    private Camera mainCamera;
    private Vector2 _pos;
    private Vector2 _prevPos;
    private Vector2Int _testLoc;
    private bool _isHeld;
    private bool _canMove;
    [SerializeField]
    private bool colour;
    private BoardTile hitTile;
    private  GameObject hitTile_gO;
    private BoardTile _currentTile;
    private BoardTile _lastTile;
    private Vector2Int _startingLoc;
    
    public void SetCurrentTile(BoardTile _tile){
        _currentTile = _tile;
    }
    
    public bool GetColour(){
        return colour;
    }

    public void SetColour(bool _colour){
        this.colour = _colour;
        if(!_colour)
            this.GetComponentsInChildren<MeshRenderer>()[0].material = board.blackColour;
        else   
            this.GetComponentsInChildren<MeshRenderer>()[0].material = board.whiteColour;
    }

    
    private void Awake()
    {
        mainCamera = Camera.main;
        _collider = GetComponent<BoxCollider2D>();
        board = BoardManager.Instance;
        _playingHeight = board.GetPlayingHeight();
    }


    // Start is called before the first frame update
    void Start()
    {
        _isHeld = false;
        _canMove = true;

        if(this.name.Contains("pawn")){
            isPawn = true;
        }

        //set starting location
        _testLoc = _currentTile.GetLocation();
        
        //Debug.Log(this.name+" hittile is "+hitTile);
        _startingLoc = _testLoc;
        _currentLoc = _testLoc;
        //Debug.Log("found "+this.name+" at "+_currentLoc);
        //_currentTile = board.GetTileFromLocation(_startingLoc);
        _lastTile = _currentTile;

        //center yourself
        this.transform.position = new Vector3(_currentTile.transform.position.x, _currentTile.transform.position.y, _playingHeight);
        
        
        populateAllowedMoves();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isHeld&&_canMove)
        {
            _pos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(_pos.x, _pos.y, _playingHeight);
        }
    }

    private void OnMouseDown()
    {
        //if colour matches player colour
        if(isPlayerColour()){
            _prevPos = new Vector2(this.transform.position.x, this.transform.position.y);
            _lastTile = _currentTile;
            //pickup
            _isHeld = true;
        }
        
    }

    private void OnMouseUp(){
        //find current location
        if(_isHeld){
            _testLoc = FindCurrentTile();
            }
        //if move is valid move
        if(isValidMove(_testLoc)){
                //set location
                this.transform.position = new Vector3(hitTile_gO.transform.position.x, hitTile_gO.transform.position.y, _playingHeight);
                _currentLoc = _testLoc;
                _currentTile = hitTile;
                _currentTile.SetOccupation(true);
                _currentTile.SetColour(colour);
                _lastTile.SetOccupation(false);
                _lastTile = _currentTile;
                Debug.Log("valid move to "+_currentLoc);
                
        }
        else{
            //return to prev location
            this.transform.position = new Vector3(_prevPos.x, _prevPos.y, _playingHeight);
            Debug.Log("not a valid move @ "+_testLoc);
        }
        //let go
        _isHeld = false;
        
    }

    private Vector2Int FindCurrentTile(){
        //Debug.Log("Starting Localization");
        RaycastHit hit;
        
        if(Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity)){
            hitTile_gO = hit.transform.gameObject;
            //Debug.Log("Got a Tile hit: "+hitTile_gO.name);
            hitTile = hitTile_gO.GetComponent<BoardTile>();
            Debug.Log(this.name+" reporting at: "+hitTile.GetLocation());
            return hitTile.GetLocation();
        }
        return new Vector2Int(0,0);
    }

    public virtual bool isValidMove(Vector2Int _newLoc){
        //get tested location and prev location
        //compare to analyze for calcultaed move (ex. x +1, y+2)
        Debug.Log("move diff is: "+(_newLoc - _currentLoc));
        Vector2Int moveDiff = _newLoc - _currentLoc;


        //check location unoccupied && CHECK COLOUR if occupied
        if(hitTile.GetOccupation()){
            if(colour==hitTile.GetColour())
                return false;
            else
                //prepare to take the piece
                Debug.Log("the piece is ready for the taking, checking move");
        }
        //compare calculated move to allowed move seta
        for(int i = 0; i<allowedMoves.Count; i++){
            //Debug.Log("diffing "+moveDiff.x+", "+moveDiff.y +" and " +allowedMoves.x +", "+allowedMoves.y);
            if(isPawn){
                if(!_currentLoc.Equals(_startingLoc)&&allowedMoves.Count>1){
                    //disable double move for any move but the first
                    Debug.Log("second pawn move detected, removing move instruction from");
                    allowedMoves.RemoveAt(1);
                }
                if(moveDiff.x.Equals(Mathf.Abs(allowedMoves[i].x))&&moveDiff.y.Equals(Mathf.Abs(allowedMoves[i].y))){
                    Debug.Log("listed pawn move");
                    return true;
                }
            }
            else if(moveDiff.x.Equals(Mathf.Abs(allowedMoves[i].x))&&moveDiff.y.Equals(Mathf.Abs(allowedMoves[i].y))||      //same
            (-moveDiff.x).Equals(Mathf.Abs(allowedMoves[i].x))&&(-moveDiff.y).Equals(Mathf.Abs(allowedMoves[i].y))||        //fully inverse
            (-moveDiff.x).Equals(Mathf.Abs(allowedMoves[i].x))&&moveDiff.y.Equals(Mathf.Abs(allowedMoves[i].y))||           //half inverse
            moveDiff.x.Equals(Mathf.Abs(allowedMoves[i].x))&&(-moveDiff.y).Equals(Mathf.Abs(allowedMoves[i].y))){           //the other half inverse
                Debug.Log("move is on the allowed Move list");
                return true;
            }
        }
        //return false otherwise
        return false;

    }


    public virtual void populateAllowedMoves(){
        //testing move allowances:
        allowedMoves.Add(new Vector2Int (0, 1));
        //Debug.Log(this.name+" has "+allowedMoves.Count+" allowed moves");
    }


    public virtual bool isPlayerColour(){
        return true;
    }
       
    
}
