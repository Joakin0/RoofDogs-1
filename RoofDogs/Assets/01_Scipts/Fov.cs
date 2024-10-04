using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Fov : MonoBehaviour
{
    [SerializeField]private CinemachineFreeLook virtualCamera;
    public bool isRuning;
    public float fovEfect = 60;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRuning = true;
            RuningEffect();
        }
        else
        {
            isRuning = false;
            virtualCamera.m_Lens.FieldOfView = 40;
        }
    }

    void RuningEffect()
    {
        if (isRuning == true)
        {
            virtualCamera.m_Lens.FieldOfView = fovEfect;
        }
    }
}
