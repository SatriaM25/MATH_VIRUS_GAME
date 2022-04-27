using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public Text questionUI;
    public Text hiddenAnswer;
    [SerializeField] private GameObject calculator;
    [SerializeField] private Image shape;
    [SerializeField] private Sprite[] shapeChoice;
    private int answer;

    // Geometry Component
    int side, height;
    int result;
    public void ActivateQuestion(){
        calculator.SetActive(true);
    }
    public void CreateQuestion()
    {
        RandomAnswer();
        hiddenAnswer.text = "" + answer;
    }
    void RandomAnswer()
    {
        int choice = Random.Range(0,6);
        int randSide = Random.Range(1,21);
        int randHeight = Random.Range(1,21);
        switch(choice){
            case 0: // Rectangle Circumference
                int recSide = randSide;
                int recHeight = randHeight;
                questionUI.text = "length = " + recSide + "  Width = " + recHeight + "\nCircumference?";
                shape.sprite = shapeChoice[0];
                answer = recSide * 2 + recHeight * 2;
                break;
            case 1: // Rectangle Area
                recSide = randSide;
                recHeight = randHeight;
                questionUI.text = "length = " + recSide + "  Width = " + recHeight + "\nArea?";
                shape.sprite = shapeChoice[0];
                answer = recSide * recHeight;
                break;
            case 2: // Triangle Circumference
                int triSide = randSide;
                questionUI.text = "Side = " + triSide + "\nCircumference?";
                shape.sprite = shapeChoice[1];
                answer = triSide * 3;
                break;
            case 3: // Triangle Area
                int triSide2 = RandomEven();
                int triHeight2 = RandomEven();
                questionUI.text = "Side = " + triSide2 + " Height = " + triHeight2 + "\nArea?";
                shape.sprite = shapeChoice[1];
                answer = triSide2 * triHeight2 / 2;
                break;
            case 4: // Circle Area
                float cirRad = RandomSeven();
                questionUI.text = "Radius = " + (int)cirRad + "\nArea?";
                shape.sprite = shapeChoice[2];
                answer = (int) Mathf.Pow(cirRad,2) * 22 / 7;
                break;
            case 5: // Circle Circumference
                cirRad = RandomSeven();
                questionUI.text = "Radius = " + (int)cirRad + "\nCircumference?";
                shape.sprite = shapeChoice[2];
                answer =  2 * (int) cirRad * 22 / 7;
                break;
        }
    }

    int RandomEven()
    {
        int temp = Random.Range(3,21);
        if(temp % 2 != 0)
        {
            temp -= 1;
        }
        return temp;
    }
    float RandomSeven()
    {
        int[] nums = {7,14,21};
        int rand = Random.Range(0,nums.Length);
        return nums[rand];
    }

}
