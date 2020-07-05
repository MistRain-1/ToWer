using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharaClick : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject CheCanvas;
    public GameObject FollowUI;
    public GameObject FollowButton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 canvaPos = Camera.main.WorldToScreenPoint(this.transform.position);
        FollowUI.transform.position = canvaPos;
        Vector3 buttonPos = Camera.main.WorldToScreenPoint(this.transform.position);
        FollowButton.transform.position = buttonPos;
    }
    public void MouseDown()
    {
        CheCanvas.SetActive(true);
        Time.timeScale = 0.2f;
    }
    public void No()
    {
        CheCanvas.SetActive(false);
        Time.timeScale = 1;
    }
    public void Yes()
    {
        Time.timeScale = 1;
        GameManage.instance.Allcost += transform.GetComponentInParent<TestCharacter>().cost;
        GameManage.instance.charaRange++;
        Destroy(transform.parent.gameObject);
    }
}
