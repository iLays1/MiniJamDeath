using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    public CinemachineVirtualCamera[] cams;
    public bool inputsEnabled = true;

    int currentCam = 0;

    private void Awake()
    {
        instance = this;
    }

    public void Update()
    {
        if (!inputsEnabled) return;
        Vector2 scrollInput = Input.mouseScrollDelta;
        
        if (Input.GetKeyDown(KeyCode.W) || scrollInput.y > 0)
            ChangeCam(currentCam + 1);
        if (Input.GetKeyDown(KeyCode.S) || scrollInput.y < 0)
            ChangeCam(currentCam - 1);
    }

    public void ChangeCam(int index)
    {
        if (index < 0) return;
        if (index >= cams.Length) return;

        currentCam = index;

        foreach(var c in cams)
        {
            c.gameObject.SetActive(false);
        }
        cams[currentCam].gameObject.SetActive(true);
    }
}
