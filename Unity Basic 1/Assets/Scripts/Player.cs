using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name; // Name of our player

    /*All the following define our charecteristics*/
    public float Attackspeed; // Float for attack speed
    public int Attack; //Integer for Attack Power
    public int health; // Integer for our health
    
    /*All the following define Player World values*/
    public float Gravity = 10; 
    public float Speed = 10;
    public float max = 10;

    private readonly bool dead; // True or False for alive or dead
    private Transform PlayerTransform;
    private GameObject Enemy;
    



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
