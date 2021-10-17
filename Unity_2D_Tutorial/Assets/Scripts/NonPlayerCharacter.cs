using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public float DisplayTime = 4.0f;
    public GameObject Dialogbox;
    float Timerdisplay;

    void Start()
    {
        Dialogbox.SetActive(false);
        Timerdisplay = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Timerdisplay >= 0)
        {
            Timerdisplay -= Time.deltaTime;
            if (Timerdisplay < 0)
            {
                Dialogbox.SetActive(false);
            }
        }
    }
    public void DisplayDialogbox()
    {
        Dialogbox.SetActive(true);
        Timerdisplay = DisplayTime;
    }
}
