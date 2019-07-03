using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public DialougBox dialoug;
    public KeyCode InteractionKey = KeyCode.Space;
    public SoundEffectsManger Sfx;

    public Text placeHolder;
    public GameObject dialougBoxObject;

    public bool onDialoug = false;
    public bool started = false;

    private int index;

    private bool skip;


    void Update()
    {
        if (started&& Input.GetKeyDown(InteractionKey))
        {
            if (index < dialoug.texts.Length -1  )
            {
                if (skip)
                {
                    index++;
                    showOnCanvas();
                }
            
            }
            else
            {
                end();
                //OtherObjectStarted = false;
            }
        }
    }

    public void showOnCanvas()
    {
        StopCoroutine(typeText());

        //onDialoug = true;
        placeHolder.text = "";
        if (!onDialoug)
        {
            Sfx.play(index.ToString());
            StartCoroutine(typeText());


        }
    }

    IEnumerator typeText()
    {
        onDialoug = true;

        int i = 0;
        skip = false;
        while (i < dialoug.texts[index].Length)
        {
            skip = false;

            placeHolder.text += dialoug.texts[index][i++];
            if (i >= dialoug.texts[index].Length)
            {
           
            }
            yield return new WaitForSeconds(0.009f);
            skip = false;


        }
        yield return new WaitForSeconds(1f);

        skip = true;
        onDialoug = false;
        
    }

    public void begin()
    {
        StopCoroutine(typeText());

        index = 0;
        started = true;
        dialougBoxObject.SetActive(true);
        showOnCanvas();

    }

    public void end()
    {
        started = false;
        StopCoroutine(typeText());
        dialougBoxObject.SetActive(false);
    }
}
