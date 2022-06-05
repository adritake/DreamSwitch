using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogList", order = 1)]
public class DialogList : ScriptableObject
{
    public Dialog[] list;
}

[Serializable]
public class Dialog
{
    public string id;
    [TextArea(10, 100)]
    public string englishText;
    [TextArea(10, 100)]
    public string spanishText;
}