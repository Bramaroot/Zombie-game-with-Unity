using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootAction : MonoBehaviour
{
    public int degatArme = 1;

    public float porteeTir = 200f;

    public float hitForce = 100f ; 

    private Camera fpsCam;

    //Cadence de tir -- temps entre chaque tir
    public float fireRate = .25f ;

    private float nextFire;

    //Pour determiner sur quel lq=ayer on peut tirer
    public LayerMask layerMask;



    // Start is called before the first frame update
    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            print(nextFire);

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f,.5f,.0f));

            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, porteeTir, layerMask))
            {
                print("Target");

                if(hit.rigidbody != null)
                {

                    hit.rigidbody.AddForce(-hit.normal * hitForce);

                    if (hit.collider.gameObject.GetComponent<ReceiveDamage>() != null)
                    {
                        hit.collider.gameObject.GetComponent<ReceiveDamage>().GetDamage(degatArme);

                    }


                }

            }


        }

    }
}
