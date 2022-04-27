using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleScript : MonoBehaviour
{
    [SerializeField] private RectTransform slidingArea;
    [SerializeField] private Scrollbar scrollbarUI;
    
    // Start is called before the first frame update
    void Start()
    {
        scrollbarUI.value = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void aiueo()
    {
        Debug.Log(scrollbarUI.value);
    }

    void calculateSlidingPos()
    {
        // posisi baru = posisi lama + perubahan posisi * persentasi handler
        // float currentPos = slidingArea.offsetMin.x;
        float maxPos = 633f;
        float newPos = (1 - scrollbarUI.value) * maxPos;
        slidingArea.offsetMax = new Vector2(slidingArea.offsetMax.x, newPos);
        slidingArea.offsetMin = new Vector2(slidingArea.offsetMin.x, newPos);
    }
    //  void SetPos(float move)
    //  {
    //      // set top position
    //      rt.offsetMax = new Vector2(rt.offsetMax.x, -move);
    //      // set bottom position
    //      rt.offsetMin = new Vector2(rt.offsetMin.x, move);
    //  }


}
