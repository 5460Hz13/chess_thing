using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{

    
    public override void populateAllowedMoves(){
        
        allowedMoves.Add(new Vector2Int (1, 1));
        allowedMoves.Add(new Vector2Int (2, 2));
        allowedMoves.Add(new Vector2Int (3, 3));
        allowedMoves.Add(new Vector2Int (4, 4));
        allowedMoves.Add(new Vector2Int (5, 5));
        allowedMoves.Add(new Vector2Int (6, 6));
        allowedMoves.Add(new Vector2Int (7, 7));
        
        //Debug.Log(this.name+" has "+allowedMoves.Count+" allowed moves");
    }

}
