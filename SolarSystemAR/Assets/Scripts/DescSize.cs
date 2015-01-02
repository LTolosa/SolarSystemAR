using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

public class DescSize : MonoBehaviour {

    float sw;   //Screen width  variable
    float sh;   //Screen height variable

    void OnGUI()
    {
        //Get screen width and height
        //Does this everytime to check for rotation
        int sw = Screen.width;
        int sh = Screen.height;

        //Shorter length of screen width and height
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
        //Apply name to top of description
        transform.FindChild("Name").GetComponent<Text>().text = name;

        //Path of information text files
        string sourcePath = System.IO.Path.Combine(Application.streamingAssetsPath,
                                            "Information/" + name + ".txt");
        //String to contain the text of the file
        string fileText = "";

        //Check for JAR files on android
        if (sourcePath.Contains("://"))
        {
            //Android
            //Use WWW to read file.
            WWW www = new WWW(sourcePath);

            while (!www.isDone) { }

            //Copy file to fileText string
            if (string.IsNullOrEmpty(www.error))
                fileText = www.text;
        }
        else
        {
            //For everything else
            if (System.IO.File.Exists(sourcePath))
            {
                //Read file and copy to file text
                StreamReader file = new StreamReader(sourcePath);
                fileText = file.ReadToEnd();
            }
        }

        //Create a list of all possible descriptions 
        List<string> info = new List<string>();

        //Iterate through each description addin to list.
        while(fileText.Contains("=="))
        {
            //Find '==' signs that separate descriptions and add to list
            int index = fileText.IndexOf("==");
            info.Add(fileText.Substring(0, index));
            //Remove everything added to list      
            fileText = fileText.Remove(0, index);

            //Cuts out the '==' and determines if there is a new line character
            int cut = fileText.Contains("\n") ? 4 : 2;
            fileText = fileText.Remove(0, cut);
        }
        if (fileText != "") info.Add(fileText);


        //Choose randomly to display on description
        //Smarter way to do this later?
        transform.FindChild("Description").GetComponent<Text>().text =
                                         info[Random.Range(0, info.Count)];


    }
}
