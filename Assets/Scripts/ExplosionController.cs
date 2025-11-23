using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _maxRayDistance = 100f;
    [SerializeField] private float _explotionRadius = 100f;
    [SerializeField] private float _explotionForce = 500f;
    [SerializeField] private ParticleSystem _explotionPrefab;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CreateExplotionunderMouse();
        }
    }

    private void CreateExplotionunderMouse()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxRayDistance))
        {
            Vector3 explotionPosition = hitInfo.point;

            SpawnEffect(explotionPosition);
            
            PushExplodables(explotionPosition);
        }
    }

    private void SpawnEffect(Vector3 position)
    {
        if (_explotionPrefab == null)
        {
            return;
        }

        ParticleSystem effect = Instantiate(_explotionPrefab, position, Quaternion.identity);

        effect.Play();
    }

    private void PushExplodables(Vector3 explotionPosition)
    {
        Collider[] colliders = Physics.OverlapSphere(explotionPosition, _explotionRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            IExplodable explodable = colliders[i].GetComponent<IExplodable>();

            if (explodable == null)
            {
                continue;
            }
            
            explodable.ApplyExplosion(explotionPosition, _explotionForce, _explotionRadius);
        }
    }
}
    