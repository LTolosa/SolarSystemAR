using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrbitPlanet : MonoBehaviour {


    public static bool orbit = true;    //Static boolean to keep the planets orbitings
    public float orbitPeriod;           //Days it takes to make one complete orbit
    public static float speed = 1f;     //Scales speed to animate faster/slower.
    
    float angle;                        //Angle the object is at.
    float angularSpeed;                 //Angular speed in rad/s normalized by earthOrbit;
    float radius;                       //Radius of the circular orbit. CIRCULAR ORBITS.

    Queue<GameObject> trail;


    const float earthOrbit = 365.256363004f;    //Number of days for earth's orbit

	// Use this for initialization
	void Start () {
        angle = (float) Random.Range(0,  2*Mathf.PI);

        //Angular speed with earth normalization
        //Makes it so 1 earth orbit = 1 year
        angularSpeed = 2*Mathf.PI/(orbitPeriod/earthOrbit * 60.0f);
        
        //Calculates radius of orbit.
        //ORBITS WILL BE CIRCULAR FOR NOW(or forever :])
        radius = Mathf.Sqrt(Mathf.Pow(transform.localPosition.x, 2) 
                          + Mathf.Pow(transform.localPosition.y, 2)
                          + Mathf.Pow(transform.localPosition.z, 2));
        trail = new Queue<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        if (orbit)
        {
            //Calculates angle in radians
            angle += angularSpeed * Time.deltaTime * speed;

            //Applies angle to position
            transform.localPosition = new Vector3(radius * Mathf.Cos(angle), 
                                                  transform.localPosition.y, 
                                                  radius * Mathf.Sin(angle));
            /*
            GameObject g = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            g.transform.SetParent(this.transform, false);
            g.transform.position = transform.position;
            g.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            trail.Enqueue(g);
             */ 
        }

        //if (trail.Count > 100)
        //    trail.Dequeue();
	}
}
