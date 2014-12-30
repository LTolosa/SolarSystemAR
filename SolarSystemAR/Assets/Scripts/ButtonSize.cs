using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonSize : MonoBehaviour {

    float sw;   //Screen width variable
    float sh;   //Screen height variable

    public static float currSpeed = 1f; //Speed on button.

    public void OnGUI()
    {
        // Rect transform of the button
        RectTransform r = this.GetComponent<RectTransform>();

        //Screen dimensions, checked each time in case of screen rotation
        sw = Screen.width;
        sh = Screen.height;

        //Gets short and long sides from dimensions
        float short_length = sw < sh ? sw : sh;
        float long_length  = sw > sh ? sw : sh;

        //Determines where the buttons are located
        //Animate button in the bottom right, Speed button in the bottom left.
        float leftS = this.gameObject.tag == "Animate" ? 0.8f : 0.1f;
        float topS  = 0.15f;

        //Determines the scale of the buttons' height and width
        //Animate button is short and wide, Speed button is closer to a square
        float widthS = this.gameObject.tag == "Animate" ? long_length * 0.2f : short_length * 0.15f;
        float heightS = short_length * 0.1f;
  
        //Places and scales button
        r.position = new Vector3(sw * leftS, sh * topS, r.position.z);
        r.sizeDelta = new Vector2(widthS, heightS);

        //Scales the text
        Text text = transform.FindChild("Text").GetComponent<Text>();

        text.fontSize = (int) Mathf.Round(short_length * 0.04f);

       

    }

    public void updateSpeed()
    {
        //Calculates new speed.
        float newSpeed = currSpeed * 2f;
        //Reverts to 1/4 if greater than 4.
        if (newSpeed > 4f)
            newSpeed = 1f / 4f;
        //Updates speed
        currSpeed = newSpeed;
        //Update text.
        string text_string = newSpeed + "x";
        Text text = transform.FindChild("Text").GetComponent<Text>();
        text.text = text_string;

    }
}
