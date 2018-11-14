using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FloodFillSprite : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public string s;
    //Texture2D texture;
    // Use this for initialization
    void Start() {
        Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = TextureExtension.textureFromSprite(sprite);
        TextureExtension.FloodFillBorder(texture, 80, 80, Color.green,Color.blue);
        texture.Apply();
    }

    //Detect current clicks on the GameObject (the one with the script attached)
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        Vector2 localCursor;
        var rect1 = GetComponent<RectTransform>();
        var pos1 = pointerEventData.position;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rect1, pos1,
            null, out localCursor))
            return;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), pos1, pointerEventData.pressEventCamera, out localCursor))
            return;
        int xpos = (int)(localCursor.x);
        int ypos = (int)(localCursor.y);

        //if (xpos < 0) xpos = xpos + (int)rect1.rect.width / 2;
        //else xpos += (int)rect1.rect.width / 2;

        //if (ypos > 0) ypos = ypos + (int)rect1.rect.height / 2;
        //else ypos += (int)rect1.rect.height / 2;

        Debug.Log("Correct Cursor Pos: " + xpos + " " + ypos);
        //Debug.Log(name + "Game Object Click in Progress");
        Texture2D texture = TextureExtension.GetTextureFromSprite(gameObject.GetComponent<Image>().sprite);
        TextureExtension.FloodFillArea(texture, 40, 40, Color.red);
        texture.Apply();
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
       // Debug.Log(name + "No longer being clicked");
    }
    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0))
        {

            //screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            //offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            //Debug.Log(screenPoint);
            //Debug.Log(offset);
            //Debug.Log("SS");
            //RaycastHit hit;
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (Physics.Raycast(Input.mousePosition, Vector3.back, out hit))
            //{
            //    Vector3 hitPos = Camera.main.WorldToScreenPoint(hit.point);
            //    Debug.Log(hitPos);
            //}

            //Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //int flood_fill_x = (int)(pos.x - gameObject.transform.position.x);
            //int flood_fill_y = (int)(pos.y - gameObject.transform.position.y);
            //Debug.Log(flood_fill_x);
            //Debug.Log(flood_fill_y);

            //Texture2D texture = TextureExtension.GetTextureFromSprite(gameObject.GetComponent<Image>().sprite);
            //TextureExtension.FloodFillArea(texture, flood_fill_x, flood_fill_y, Color.red);
            //texture.Apply();

            //Debug.Log(pos);
            //Camera _cam = Camera.main;
            //Ray ray = _cam.ScreenPointToRay(pos);
            //RaycastHit hit;
            //Physics.Raycast(_cam.transform.position, ray.direction, out hit, 10000.0f);
            //Color c;
            //if (hit.collider)
            //{
            //    Debug.Log(hit.point);
            //    //Texture2D tex = (Texture2D)hit.collider.gameObject.renderer.material.mainTexture; // Get texture of object under mouse pointer
            //    //c = tex.GetPixelBilinear(hit.textureCoord2.x, hit.textureCoord2.y); // Get color from texture
            //}
        }
    }


    private void OnMouseDown()
    {
        //Vector3 clickedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(clickedPosition);
        //Vector3 clickedPosition1 = Input.mousePosition;
        //Debug.Log(clickedPosition1);
    }

   
}
