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

}
