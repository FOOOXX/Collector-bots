using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Scanner))]
public class Base : MonoBehaviour
{
    [SerializeField] private Transform _stock;

    private readonly Queue<Bot> _bots = new();
    private Scanner _scanner;

    private void Awake()
    {
        _scanner = GetComponent<Scanner>();

        for (int i = 0; i < transform.childCount; i++)
        {
            _bots.Enqueue(transform.GetChild(i).GetComponent<Bot>());
        }
    }

    private void FixedUpdate()
    {
        _scanner.Scan();

        if (_bots.Count > 0 && _scanner.AmountFoundedCrystal != 0)
        {
            GiveTask();
        }
    }

    public Vector3 GiveStockPosition()
    {
        return _stock.position;
    }

    public void Park(Bot bot)
    {
        if (bot == null)
        {
            throw new ArgumentNullException(nameof(bot));
        }

        bot.Park();
        _bots.Enqueue(bot);
    }

    public bool IsCrystalBusy(Crystal crystal)
    {
        if (crystal == null)
        {
            throw new ArgumentNullException(nameof(crystal));
        }

        foreach (Bot bot in _bots)
        {
            if (bot.GetComponentInChildren<Crystal>() == crystal)
                return true;
        }

        return false;
    }

    private void GiveTask()
    {
        _bots.Dequeue().SetWayToCrystal(_scanner.GetDirection());
    }
}
