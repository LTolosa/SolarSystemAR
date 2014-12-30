using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public class DescSize : MonoBehaviour {

    float sw;
    float sh;

    void OnGUI()
    {
        //Get screen width and height
        //Does this everytime to check for rotation
        int sw = Screen.width;
        int sh = Screen.height;

        int short_length = sw < sh ? sw : sh;

        RectTransform rect = this.GetComponent<RectTransform>();

        //Box Is gonna be places 70% from the left and 60% from the top
        float leftS = 0.7f;
        float topS = 0.6f;

        //Width is 40% of screen size
        //Height is 60%
        float widthS = sw * 0.4f;
        float heightS = sh * 0.6f;

        //Places and scales button
        rect.anchoredPosition = new Vector3(sw * leftS, 
                                            sh * topS, 
                                            rect.position.z);
        rect.sizeDelta = new Vector2(widthS, heightS);
        
        //Get rect transforms of the text children
        Transform name = transform.FindChild("Name");
        Transform desc = transform.FindChild("Description");
        RectTransform nameRect = name.GetComponent<RectTransform>();
        RectTransform descRect = desc.GetComponent<RectTransform>();

        //place and scale text on the newly scaled box.
        nameRect.anchoredPosition = new Vector3(widthS * 0.5f,-heightS * 0.1f, 0f);
        nameRect.sizeDelta =        new Vector2(widthS * 0.8f, heightS * 0.2f);

        descRect.anchoredPosition = new Vector3(widthS * 0.5f, -heightS * 0.6f, 0f);
        descRect.sizeDelta =        new Vector2(widthS * 0.8f, heightS * 0.8f);

        name.GetComponent<Text>().fontSize = (int)Mathf.Round(short_length * 0.04f);
        desc.GetComponent<Text>().fontSize = (int)Mathf.Round(short_length * 0.04f);
    }

    public void fillDesc(string name)
    {
        transform.FindChild("Name").GetComponent<Text>().text = name;

        string path = Application.persistentDataPath + "Information\\" + name + ".txt";

        StreamReader file = new StreamReader(path);
        string sLine = "";
        List<string> text = new List<string>();
        text.Add("");
        int i = 0;
        while (sLine != null)
        {
            sLine = file.ReadLine();
            if (sLine != null)
            {
                if (sLine.Equals("=="))
                {
                    text.Add("");
                    i++;
                }
                else
                    text[i] += sLine + " ";
            }
        }
        int index = 0;
        if (i > 0) index = Random.Range(0, i);

        Debug.Log("Index: " + index + "\nlist length: " + text.Count);
        Transform desc = transform.FindChild("Description");
        desc.GetComponent<Text>().text = text[index];


    }
}
