using UnityEngine;
using System.Collections;

public class SelectObject : MonoBehaviour {

    public GameObject info;     //Information pre-fab
    public GameObject canvas;   //Canvas on the hierarchy
    GameObject infobox;         //Newly created info box

    int selectableMask;         //Layer that has selectable objects

    GameObject selected;        //Previously selected GameObject
    Shader selectedShader;      //Shader of previously selected object

    float t;                    //Time variable

	// Use this for initialization
	void Awake () {
        //Initialize variables
        selectableMask = LayerMask.GetMask("Selectable");
        selected = null;
        selectedShader = Shader.Find("Diffuse");
        t = Time.time;
	}
	
	// Update is called once per frame
	void Update () { 
        //Only allow touch every 0.5 seconds
        if (t + 0.5f < Time.time && Input.GetMouseButtonDown(0))
        {
            t = Time.time;
            //Gets the raycast from the screen
            Ray touchRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit objhit;

            //Checks if the ray hit any object
            if (Physics.Raycast(touchRay, out objhit, Mathf.Infinity, selectableMask))
            {
                //Initialized selected if object wasn't previously selected.
                if (selected != objhit.transform.gameObject)
                {
                    if (!object.ReferenceEquals(null, selected))
                    {
                        //Returns original back to its color.
                        selected.renderer.material.shader = selectedShader;
                        destroyDescription();
                    }

                    //Sets up new selected
                    selected = objhit.transform.gameObject;
                    selectedShader = selected.renderer.material.shader;
                    selected.renderer.material.shader = Shader.Find("Particles/Alpha Blended Premulitply");
                    this.createDescription(selected.name);
                    Debug.Log(selected.name);
                }
                else if (selected == objhit.transform.gameObject)
                {
                    //Removes selection if object is reselected
                    selected.renderer.material.shader = selectedShader;
                    selected = null;
                    selectedShader = Shader.Find("Diffuse");
                    this.destroyDescription();
                }


            }
            else if (!object.ReferenceEquals(null, selected))
            {
                //Removes selection if touch in empty area.
                selected.renderer.material.shader = selectedShader;
                selected = null;
                selectedShader = Shader.Find("Diffuse");
                this.destroyDescription();
            }
        }
	}

    //Creates the info box and fills description
    void createDescription(string name)
    {
        infobox = Instantiate(info) as GameObject;
        infobox.transform.SetParent(canvas.transform, false);
        infobox.GetComponent<DescSize>().fillDesc(name);

    }

    //Destroys info box
    void destroyDescription()
    {
        Destroy(infobox);
    }
}
