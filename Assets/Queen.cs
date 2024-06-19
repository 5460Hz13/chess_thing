using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{

    
    public override void populateAllowedMoves(){
        
        allowedMoves.Add(new Vector2Int (0, 1));
        allowedMoves.Add(new Vector2Int (1, 0));
        allowedMoves.Add(new Vector2Int (1, 1));

        allowedMoves.Add(new Vector2Int (0, 2));
        allowedMoves.Add(new Vector2Int (2, 0));
        allowedMoves.Add(new Vector2Int (2, 2));

        allowedMoves.Add(new Vector2Int (0, 3));
        allowedMoves.Add(new Vector2Int (3, 0));
        allowedMoves.Add(new Vector2Int (3, 3));

        allowedMoves.Add(new Vector2Int (0, 4));
        allowedMoves.Add(new Vector2Int (4, 0));
        allowedMoves.Add(new Vector2Int (4, 4));

        allowedMoves.Add(new Vector2Int (0, 5));
        allowedMoves.Add(new Vector2Int (5, 0));
        allowedMoves.Add(new Vector2Int (5, 5));

        allowedMoves.Add(new Vector2Int (0, 6));
        allowedMoves.Add(new Vector2Int (6, 0));
        allowedMoves.Add(new Vector2Int (6, 6));

        allowedMoves.Add(new Vector2Int (0, 7));
        allowedMoves.Add(new Vector2Int (7, 0));
        allowedMoves.Add(new Vector2Int (7, 7));

        allowedMoves.Add(new Vector2Int (0, 8));        
        allowedMoves.Add(new Vector2Int (8, 0));
        
        Debug.Log(this.name+" has "+allowedMoves.Count+" allowed moves");
    }

}
