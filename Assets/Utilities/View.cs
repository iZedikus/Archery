using UnityEngine;

public class View : MonoBehaviour
{
    void Update()
    {
        GetComponent<Camera>().fieldOfView = 90 - Bow.DynamicFOV;
    }
}
