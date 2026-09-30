using UnityEngine;
using System.Collections;

public class Scene01Events : MonoBehaviour
{
    [SerializeField] GameObject fadeScreenIn;
    public GameObject character1;
    public GameObject character2;

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(1.5);
        fadeScreenIn.SetActive(false);
        character1.SetActive(true);
        yield return new WaitForSeconds(2);
        //text function
        yield return new WaitForSeconds(2);
        character2.SetActive(true);
    }
}
