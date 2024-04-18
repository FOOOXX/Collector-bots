using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private Vector3 _halfExtens;

    private readonly Queue<Crystal> _foundedCrystals = new();
    private readonly List<Crystal> _takenCrystals = new();

    private Collider[] _result;
    private Base _base;

    public int AmountFoundedCrystal => _foundedCrystals.Count;

    private void Awake()
    {
        _base = GetComponent<Base>();
    }

    public Crystal GetDirection()
    {
        Crystal crystal = _foundedCrystals.Dequeue();

        while (crystal == null)
            crystal = _foundedCrystals.Dequeue();

        _takenCrystals.Add(crystal);

        return crystal;
    }

    public void Scan()
    {
        _result = Physics.OverlapBox(transform.position, _halfExtens, Quaternion.identity, LayerMask.GetMask("Crystal"));
        
        foreach (Collider collider in _result)
        {
            if(collider.TryGetComponent(out Crystal crystal) && _foundedCrystals.Contains(crystal) == false 
                                                             && _takenCrystals.Contains(crystal) == false
                                                             && _base.IsCrystalBusy(crystal) == false)
            {
                _foundedCrystals.Enqueue(crystal);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, _halfExtens);
        Gizmos.color = Color.white;
    }
}
