using System;
using UnityEngine;

[RequireComponent(typeof(BotMovement))]
[RequireComponent (typeof(Collector))]
public class Bot : MonoBehaviour
{
    private BotMovement _movement;
    private Collector _collector;
    private Base _base;

    private void Awake()
    {
        _movement = GetComponent<BotMovement>();
        _collector = GetComponent<Collector>();
        _base = GetComponentInParent<Base>();
    }

    private void OnEnable()
    {
        _collector.CrystalCollected += SetWayToStock;
    }

    private void OnDisable()
    {
        _collector.CrystalCollected -= SetWayToStock;
    }

    public void Park()
    {
        _movement.SetTarget(_movement.StartPosition);
    }

    public void SetWayToCrystal(Crystal crystal)
    {
        if (crystal == null)
        {
            throw new ArgumentNullException(nameof(crystal));
        }

        _movement.SetTarget(crystal.transform.position);
        _collector.AttachCrystalToBot(crystal);
    }

    private void SetWayToStock()
    {
        _movement.SetTarget(_base.GiveStockPosition());
    }
}
