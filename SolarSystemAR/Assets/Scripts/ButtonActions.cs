using UnityEngine;
using System.Collections;

public class ButtonActions : MonoBehaviour {

    float speed;

    void Awake()
    {
        speed = 1f;
    }

    public void stopOrbit()
    {
        OrbitPlanet.orbit = !OrbitPlanet.orbit;
    }

    public void speedChange(int scale)
    {
        float newSpeed = 2 * speed;
        if (newSpeed > 4f)
            newSpeed = 1f / 4f;
        speed = newSpeed;

        OrbitPlanet.speed = speed;
        RotatePlanet.speed = speed;

    }


}
