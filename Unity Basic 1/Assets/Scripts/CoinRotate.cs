using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotate : MonoBehaviour
{

    public float speed = 9;
    public int random;


    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(3, (int)speed); //Get some random speed
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(Vector3.up * random); //Rotate the coin based on random speed
    }
}
