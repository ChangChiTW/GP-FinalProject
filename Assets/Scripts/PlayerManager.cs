using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private int _balance;
    private List<Item> _package;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        _balance = 1000;
    }

    public void AddBalance(int amount)
    {
        _balance += amount;
    }

    public int GetBalance()
    {
        return _balance;
    }

    public void AddItem(Item item)
    {
        _package.Add(item);
    }

    public void RemoveItem(Item item)
    {
        _package.Remove(item);
    }

    public List<Item> GetPackage()
    {
        return _package;
    }
}
