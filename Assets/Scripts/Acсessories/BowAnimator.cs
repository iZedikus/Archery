using UnityEngine;

public class BowAnimator : MonoBehaviour
{
    [SerializeField]GameObject left, right, strin;
    float angle;

    void Update()
    {
        switch(GetComponent<Bow>().stage)
        {
            case "���������":
            angle = FingerPosition.distance * .001f;
            break;
            
            case "�������":
            angle = FingerPosition.distance * .025f;
            break;

            case "�������":
            angle = FingerPosition.distance * .025f;
            break;

            case "�������":
            angle = Mathf.Abs(FingerPosition.dif1.x) + Mathf.Abs(FingerPosition.dif2.x);
            break;

            case "�������":
            angle = 0;
            break;

            default:
            angle = FingerPosition.distance * .01f;
            break;
        }
        left.transform.localRotation = Quaternion.Euler(-90, Mathf.Lerp(right.transform.localRotation.y, -angle, .5f), 0);
        right.transform.localRotation = Quaternion.Euler(-90, Mathf.Lerp(right.transform.localRotation.y, angle, .5f), 0);
        strin.transform.localPosition = new Vector3(0, 0, -1.78f - angle * .4f);
    }
}
