using UnityEngine;

public class FingerPosition : MonoBehaviour
{
    public static float distance;
    public static Vector2 dif1, dif2, delta;
    public static int TouchCount;
    public static float k, edge;
    
    Vector2 t11, t21;
    float begindist, curdist;

    private void Awake()
    {
        k = 1000f / Screen.currentResolution.width;
        edge = k * 200;
    }

    private void Update()
    {
        TouchCount = Input.touchCount;
        if(TouchCount != 0)
        {
            Touch t1 = Input.GetTouch(0);
            
            if (t1.phase == TouchPhase.Began) t11 = t1.position;
            dif1 = (t1.position - t11) * k;
            delta = t1.deltaPosition * k;

            if (TouchCount == 2)
            {
                Touch t2 = Input.GetTouch(1);

                if (t2.phase == TouchPhase.Began)
                {
                    t21 = t2.position;
                    begindist = new Vector2(t1.position.x - t2.position.x, t1.position.y - t2.position.y).magnitude;
                }
                dif2 = (t2.position - t21) * k;
                delta += t2.deltaPosition * k;

                curdist = new Vector2(t1.position.x - t2.position.x, t1.position.y - t2.position.y).magnitude;
                distance = 2 + (curdist - begindist) * k;
                if (distance == 0) distance = 2;
            }
            else
            {
                dif2 = Vector2.zero;
                distance = 1;
            }
        }
        else 
        {
            dif1 = Vector2.zero;
            dif2 = Vector2.zero;
            delta = Vector2.zero;
            distance = 0;
        }
    }
}