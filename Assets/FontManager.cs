using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontManager : MonoBehaviour
{
    public TextMeshProUGUI[] allTexts;

    public TMP_FontAsset pixelFontAsset;
    public TMP_FontAsset normalFontAsset;
        private bool pixelfont;


    public static FontManager instance;


    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        FindObjectsOfType<TextMeshProUGUI>().CopyTo(allTexts,2);
    }

    public void ChangeAllFont()
    {
        pixelfont = !pixelfont;
        for (int i = 0; i < allTexts.Length; i++)
        {
            allTexts[i].font = pixelfont ? pixelFontAsset : normalFontAsset;
        }
    }
}
