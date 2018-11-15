using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InitMap : MonoBehaviour {

    private GameObject original_map;
    private GameObject editable_map;

    // Use this for initialization
    void Start () {
         original_map = GameObject.Find("OriginalMap");
        editable_map = GameObject.Find("EditableMap");
        original_map.SetActive(false);
        editable_map.SetActive(true);       
        SetColorButtons();
    }

    void SetColorButtons()
    {
        Texture2D t1 = TextureExtension.textureFromSprite(original_map.GetComponent<Image>().sprite);
        Color32[] colors = TextureExtension.GetColorsFromTexture(t1);
        Debug.Log("Colors found in original image:" + colors.Length);
        for(int i = 0; i < colors.Length; ++i)
        {
            string button_name = "Color" + (i+1).ToString();
            GameObject btn = GameObject.Find(button_name);
            btn.GetComponent<Image>().color = colors[0];
            Debug.Log(colors[0]);
        }
    }
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowHideHint()
    {

        original_map.SetActive(true);
        editable_map.SetActive(false);
        StartCoroutine("FlashHint");
    }

    public  bool AreImagesMatching()
    {
        Texture2D t1 = TextureExtension.textureFromSprite(original_map.GetComponent<Image>().sprite);
        Texture2D t2 = TextureExtension.textureFromSprite(editable_map.GetComponent<Image>().sprite);

        return (TextureExtension.AreTexturesSameByColor(t1, t2));
        
    }

    IEnumerator FlashHint()
    {
        int current_time = 0;
        while (current_time < 3 )
        {
            current_time++;
            yield return new WaitForSeconds(1f);
        }
        original_map.SetActive(false);
        editable_map.SetActive(true);


    }
}
