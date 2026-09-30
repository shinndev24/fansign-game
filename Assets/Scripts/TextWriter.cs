using System;
using System.Collections;
using UnityEngine;

public class TextWriter : MonoBehaviour
{
    public static TMPro.TMP_Text viewText;
    public static bool runTextPrint;
    public static int charCount;
    public static bool isTyping;
    [SerializeField] string transferText;
    [SerializeField] int internalCount;

    void Update()
    {
        internalCount = charCount;
        charCount = GetComponent<TMPro.TMP_Text>().text.Length;
        if(runTextPrint == true)
        {
            runTextPrint = false;
            viewText = GetComponent<TMPro.TMP_Text>();
            transferText = viewText.text;
            viewText.text = "";
            StartCoroutine(WriteText());
        }
    }

    IEnumerator WriteText()
    {
        isTyping = true;
        foreach(char c in transferText)
        {
            viewText.text += c;
            yield return new WaitForSeconds(0.03f);
        }
        isTyping = false;
    }
}
