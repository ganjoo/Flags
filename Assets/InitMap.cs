using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InitMap : MonoBehaviour {

    private GameObject original_map;
    private GameObject editable_map;

    public Sprite Initial_State;
    private Texture2D Initial_Texture;

    // Use this for initialization
    void Start () {
         original_map = GameObject.Find("OriginalMap");
        editable_map = GameObject.Find("EditableMap");
        original_map.SetActive(false);
        editable_map.SetActive(true);       
        SetColorButtons();

        Initial_Texture = new Texture2D((int)editable_map.GetComponent<Image>().sprite.rect.width, (int)editable_map.GetComponent<Image>().sprite.rect.height,
            TextureExtension.textureFromSprite(editable_map.GetComponent<Image>().sprite).format,false);
        Graphics.CopyTexture(TextureExtension.textureFromSprite(editable_map.GetComponent<Image>().sprite), Initial_Texture);
        Initial_Texture.Apply();
        Reset();
        

    }

    void SetColorButtons()
    {

        Texture2D t1 = TextureExtension.textureFromSprite(original_map.GetComponent<Image>().sprite);
        ColorFrugal[] colors_comparable = TextureExtension.GetMainColorsFromTexture(t1);

        Debug.Log("Colors found in original image:" + colors_comparable.Length);
        for (int i = 0; i < colors_comparable.Length; ++i)
        {
            string button_name = "Color" + (i+1).ToString();
            GameObject btn = GameObject.Find(button_name);
            btn.GetComponent<Image>().color = colors_comparable[i].ToColor32();
            Debug.Log(colors_comparable[i].ToColor32());
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

    public void Reset()
    {
        editable_map.GetComponent<Image>().sprite = Sprite.Create(Initial_Texture, new Rect(0.0f, 0.0f, Initial_Texture.width, Initial_Texture.height), new Vector2(0.0f, 0.0f));
        Initial_Texture.Apply();
    }

    public  bool AreImagesMatching(Texture2D current_texture)
    {
        Texture2D t1 = TextureExtension.textureFromSprite(original_map.GetComponent<Image>().sprite);
        //Texture2D t2 = TextureExtension.textureFromSprite(editable_map.GetComponent<Image>().sprite);

        return (TextureExtension.AreTexturesSameByColor(t1, current_texture,3000));
        
    }

    IEnumerator FlashHint()
    {
        //AreImagesMatching();
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
