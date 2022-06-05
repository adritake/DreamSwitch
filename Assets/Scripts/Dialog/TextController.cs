using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class TextController : Singleton<TextController>
{
    [Header("Language")]
    public Language language;

    [Header("Objects")]
    public GameObject dialogBox;
    public TMPro.TMP_Text textBox;
    public DialogList dialogList;

    [Header("Text Parameters")]
    public float textWaitTime;
    public float dialogShowTime;

    public enum Language
    { 
        English,
        Spanish 
    }

    public void StartDialog(string id)
    {
        StartCoroutine(ShowDialog(id));
    }

    IEnumerator ShowDialog(string id)
    {
        textBox.text = "";
        Dialog dialog = dialogList.list.First(x => x.id == id);
        string text = "";

        if(language == Language.English)
        {
            text = dialog.englishText;
        }
        if(language == Language.Spanish)
        {
            text = dialog.spanishText;
        }

        dialogBox.SetActive(true);
        
        textBox.text = "";
        for(int i = 0; i < text.Length; i++)
        {
            textBox.text += text[i];
            yield return new WaitForSeconds(textWaitTime);
        }
        
        yield return new WaitForSeconds(dialogShowTime);

        
        dialogBox.SetActive(false);

    }
}
