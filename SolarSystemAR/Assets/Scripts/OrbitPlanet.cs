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
    float worldRadius;

    const float earthOrbit = 365.256363004f;    //Number of days for earth's orbit
    GameObject planetOrbit;

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
        worldRadius = Mathf.Sqrt(Mathf.Pow(transform.position.x, 2)
                               + Mathf.Pow(transform.position.z, 2));

        /*
        if (name == "Earth")
        {
            planetOrbit = this.transform.FindChild("EarthOrbit").gameObject;
            LineRenderer trail = planetOrbit.GetComponent<LineRenderer>();
            trail.SetVertexCount(41);
            float angDif = 2*Mathf.PI / 40f;
            int j = 0;
            for (float i = 0; i <= 2*Mathf.PI + 2*Mathf.PI/40f; i += 2*Mathf.PI/40f)
            {
                trail.SetPosition(j++, new Vector3(worldRadius * Mathf.Cos(i),
                                                   this.transform.position.y,
                                                   worldRadius * Mathf.Sin(i)));
            }

        }
         */
     

	}
	
	// Update is called once per frame
	void Update () {
        if (orbit)
        {
           
            //Calculates angle in radians
            angle -= angularSpeed * Time.deltaTime * speed;

            //Applies angle to position
            transform.localPosition = new Vector3(radius * Mathf.Cos(angle), 
                                                  transform.localPosition.y, 
                                                  radius * Mathf.Sin(angle));
        }

        if (angle > 2 * Mathf.PI) angle = angle - 2*Mathf.PI;
        if (angle < 0) angle = angle + 2 * Mathf.PI;
        /*
        posdiff = newpos - oldpos;
        for (if=1;if<posarray.size; ++if)
        {
            posarray[if-1]=posarray[if]+posdiff;
        }
        posdiff.last()=posdiff;
        */
	}
}
