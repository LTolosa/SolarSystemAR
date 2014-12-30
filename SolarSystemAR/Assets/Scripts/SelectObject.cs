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
        selected = null;
        selectedShader = Shader.Find("Diffuse");
        t = Time.time;
	}
	
	// Update is called once per frame
	void Update () { 
        if (/*true || */t + 0.5f < Time.time && /*Input.touchCount == 1*/ Input.GetMouseButtonDown(0))
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
                    if (!object.ReferenceEquals(null, selected))
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
                    this.createDescription(selected.name);
                    Debug.Log(selected.name);
                }
                else if (selected == objhit.transform.gameObject)
                {
                    selected.renderer.material.shader = selectedShader;
                    selected = null;
                    selectedShader = Shader.Find("Diffuse");
                    this.destroyDescription();
                }


            }
            else if (!object.ReferenceEquals(null, selected))
            {
                selected.renderer.material.shader = selectedShader;
                selected = null;
                selectedShader = Shader.Find("Diffuse");
                this.destroyDescription();
            }
        }
	}

    void createDescription(string name)
    {
        infobox = Instantiate(info) as GameObject;
        infobox.transform.SetParent(canvas.transform, false);
        infobox.GetComponent<DescSize>().fillDesc(name);

    }

    void destroyDescription()
    {
        Destroy(infobox);
    }
}
