using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum state
    {
        LOOKFOR,
        GOTO,
        ATTACK,
    }

    public state curState;
    public float speed = 5f;
    public float gotodist = 13f;
    public float attckdist = 2f;
    public float attacktimer = 2f;

    public Transform target;
    public string PlayerTag = "Player";
    private float curtime;
    private Player PlayerScript;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        //Our Start

        target = GameObject.FindGameObjectWithTag(PlayerTag).transform;
        curtime = attacktimer;
        if (target != null)
        {
            PlayerScript = target.GetComponent<Player>();
        }

        while (true)
        { //This is where update happens

            switch (curState)
            {
                case state.LOOKFOR: Lookfor();
                    break;

                case state.GOTO: Goto();
                    break;

                case state.ATTACK: Attack();
                    break;

            }

            yield return 0;
        }
        

    }

    void Lookfor()
    {
        print("Looking For Player");
        if (Vector3.Distance(target.position, transform.position) < (gotodist))
        {
            curState = state.GOTO;

        }

    }
    void Goto()
    {
        print("Found The Bastard!");
        transform.LookAt(target); //To make enemy look at target

        //Ray casting for Line of sight
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit Ray;

        if(Physics.Raycast(transform.position,fwd,out Ray))
        {
            if(Ray.transform.tag!=PlayerTag)
            {
                curState = state.LOOKFOR;
                return;
            }
        }

        //Moving to target
        if (Vector3.Distance(target.position, transform.position) > (attckdist))
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else //Attacking Target
        {
            curState = state.ATTACK;
        }
    }
    void Attack()
    {
        print("Attck that a**hole");
        curtime = curtime - Time.deltaTime;
        transform.LookAt(target);

        //Attacking Target
        if (curtime < 0)
        {
            PlayerScript.health--;
            curtime = attacktimer;

        }

        //If Attackdist increases. Back to Goto State

        if (Vector3.Distance(target.position, transform.position) > (attckdist))
        {
            curState = state.GOTO;
        }

    }

    void Update()
    {
        if(PlayerScript.health==0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Basic Player");
        }
    }
}
