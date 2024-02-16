using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;


public class MonsterControler : MonoBehaviour
{

    public GameObject player;

    public melleCaillou caillouZombie;

    UnityEngine.AI.NavMeshAgent navMeshAgent;

    //Composants
    Animator animator;

    //stand = idle for waiting
    const string STAND_STATE = "stand";

    const string TAKE_DAMAGE_STATE = "damage";

    const string DEFEATED_STATE = "defeated";

    const string WALK_STATE = "walk";

    const string ATTACK_STATE = "attack";


    

    public string currentAction; // pour savoir l'etat

    private void Awake()
    {
        currentAction = STAND_STATE ;

        animator = GetComponent<Animator>();

        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        player = FindObjectOfType<script>().gameObject;

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Stand()
    {
        //Renitialise les param de l'amim
        ResetAnimation();

        //actu on est a stand
        currentAction = STAND_STATE ;

        animator.SetBool("stand",true);
    }

    private void TakeDamage()
    {
        //Renitialise les param de l'amim
        ResetAnimation();

        //actu on est a stand
        currentAction = TAKE_DAMAGE_STATE ;

        animator.SetBool("damage",true);
    }

    private void Defeated()
    {
        //Renitialise les param de l'amim
        ResetAnimation();

        //actu on est a stand
        currentAction = DEFEATED_STATE ;

        animator.SetBool(DEFEATED_STATE,true);
    }

    private void TakingDamage()
    {
        if(this.animator.GetCurrentAnimatorStateInfo(0).IsName(TAKE_DAMAGE_STATE))
        {

            float normalizedTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

            //fin de l'animation
            if(normalizedTime>1)
               { Stand();
                
                }
        } 
    }
private void attacking()
{
    if (this.animator.GetCurrentAnimatorStateInfo(0).IsName(ATTACK_STATE))
    {
        float normalizedTime = this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;

        if (normalizedTime > 1)
        {
            caillouZombie.StopAttack();
            Stand();
            return;
        }
        caillouZombie.StartAttack();

    }

}
    // Update is called once per frame
    private void Update()
    {
        if (currentAction == DEFEATED_STATE)
        {
            navMeshAgent.ResetPath();
            return;
        }

        if (currentAction == TAKE_DAMAGE_STATE)
        {
            navMeshAgent.ResetPath();
            TakingDamage();
            return;
        }

        if (player != null)
        {
            if (MovingToTarget())
            {
                // en train de bouger
                return;
            }
            else
            {
                if(currentAction != ATTACK_STATE && currentAction != TAKE_DAMAGE_STATE)
                {
                    Attack();
                    return;
                }
                if(currentAction == ATTACK_STATE)
                {
                    attacking();
                    return;
                }
                Stand();
            }
        }
        // pluss tard
    }

    private void ResetAnimation()
    {
        animator.SetBool(STAND_STATE,false);
                animator.SetBool(TAKE_DAMAGE_STATE,false);
                animator.SetBool(DEFEATED_STATE,false);
                animator.SetBool(WALK_STATE,false);
                animator.SetBool(ATTACK_STATE, false);





    }

    private bool MovingToTarget()
    {
        navMeshAgent.SetDestination(player.transform.position);


        if (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            if(currentAction != WALK_STATE)
                walk();
        }
        else 
        {
            RotateToTarget(player.transform);
            return false;
        }
        return true;
    }

    private void walk()
    {
        ResetAnimation();
        currentAction = WALK_STATE;
        animator.SetBool(WALK_STATE,true);
    }
    private void Attack()
    {
        ResetAnimation();

        currentAction = ATTACK_STATE;

        animator.SetBool(ATTACK_STATE, true);
       
    }

    private void RotateToTarget(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0 , direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3f);
    }


}
