using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class HighlightField
{
    private static Color32 normalColor = new(67, 3, 3, 255);
    private static Color32 higlightedColor = new(145, 43, 13, 255);

    public static void Highlight(SpriteRenderer sprite)
    {
        sprite.color = higlightedColor;
    }

    public static void Dim(SpriteRenderer sprite)
    {
        sprite.color = normalColor;
    }
    public static void White(SpriteRenderer sprite)
    {
        sprite.color = Color.white;
    }
}
