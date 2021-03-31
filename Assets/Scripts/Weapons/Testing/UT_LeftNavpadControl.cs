using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Evacuation.UserInterface.Touch;

public class UT_LeftNavpadControl : MonoBehaviour
{
    private ITouchLeftNavpadControl navInterface;
    public GameObject targetPositionItem;

    // Start is called before the first frame update
    void Start()
    {
        TouchNavpadControlUI navUI = GameObject.FindObjectOfType<TouchNavpadControlUI>();
        navInterface = navUI.GetComponent<ITouchLeftNavpadControl>();
        navInterface.RevealPad(targetPositionItem.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        navInterface.TransformNavStick(targetPositionItem.transform.position);
    }
}
