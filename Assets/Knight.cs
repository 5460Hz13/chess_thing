using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{

    
    public override void populateAllowedMoves(){
        
        allowedMoves.Add(new Vector2Int (1, 2));
        allowedMoves.Add(new Vector2Int (2, 1));
        
        Debug.Log(this.name+" has "+allowedMoves.Count+" allowed moves");
    }

}