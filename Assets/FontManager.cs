using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class FontManager : MonoBehaviour
{
    public TextMeshProUGUI[] allTexts;
    public TMP_FontAsset[] allFonts;
    public TMP_FontAsset pixelFontAsset;
    public TMP_FontAsset normalFontAsset;
    public bool useDefaultFont = false;

    private static FontManager _instance;
    public static FontManager Instance
    {
        get
        {
            if (_instance == null)
                return null;
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        allTexts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        allFonts = new TMP_FontAsset[allTexts.Length];

        for (int i = 0; i < allTexts.Length; i++)
            allFonts[i] = allTexts[i].font;

    }
    public void ChangeAllFont()
    {
        useDefaultFont = !useDefaultFont;

        if (useDefaultFont)
        {
            for (int i = 0; i < allTexts.Length; i++)
            {
                if (allTexts[i])
                {
                    float fontSize = allTexts[i].fontSize;
                    allTexts[i].font = normalFontAsset;
                    allTexts[i].fontSize = fontSize * 2.5f;
                }
            }

        }
        else
        {
            for (int i = 0; i < allTexts.Length; i++)
            {
                if (allTexts[i] && allFonts[i])
                {
                    float fontSize = allTexts[i].fontSize;
                    allTexts[i].font = allFonts[i];
                    allTexts[i].fontSize = fontSize / 2.5f;
                }
            }
        }
    }
}
