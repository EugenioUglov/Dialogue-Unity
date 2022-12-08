using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueYesNo : Dialogue
{
    [SerializeField] protected GameObject _buttonYes;
    [SerializeField] protected GameObject _buttonNo;
    
    private DialogueStruct _currentDialogueStruct;
    private List<DialogueStruct> _dialogues = new List<DialogueStruct>();
    
    
    public bool ShowNextDialogue()
    {
        _contentTextMeshPro.text = "";
        
        _i_currentDialogueStruct++;
        
        _buttonYes.SetActive(false);
        _buttonNo.SetActive(false);

        if (_i_currentDialogueStruct >= _dialogues.Count - 1 || _dialogues[_i_currentDialogueStruct].GetName() == null)
        {
            return false;
        }
        
        _currentDialogueStruct = _dialogues[_i_currentDialogueStruct];

        StartCoroutine(TypeTextCoroutine(
            _currentDialogueStruct.GetContent(), 
            _contentTextMeshPro, 
            () =>
            {
                _buttonYes.SetActive(true);
                _buttonNo.SetActive(true);
            })
        );

        return true;
    }
    
    public void AddDialogue(string name, string content, Action onClickButtonYes = null, Action onClickButtonNo = null)
    {
        DialogueStruct newDialogue = new DialogueStruct(name.ToString(), content, onClickButtonYes, onClickButtonNo);

        _dialogues.Add(newDialogue);
    }
    
    public void OnClickButtonYes()
    {
        OnAnswer(isAnswerYes: true);
    }
    
    public void OnClickButtonNo()
    {
        OnAnswer(isAnswerYes: false);
    }

    public new void ShuffleDialogues()
    {
        System.Random rnd = new System.Random();
        DialogueStruct[] shuffledDialogues = _dialogues.OrderBy(x => rnd.Next()).ToArray();

        _dialogues = shuffledDialogues.ToList();
    }

    private void OnAnswer(bool isAnswerYes)
    {
        if (isAnswerYes)
        {
            _currentDialogueStruct.OnClickButtonYes();
        }
        else
        {
            _currentDialogueStruct.OnClickButtonNo();
        }
    }
    
    private struct DialogueStruct
    {
        private string _name;
        [TextArea(3, 10)]
        private string _content;
        private Action _onClickButtonYes;
        private Action _onClickButtonNo;

        
        public string GetName() => _name;
        public string GetContent() => _content;
        public Action OnClickButtonYes => _onClickButtonYes;
        public Action OnClickButtonNo => _onClickButtonNo;
        
        public DialogueStruct(string name, string content, Action onClickButtonYes = null, Action onClickButtonNo = null)
        {
            _name = name;
            _content = content;
            _onClickButtonYes = onClickButtonYes;
            _onClickButtonNo = onClickButtonNo;
        }
    }
}
