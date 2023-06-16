using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector]
    public float damage;

    public void Shoot()
    {
        GetComponent<Collider>().enabled = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, transform.rotation.y, 1000f));
        Destroy(gameObject, 1f);
    }

    public void Disappear(Transform quiver)
    {
        GetComponent<Interpolator>().Smoothness = 10f;
        GetComponent<Interpolator>().Target = quiver;
        Destroy(gameObject, .5f);
    }
}
