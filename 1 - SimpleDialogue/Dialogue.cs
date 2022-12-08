using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    // VIEW
    [SerializeField] protected TMP_Text _contentTextMeshPro;
    
    protected int _i_currentDialogueStruct = -1;
    protected DialogueStruct _currentDialogueStruct;
    protected List<DialogueStruct> _dialogues = new List<DialogueStruct>();
    

    public void AddDialogue(string name, string content)
    {
        DialogueStruct newDialogue = new DialogueStruct(name.ToString(), content);

        _dialogues.Add(newDialogue);
    }

    public bool ShowNextDialogue()
    {
        _contentTextMeshPro.text = "";
        
        _i_currentDialogueStruct++;

        if (_i_currentDialogueStruct >= _dialogues.Count - 1 || _dialogues[_i_currentDialogueStruct].GetName() == null)
        {
            return false;
        }
        
        _currentDialogueStruct = _dialogues[_i_currentDialogueStruct];

        StartCoroutine(TypeTextCoroutine(
            _currentDialogueStruct.GetContent(), 
            _contentTextMeshPro)
        );

        return true;
    }

    public void ShuffleDialogues()
    {
        System.Random rnd = new System.Random();
        DialogueStruct[] shuffledDialogues = _dialogues.OrderBy(x => rnd.Next()).ToArray();

        _dialogues = shuffledDialogues.ToList();
    }
    
    
    protected IEnumerator TypeTextCoroutine(string inputText, TMP_Text textMeshProToShowText, Action OnTextShowed = null)
    {
        textMeshProToShowText.text = "";

        foreach (char letter in inputText.ToCharArray())
        {
            textMeshProToShowText.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
        
        OnTextShowed?.Invoke();
    }

    protected struct DialogueStruct
    {
        private string _name;
        [TextArea(3, 10)]
        private string _content;
        
        public string GetName() => _name;
        public string GetContent() => _content;


        public DialogueStruct(string name, string content)
        {
            _name = name;
            _content = content;
        }
    }
}