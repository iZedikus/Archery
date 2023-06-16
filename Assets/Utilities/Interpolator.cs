using UnityEngine;

public class Interpolator : MonoBehaviour
{
    public float Smoothness;
    public Transform Target;

    void LateUpdate()
    {
        
        transform.position = Vector3.Lerp(transform.position, Target.position, Smoothness * Time.deltaTime);
        
        Quaternion rotation = transform.rotation, targetRotation = Target.rotation;
        transform.eulerAngles = new Vector3(
            Mathf.LerpAngle(rotation.eulerAngles.x, targetRotation.eulerAngles.x, Smoothness * Time.deltaTime),
            Mathf.LerpAngle(rotation.eulerAngles.y, targetRotation.eulerAngles.y, Smoothness * Time.deltaTime),
            Mathf.LerpAngle(rotation.eulerAngles.z, targetRotation.eulerAngles.z, Smoothness * Time.deltaTime));
    }
}
