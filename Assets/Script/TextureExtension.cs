﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public static class TextureExtension
{
    public struct Point
    {
        public short x;
        public short y;
        public Point(short aX, short aY) { x = aX; y = aY; }
        public Point(int aX, int aY) : this((short)aX, (short)aY) { }
    }


    public static void FloodFillArea(this Texture2D aTex, int aX, int aY, Color aFillColor)
    {
        int w = aTex.width;
        int h = aTex.height;
        Color[] colors = aTex.GetPixels();
        Color refCol = colors[aX + aY * w];
        Queue<Point> nodes = new Queue<Point>();
        nodes.Enqueue(new Point(aX, aY));
        while (nodes.Count > 0)
        {
            Point current = nodes.Dequeue();
            for (int i = current.x; i < w; i++)
            {
                Color C = colors[i + current.y * w];
                if (C != refCol || C == aFillColor)
                    break;
                colors[i + current.y * w] = aFillColor;
                if (current.y + 1 < h)
                {
                    C = colors[i + current.y * w + w];
                    if (C == refCol && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    C = colors[i + current.y * w - w];
                    if (C == refCol && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
            for (int i = current.x - 1; i >= 0; i--)
            {
                Color C = colors[i + current.y * w];
                if (C != refCol || C == aFillColor)
                    break;
                colors[i + current.y * w] = aFillColor;
                if (current.y + 1 < h)
                {
                    C = colors[i + current.y * w + w];
                    if (C == refCol && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    C = colors[i + current.y * w - w];
                    if (C == refCol && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
        }
        aTex.SetPixels(colors);
    }

    public static void FloodFillBorder(this Texture2D aTex, int aX, int aY, Color aFillColor, Color aBorderColor)
    {
  
        int w = aTex.width;
        int h = aTex.height;
        Debug.Log(w);
        Debug.Log(h);
        Color[] colors = aTex.GetPixels();
        byte[] checkedPixels = new byte[colors.Length];
        Color refCol = aBorderColor;
        Queue<Point> nodes = new Queue<Point>();
        nodes.Enqueue(new Point(aX, aY));
        while (nodes.Count > 0)
        {
            Point current = nodes.Dequeue();

            for (int i = current.x; i < w; i++)
            {
                if (checkedPixels[i + current.y * w] > 0 || colors[i + current.y * w] == refCol)
                    break;
                colors[i + current.y * w] = aFillColor;
                checkedPixels[i + current.y * w] = 1;
                if (current.y + 1 < h)
                {
                    if (checkedPixels[i + current.y * w + w] == 0 && colors[i + current.y * w + w] != refCol)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    if (checkedPixels[i + current.y * w - w] == 0 && colors[i + current.y * w - w] != refCol)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
            for (int i = current.x - 1; i >= 0; i--)
            {
                if (checkedPixels[i + current.y * w] > 0 || colors[i + current.y * w] == refCol)
                    break;
                colors[i + current.y * w] = aFillColor;
                checkedPixels[i + current.y * w] = 1;
                if (current.y + 1 < h)
                {
                    if (checkedPixels[i + current.y * w + w] == 0 && colors[i + current.y * w + w] != refCol)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    if (checkedPixels[i + current.y * w - w] == 0 && colors[i + current.y * w - w] != refCol)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
        }
        aTex.SetPixels(colors);
    }

    public static Texture2D GetTextureFromSprite(Sprite sprite)
    {

        Texture2D croppedTexture = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
        Color[] pixels = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                (int)sprite.textureRect.y,
                                                (int)sprite.textureRect.width,
                                                (int)sprite.textureRect.height);
        croppedTexture.SetPixels(pixels);
        croppedTexture.Apply();
        return croppedTexture;
    }

    public static Texture2D textureFromSprite(Sprite sprite)
    {


        if (sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                         (int)sprite.textureRect.y,
                                                         (int)sprite.textureRect.width,
                                                         (int)sprite.textureRect.height);
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }


    public static void FloodFillAreaWithTolerance(this Texture2D aTex, int aX, int aY, Color aFillColor, float tolerance)
    {

        float tol = tolerance / 100;
        int w = aTex.width;
        int h = aTex.height;
        Color[] colors = aTex.GetPixels();
        Color refCol = colors[aX + aY * w];
        Queue<Point> nodes = new Queue<Point>();
        nodes.Enqueue(new Point(aX, aY));
        while (nodes.Count > 0)
        {
            Point current = nodes.Dequeue();
            //this goes right
            for (int i = current.x; i < w; i++)
            {
                Color C = colors[i + current.y * w];
                if (ColorTest(refCol, C, tol) == false || C == aFillColor)
                    break;
                colors[i + current.y * w] = aFillColor;
                if (current.y + 1 < h)
                {
                    C = colors[i + current.y * w + w];
                    if (ColorTest(refCol, C, tol) == true && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    C = colors[i + current.y * w - w];
                    if (ColorTest(refCol, C, tol) == true && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
            //this goes left
            for (int i = current.x - 1; i >= 0; i--)
            {
                Color C = colors[i + current.y * w];
                if (ColorTest(refCol, C, tol) == false || C == aFillColor)
                    break;
                colors[i + current.y * w] = aFillColor;
                if (current.y + 1 < h)
                {
                    C = colors[i + current.y * w + w];
                    if (ColorTest(refCol, C, tol) == true && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    C = colors[i + current.y * w - w];
                    if (ColorTest(refCol, C, tol) == true && C != aFillColor)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
        }
        aTex.SetPixels(colors);
    }

    public static bool AreTexturesSameByColor(Texture2D t1, Texture2D t2,int threshold_pixels)
    {
        Color32[] colors1 = t1.GetPixels32();
        Color32[] colors2 = t2.GetPixels32();
        int diff_pixels = 0; //Count of different pixels;
        for (int i = 0; i < colors1.Length; ++i)
        {
            Color32 c1 = colors1[i];
            Color32 c2 = colors2[i];

            if ((Mathf.Abs(c1.g - c2.g) < 3) && (Mathf.Abs(c1.b - c2.b) < 3) && (Mathf.Abs(c1.r - c2.r) < 3))
            {
                continue;

            }
            else
            {
                diff_pixels++;
                if (diff_pixels > threshold_pixels)
                {
                    Debug.Log(diff_pixels + " pixels differ");
                    return false; //No need to process further pixels
                }

            }
            
        }
        if (diff_pixels < threshold_pixels)
        {
            Debug.Log("Images match, only " + diff_pixels + " differ");
            return true;
        }
            
        else
            return false;
    }



    public static void FloodFillBorderWithTolerance(this Texture2D aTex, int aX, int aY, Color aFillColor, Color aBorderColor)
    {
        int w = aTex.width;
        int h = aTex.height;
        Color[] colors = aTex.GetPixels();
        byte[] checkedPixels = new byte[colors.Length];
        Color refCol = aBorderColor;
        Queue<Point> nodes = new Queue<Point>();
        nodes.Enqueue(new Point(aX, aY));
        while (nodes.Count > 0)
        {
            Point current = nodes.Dequeue();

            for (int i = current.x; i < w; i++)
            {
                if (checkedPixels[i + current.y * w] > 0 || colors[i + current.y * w] == refCol)
                    break;
                colors[i + current.y * w] = aFillColor;
                checkedPixels[i + current.y * w] = 1;
                if (current.y + 1 < h)
                {
                    if (checkedPixels[i + current.y * w + w] == 0 && colors[i + current.y * w + w] != refCol)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    if (checkedPixels[i + current.y * w - w] == 0 && colors[i + current.y * w - w] != refCol)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
            for (int i = current.x - 1; i >= 0; i--)
            {
                if (checkedPixels[i + current.y * w] > 0 || colors[i + current.y * w] == refCol)
                    break;
                colors[i + current.y * w] = aFillColor;
                checkedPixels[i + current.y * w] = 1;
                if (current.y + 1 < h)
                {
                    if (checkedPixels[i + current.y * w + w] == 0 && colors[i + current.y * w + w] != refCol)
                        nodes.Enqueue(new Point(i, current.y + 1));
                }
                if (current.y - 1 >= 0)
                {
                    if (checkedPixels[i + current.y * w - w] == 0 && colors[i + current.y * w - w] != refCol)
                        nodes.Enqueue(new Point(i, current.y - 1));
                }
            }
        }
        aTex.SetPixels(colors);
    }
    public static bool ColorTest(Color c1, Color c2, float tol)
    {
        float diffRed = Mathf.Abs(c1.r - c2.r);
        float diffGreen = Mathf.Abs(c1.g - c2.g);
        float diffBlue = Mathf.Abs(c1.b - c2.b);
        //Those values you can just divide by the amount of difference saturations (255), and you will get the difference between the two.

        float pctDiffRed = (float)diffRed / 255;
        float pctDiffGreen = (float)diffGreen / 255;
        float pctDiffBlue = (float)diffBlue / 255;

        //After which you can just find the average color difference in percentage.
        float diffPercentage = (pctDiffRed + pctDiffGreen + pctDiffBlue) / 3 * 100;

        if (diffPercentage >= tol)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public static ColorFrugal[] GetMainColorsFromTexture(Texture2D texture)
    {

        Color32[] colors = texture.GetPixels32();
        Debug.Log("Found Total colors:" + colors.Length);
        ColorFrugal[] sArray = new ColorFrugal[colors.Length];
        for (int j = 0; j < colors.Length; ++j)
        {
            sArray[j] = new ColorFrugal(colors[j].r, colors[j].g, colors[j].b, colors[j].a);

        }

        return GetHistogram(sArray,1200);
    }

    public static ColorFrugal[] GetHistogram(ColorFrugal[] colors, int min_pixels)
    {

        SortedDictionary<ColorFrugal, int> histogram = new SortedDictionary<ColorFrugal, int>(new TypeComparer());
        foreach (ColorFrugal item in colors)
        {
            if (histogram.ContainsKey(item))
            {
                histogram[item]++;
            }
            else
            {
                histogram[item] = 1;
            }

        }

        ArrayList sList = new ArrayList();
        foreach (KeyValuePair<ColorFrugal, int> pair in histogram)
        {

            if(pair.Value > min_pixels)
            {
                sList.Add(pair.Key);
                Debug.Log(pair.Key);
                Debug.Log(pair.Key + " occurred " + pair.Value + "times");
            }
            
        }

        ColorFrugal[] main_colors = sList.ToArray(typeof(ColorFrugal)) as ColorFrugal[];
        return main_colors;

    }
}

public class TypeComparer : IComparer<ColorFrugal>
{
    public int Compare(ColorFrugal x, ColorFrugal y)
    {
        if (x != null && y != null)
            return Equals(x, y);
       
        return 0;
    }

    public int Equals(ColorFrugal c1, ColorFrugal c2)
    {
        if (c1.r.Equals(c2.r) && c1.g.Equals(c2.g) && c1.b.Equals(c2.b) && c1.a.Equals(c2.a))
            return 0;
        else
            return -1;
    }
}