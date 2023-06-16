using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    [SerializeField] bool normalize;
    void Start()
    {
        Transform _transform = transform;
        var pos = _transform.position;
        if (Physics.Raycast(pos, Vector3.down, out RaycastHit hit) && hit.collider.name == "Terrain")
        {
            _transform.position = hit.point;
            if (normalize) _transform.up = hit.normal;
            else _transform.forward = _transform.position - GetComponentInParent<TerrainBuilder>().playerPosition;
        }
        else Destroy(gameObject);
    }
}
