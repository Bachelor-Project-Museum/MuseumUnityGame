using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingspanelFunctions : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnCloseWindow()
    {
        gameObject.SetActive(false );
    }
}
