using UnityEngine;
using System.Collections;

public class ButtonActions : MonoBehaviour {

    float speed;    //Speed or orbit and rotation

    void Awake()
    {
        speed = 1f; //Initial speed is at 1
    }

    public void stopOrbit()
    {
        OrbitPlanet.orbit = !OrbitPlanet.orbit; //Makes orbit false
    }

    public void speedChange(int scale)
    {
        //Doubles speed
        float newSpeed = 2 * speed;
        //Reset to 1/4 if speed is greater than 4.
        if (newSpeed > 4.0f)
            newSpeed = 1.0f / 4.0f;
        speed = newSpeed;

        //Set new speed for rotation and orbit
        OrbitPlanet.speed = speed;
        RotatePlanet.speed = speed;

    }


}
