using System.Collections;
using UnityEngine;

public class CrystalGenerator : MonoBehaviour
{
    [SerializeField] private Crystal _crystal;
    [SerializeField] private BoxCollider _ground;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private float _upPosition = -0.3f;

    private bool _isBusy;

    private Vector3 _spawnPosition;
    private Coroutine _coroutine;
    private Collider[] _colliders;

    private void OnEnable()
    {
        Stopgenerate();

        _coroutine = StartCoroutine(Generate());
    }

    private void OnDisable()
    {
        Stopgenerate();
    }

    private void Stopgenerate()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Generate()
    {
        WaitForSeconds wait = new(Random.Range(1,4));

        while(enabled)
        {
            yield return wait;

            _spawnPosition = GetSpawnPosition();
            _isBusy = CheckSpawnPoint();

            if (_isBusy == false)
            {
                Crystal crystal = Instantiate(_crystal, _spawnPosition, _crystal.transform.rotation);
                crystal.transform.parent = transform;
            }
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float positionX = Random.Range(0, _ground.bounds.extents.x);
        float positionZ = Random.Range(0, _ground.bounds.extents.z);

        float spawnPositionX = Random.Range(_ground.transform.position.x - positionX, _ground.transform.position.x + positionX);
        float spawnPositionZ = Random.Range(_ground.transform.position.z - positionZ, _ground.transform.position.z + positionZ);

        return new(spawnPositionX, _upPosition, spawnPositionZ);
    }

    private bool CheckSpawnPoint()
    {
        _colliders = Physics.OverlapBox(_spawnPosition, Vector3.one, transform.rotation, _layerMask);

        return _colliders.Length > 0;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_spawnPosition, Vector3.one);
    }
}
