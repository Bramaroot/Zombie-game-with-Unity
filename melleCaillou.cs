using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melleCaillou : MonoBehaviour
{

    public int damage = 1;

    public LayerMask layerMask;

    public bool isAttacking = false;

    // Start is called before the first frame update
    public void StartAttack()
    {
        isAttacking = true;
    }

     public void StopAttack()
    {
        isAttacking = false;
    }


    private void onTriggerEnter(Collider other)
    {
        if (!isAttacking)
        return;

        if ((layerMask.value & (1<< other.gameObject.layer)) == 0 )
            return;

            other.GetComponent<ReceiveDamage>().GetDamage(damage);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
