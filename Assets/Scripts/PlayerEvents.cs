using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public delegate void HitObstacle();
    public static event HitObstacle OnHit;

    public static void PlayerHit()
    {
        if(OnHit != null)
            OnHit();
    }
}
