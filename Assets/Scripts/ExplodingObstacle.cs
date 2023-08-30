using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingObstacle : Obstacle
{
    public override void HitPlayer()
    {    
        base.HitPlayer();
        Destroy(this.gameObject);
    }
}
