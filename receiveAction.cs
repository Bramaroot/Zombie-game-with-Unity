using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receiveAction : MonoBehaviour
{

    //Maximum de points de vie
    public int maxHitPoint = 5;

    //point de vie actu
    public int  hitPoint = 0;

   private void Start()
    {
        //Au debut du jeu

        hitPoint = maxHitPoint;
    
    }

    public void GetDamage(int degat)
        {
            hitPoint -= degat;

            //si les points de vie sont finis destroy gameObjects

            if (hitPoint < 1)
            {
                Destroy(gameObject);
            }
               

        }

  
}
