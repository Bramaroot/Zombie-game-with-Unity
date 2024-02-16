using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamage : MonoBehaviour
{
    public int maxHitPoint = 5;

    public int hitPoint = 0;

    public bool isInvulnerable;

    public float invulnerablyTime;

    private float timeSinceLastHit = 0.0f;



    // Start is called before the first frame update
    void Start()
    {
        
        hitPoint = maxHitPoint;

        isInvulnerable = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (isInvulnerable)
        {
            timeSinceLastHit += Time.deltaTime;

            if (timeSinceLastHit > invulnerablyTime)
            {
                timeSinceLastHit = 0.0f;
                isInvulnerable = false;
            }
        }
    }

    public void GetDamage(int degat)
    {
        if(isInvulnerable)
            return;

            isInvulnerable = true;
         //Les degats
        hitPoint -= degat;

        if (hitPoint > 0)
        {
            gameObject.SendMessage("TakeDamage",SendMessageOptions.DontRequireReceiver);

        }

        else 
        {
            gameObject.SendMessage("Defeated",SendMessageOptions.DontRequireReceiver);
            Destroy(gameObject);
        }
    }
}
