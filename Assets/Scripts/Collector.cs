using System;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public Action CrystalCollected;
    private Crystal _crystal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Crystal crystal) && _crystal == crystal)
        {
            crystal.transform.parent = transform;

            CrystalCollected?.Invoke();
        }
    }

    public void AttachCrystalToBot(Crystal crystal) => _crystal = crystal;
}
