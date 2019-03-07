using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool dead; // Alive or Dead
    public int health; //Total Health
    public float gravity = 10;
    public float speed = 10;
    public float maxvelocitychange = 10;
    public float jumpheight = 2;
    public int point;

    private bool grounded;

    private Transform playerTransform;
    private readonly GameObject enemy;
    private Rigidbody _rigidbody;



    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _rigidbody.freezeRotation = true;



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        playerTransform.Rotate(0, Input.GetAxis("Horizontal"), 0); //For AD keys

        Vector3 targetvelocity = new Vector3(0, 0, Input.GetAxis("Vertical")); //For WS keys
        
        targetvelocity = playerTransform.TransformDirection(targetvelocity); //For Moving to a certain velocity
        targetvelocity = targetvelocity * speed;


        Vector3 velocity = _rigidbody.velocity; //Giving regidbody a velocity
        Vector3 velocitychange = targetvelocity - velocity; //Allowing for changes in velocity

        velocitychange.x = Mathf.Clamp(velocitychange.x, -maxvelocitychange, maxvelocitychange); //Clamping x
        velocitychange.z = Mathf.Clamp(velocitychange.z, -maxvelocitychange, maxvelocitychange); //Clamping z
        velocitychange.y = 0; //Y =0 since no need for velocity on that axis

        _rigidbody.AddForce(velocitychange, ForceMode.VelocityChange); //Adding a force to rigidbody WS velocity

        if (Input.GetButton("Jump") && grounded==true) //Jumping whenever Jump is pressed. Checking for grounded as well
        {
            _rigidbody.velocity = new Vector3(velocity.x, CalculateJump(), velocity.z);
        }

        _rigidbody.AddForce(new Vector3(0, -gravity * _rigidbody.mass, 0)); //Adding Gravity
        grounded = false;


    }
    float CalculateJump() //Calculating Jump
    {
        float Jump = Mathf.Sqrt(2 * jumpheight * gravity);
        return Jump;

    }

    private void OnCollisionStay(Collision collision) //On Collision to stop from falling into the ground
    {
        grounded = true;

    }

    private void OnTriggerEnter(Collider ABC) //Coin Trigger
    {
        if(ABC.tag=="Coin")
        {
            point = point + 5;

            Destroy(ABC.gameObject);

        }
    }
}


