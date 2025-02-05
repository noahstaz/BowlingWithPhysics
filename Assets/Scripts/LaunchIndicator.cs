using UnityEngine;
using Unity.Cinemachine;

public class ArrowIndicator : MonoBehaviour
{
    [SerializeField] private CinemachineCamera freeLookCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
        transform.forward = freeLookCamera.transform.forward;
        transform.rotation = Quaternion.Euler(0,transform.rotation.eulerAngles.y,0);
    }
}
