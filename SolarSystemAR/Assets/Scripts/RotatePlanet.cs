using UnityEngine;
using System.Collections;

public class RotatePlanet : MonoBehaviour {

    public float RotationSpeed;         //Rotation speed of planet. Calculated same as earth
    public static float speed = 1f;     //Speed of rotation
    const float earthRot = 463.831f;    //Units in m/s. Planet radius * 2pi/#of days/24hours/60min/60s
	
	void Update () {
        //Only rotate around y axis
        this.transform.Rotate(0,
                              speed *  360f * RotationSpeed/earthRot * Time.deltaTime, 
                              0);
	
	}
}
