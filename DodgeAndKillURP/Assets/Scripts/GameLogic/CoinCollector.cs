using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    int moneyParticle = 5;
    private void OnParticleCollision(GameObject other)
    {
        GameManager.Instance.Inventory.AddMoney(moneyParticle);
    }
}
