using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAnswer : MonoBehaviour
{
    // Answer Question
    [SerializeField] private Button[] numButton;
    [SerializeField] private Text HiddenUI;
    [SerializeField] private Text AnswerUI;
    private bool correct = false;

    void Start()
    {
        
        numButton[0].onClick.AddListener(delegate{ButtonValue(0);});
        numButton[1].onClick.AddListener(delegate{ButtonValue(1);});
        numButton[2].onClick.AddListener(delegate{ButtonValue(2);});
        numButton[3].onClick.AddListener(delegate{ButtonValue(3);});
        numButton[4].onClick.AddListener(delegate{ButtonValue(4);});
        numButton[5].onClick.AddListener(delegate{ButtonValue(5);});
        numButton[6].onClick.AddListener(delegate{ButtonValue(6);});
        numButton[7].onClick.AddListener(delegate{ButtonValue(7);});
        numButton[8].onClick.AddListener(delegate{ButtonValue(8);});
        numButton[9].onClick.AddListener(delegate{ButtonValue(9);});
    }
    private void ButtonValue(int index)
    {
        AnswerUI.text += "" + index;
    }
    public void CheckInput()
    {
        if(AnswerUI.text == HiddenUI.text)
        {
            // Debug.Log("Enemy Dead");
            correct = true;
        }
    }

    public bool isCorrect()
    {
        if(correct)
        {
            correct = false;
            AnswerUI.text = "";
            ExitCalculator();
            return true;
        } else return false;
    }

    public void Backspace()
    {
        string value = AnswerUI.text;
        string temp = value.Length > 0 ? value.Substring(0, value.Length - 1) : value;
        AnswerUI.text = temp;
    }
    public void ExitCalculator()
    {
        gameObject.SetActive(false);
    }

}
