using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for facilitation of new project files in unity project directory
[CreateAssetMenu(menuName = "Dialogue/New Dialogue Container")]
public class DialogueText : ScriptableObject
{
    public string speakerName;

    [TextArea(5, 10)]
    public string[] paragraphs;
}
