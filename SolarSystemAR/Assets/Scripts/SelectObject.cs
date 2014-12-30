using UnityEngine;
using System.Collections;

public class SelectObject : MonoBehaviour {

    public GameObject info;
    public GameObject canvas;
    GameObject infobox;

    Ray selectRay;
    RaycastHit selectHit;
    int selectableMask;

    GameObject selected;
    Material selectedMaterial;
    Shader selectedShader;

    float t;

	// Use this for initialization
	void Awake () {
        selectableMask = LayerMask.GetMask("Selectable");
        selected = new GameObject("temp");
        selectedShader = Shader.Find("Diffuse");
        t = Time.time;
	}
	
	// Update is called once per frame
	void Update () { 
        if (/*true || */t + 1.0f < Time.time && /*Input.touchCount == 1*/ Input.GetMouseButtonDown(0))
        {
            t = Time.time;
            //Gets the raycast from the screen
            Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition/*Input.GetTouch(0).position*/);
            RaycastHit objhit;

            //Checks if the ray hit any object
            if (Physics.Raycast(touchRay, out objhit, Mathf.Infinity, selectableMask))
            {
                //Initialized selected if object wasn't previously selected.
                if (selected != objhit.transform.gameObject)
                {
                    if (selected.name != "temp")
                    {
                        //Returns original back to its color.
                        selected.renderer.material.shader = selectedShader;
                        destroyDescription();
                    }
                    else
                        Destroy(selected);
                   
                 

                    //Sets up new selected
                    selected = objhit.transform.gameObject;
                    selectedShader = selected.renderer.material.shader;
                    selected.renderer.material.shader = Shader.Find("Particles/Alpha Blended Premulitply");
                    this.createDescription();
                    Debug.Log(selected.name);
                }
                else if (selected == objhit.transform.gameObject)
                {
                    selected.renderer.material.shader = selectedShader;
                    selected = new GameObject("temp");
                    selectedShader = Shader.Find("Diffuse");
                    this.destroyDescription();
                }


            }
        }
	}

    void createDescription()
    {
        Debug.Log("Creating Description!");
        infobox = Instantiate(info) as GameObject;
        infobox.transform.SetParent(canvas.transform, false);

    }

    void destroyDescription()
    {
        Debug.Log("Destorying Description");
        Destroy(infobox);
    }
}
