using UnityEngine;
using System.Collections;

public class Scene01Events : MonoBehaviour
{
    private string textToSpeak;
    private int currentTextLength;
    private int textLength;
    private GameObject currentSpeaker;

    public GameObject fadeScreenIn;
    public GameObject character1;
    public GameObject character2;
    public GameObject chiyori;

    [SerializeField] GameObject textBox;
    [SerializeField] GameObject mainText;
    [SerializeField] GameObject clickToNext;
    [SerializeField] GameObject talkingName;
    [SerializeField] Color talk_normal;
    [SerializeField] Color talk_thought;

    [SerializeField] string[] lines;
    [SerializeField] GameObject[] characterStarts;
    [SerializeField] int currentLine;

    void Update()
    {
        textLength = TextWriter.charCount;
        if (Input.GetMouseButtonDown(0))
        {
            NextText();
        }
    }

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(0.5f);
        fadeScreenIn.SetActive(false);

        StartCoroutine(ShowLine(currentLine));
    }

    IEnumerator ShowLine(int index)
    {
        mainText.SetActive(true);
        textToSpeak = lines[index];
        TMPro.TMP_Text textComponent = textBox.GetComponent<TMPro.TMP_Text>();
        bool speakerChanged = false;

        if (characterStarts != null && index < characterStarts.Length && characterStarts[index] != null)
        {
            for (int characterIndex = 0; characterIndex < characterStarts.Length; characterIndex++)
            {
                if (characterStarts[characterIndex] != null)
                {
                    characterStarts[characterIndex].SetActive(false);
                }
            }

            currentSpeaker = characterStarts[index];
            currentSpeaker.SetActive(true);
            speakerChanged = true;

            Transform parent = currentSpeaker.transform.parent;
            while (parent != null)
            {
                parent.gameObject.SetActive(true);
                parent = parent.parent;
            }
        }

        bool isChiyoriSpeaking = currentSpeaker == chiyori;

        if (!isChiyoriSpeaking && currentSpeaker != null && chiyori != null)
        {
            isChiyoriSpeaking = currentSpeaker.transform.IsChildOf(chiyori.transform);
        }

        if (isChiyoriSpeaking)
        {
            textComponent.color = talk_thought;

            if (speakerChanged)
            {
                ShowChiyoriName();
            }
        }
        else
        {
            textComponent.color = talk_normal;

            if (speakerChanged && talkingName != null)
            {
                talkingName.SetActive(false);
            }
        }

        textComponent.text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextWriter.runTextPrint = true;
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
    }

    void ShowChiyoriName()
    {
        if (talkingName == null) return;

        talkingName.SetActive(false);
        talkingName.SetActive(true);

        Animator nameAnimator = talkingName.GetComponent<Animator>();
        if (nameAnimator != null)
        {
            nameAnimator.Play("NameBoxIn", 0, 0f);
        }
    }

    public void NextText()
    {
        if (TextWriter.isTyping) return;

        currentLine++;

        if (currentLine < lines.Length)
        {
            StartCoroutine(ShowLine(currentLine));
        }
        else
        {
            mainText.SetActive(false);
            talkingName.SetActive(false);
        }
    }
}
