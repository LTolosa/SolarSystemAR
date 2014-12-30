using UnityEngine;
using System.Collections;

public class RotatePlanet : MonoBehaviour {

    public float RotationSpeed;
    public static float speed = 1f;
    const float earthRot = 463.831f;    //Units in m/s. Planet radius * 2pi/#of days/24hours/60min/60s
	
	// Update is called once per frame
	void Update () {

        this.transform.Rotate(0,speed *  360f * RotationSpeed/earthRot * Time.deltaTime, 0);
	
	}
}
