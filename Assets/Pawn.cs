using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{

    
    public override void populateAllowedMoves(){
        
        allowedMoves.Add(new Vector2Int (0, 1));
        allowedMoves.Add(new Vector2Int (0, 2));
        //Debug.Log(this.name+" has "+allowedMoves.Count+" allowed moves");
    }

}
