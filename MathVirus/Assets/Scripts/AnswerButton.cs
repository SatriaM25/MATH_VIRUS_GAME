using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerButton : MonoBehaviour
{
    public GameObject questionBox;
    
    public void ActivateQuestion(){
        questionBox.SetActive(true);
    }
}
