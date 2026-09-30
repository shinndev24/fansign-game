using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TMPro.TMP_Text))]
public class WriteText : MonoBehaviour
{
    public static TMPro.TMP_Text viewText;
    public static bool printText;
    public static int count;

    [SerializeField] float textSpeed = 0.03f;
    [SerializeField] string transferText;
    [SerializeField] int internalCount;

    void Update()
    {
        internalCount = count;
        count = GetComponent<TMPro.TMP_Text>().text.Length;
        if(printText == true)
        {
            printText = false;
            viewText = GetComponent<TMPro.TMP_Text>();
            transferText = viewText.text;
            viewText.text = "";
            StartCoroutine(RollText());
        }
    }

    IEnumerator RollText()
    {
        foreach(char c in transferText)
        {
            viewText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

}
