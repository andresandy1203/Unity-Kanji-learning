using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Answer Data class
public class AnswerData : MonoBehaviour
{
    //Set up serializable fields
    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI infoTextObject = null;
    [SerializeField] Image toggle = null;

    [Header("Textures")]
    [SerializeField] Sprite uncheckedToggle = null;
    [SerializeField] Sprite checkedToggle = null;

    [Header("References")]
    [SerializeField] GameEvents events = null;

    private RectTransform _rect = null;
    public RectTransform Rect
    {
        get
        {
            if(_rect==null)
            {
                _rect = GetComponent<RectTransform>() ?? gameObject.AddComponent<RectTransform>();
            }
            return _rect;
        }
    }

    private int _answerIndex = -1;

    public int AnswerIndex { get { return _answerIndex; } }

    private bool Checked = false;

    //Uodate answer data
    public void UpdateData(string info, int index)
    {
        infoTextObject.text = info;
        _answerIndex = index;
    }
    public void Reset()
    {
        Checked= false;
        UpdateUI();
    }

    //Switch state when clicked
    public void SwitchState()
    {
        Checked = !Checked;
        UpdateUI();
        if(events.UpdateQuestionAnswer!= null)
        {
            events.UpdateQuestionAnswer(this);
        }

    }

    //odate UI depending of state
    void UpdateUI()
    {
        toggle.sprite = (Checked) ? checkedToggle : uncheckedToggle;
    }
}
