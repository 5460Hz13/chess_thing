using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardTile : MonoBehaviour
{
    private bool colour;

    private Vector2Int _location;

    private bool isOccupied = false;

    public Vector2Int GetLocation(){
        return _location;
    }

    public void SetOccupation(bool _val){
        isOccupied = _val;
    }

    public bool GetOccupation(){
        return isOccupied;
    }

    public void SetColour(bool _colour){
        colour = _colour;
    }

    public bool GetColour(){
        return colour;
    }


    // Start is called before the first frame update
    void Awake()
    {
        _location = new Vector2Int(int.Parse(this.gameObject.name), int.Parse(this.transform.parent.gameObject.name));
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
