using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerBody;

    public static float Modifiers;

    [HideInInspector]
    public float Sensivity;
    float verticalRotation;

    private void Start()
    {
        Modifiers = 1;
        Sensivity = 5;
    }

    private void Update()
    {
        Vector2 delta = FingerPosition.delta;
        verticalRotation -= delta.y * Time.deltaTime * Modifiers * Sensivity; verticalRotation = Mathf.Clamp(verticalRotation, -89, 89);
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, -delta.x * Modifiers * 0.25f);
        PlayerBody.transform.Rotate(Vector3.up * delta.x * Time.deltaTime * Modifiers * Sensivity);
        Modifiers = 1;
    }
}
