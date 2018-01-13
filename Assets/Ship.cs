using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    //some public vars that can be used to tune the Ship's movement
    public Vector3 forceVector;
    public float rotationSpeed;
    public float rotation;
    public GameObject bullet;

	// Use this for initialization
	void Start () {
        //Vector3 default inits to all 0s
        forceVector.x = 1.0f;
        rotationSpeed = 2.0f;
	}

    /* force changes to rigid body physics params should be done through 
     * FixedUpdate(), not the Update() method
     * 
     */
    private void FixedUpdate()
    {
        //force thruster
        if(Input.GetAxisRaw("Vertical") > 0)
        {
            GetComponent<Rigidbody>().AddRelativeForce(forceVector);
        }
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            rotation += rotationSpeed;
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            GetComponent<Rigidbody>().MoveRotation(rot);
            //GetComponent<Rigidbody>().transform.Rotate(new Vector3(0, 2, 0));
        }

        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            rotation -= rotationSpeed;
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            GetComponent<Rigidbody>().MoveRotation(rot);
            //GetComponent<Rigidbody>().transform.Rotate(new Vector3(0, -2, 0));
        }
    }


    //// Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            Debug.Log("Fire! " + rotation);
            /* we don't want to spawn a Bullet inside our ship, so 
             * some simple trig is done here to spawn the bullet at
             * the tip of where the ship is pointed.
             */
            Vector3 spawnPos = gameObject.transform.position;
            spawnPos.x += 1.5f * Mathf.Cos(rotation * Mathf.PI / 180);
            spawnPos.z -= 1.5f * Mathf.Sin(rotation * Mathf.PI / 180);

            //instantiate the Bullet
            GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;

            //get the bullet script component of the new bullet instance
            BulletScript b = obj.GetComponent<BulletScript>();

            //set the direction the bullet will travel in
            Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
            b.heading = rot;
        }
    }
}
