﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorStatus : MonoBehaviour {

    static public Color current_color = Color.white;
    static public float flood_fill_tolerance = 0.3f;
    // Use this for initialization
    void Start () {

        GameObject selected_color = GameObject.Find("selected_color");
        selected_color.GetComponent<Image>().color = ColorStatus.current_color;
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseDown()
    {
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().color = ColorSprite.current_color;

        }

    }

    public void OnClicked(Button button)
    {
        GameObject btn = GameObject.Find(button.name);
        var color = btn.GetComponent<Image>().color;
        ColorStatus.current_color = color;
        GameObject selected_color = GameObject.Find("selected_color");
        selected_color.GetComponent<Image>().color = ColorStatus.current_color;
        print(button.name);
        Debug.Log("Object clicked " + ColorSprite.current_color);
    }

   
}
