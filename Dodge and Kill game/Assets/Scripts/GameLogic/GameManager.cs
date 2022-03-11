using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public List<Pool> pools;
    public List<DamageMaterial> damageMaterialsList;

    public Dictionary<PooledObjectTag, ObjectPool<GameObject>> poolDictionary;
    public Dictionary<DamageType, Material> damageMaterialsDictionary;

    public static GameManager Instance { get => instance; private set => instance = value; }

    private void Awake()
    {
        if (instance != null) return;
        instance = this;

        InitPools();
        InitDamageMaterials();

    }

    void InitPools()
    {
        poolDictionary = new Dictionary<PooledObjectTag, ObjectPool<GameObject>>();

        foreach (var p in pools)
        {
            ObjectPool<GameObject> objectPool = new ObjectPool<GameObject>(() =>
            {
                return Instantiate(p.prefab);
            }, obj =>
            {
                obj.SetActive(true);
            }, obj =>
            {
                obj.SetActive(false);
            }, obj =>
            {
                Destroy(obj);
            }, false, p.defaultCapacity, p.maxSize);

            poolDictionary.Add(p.tag, objectPool);
        }
    }

    void InitDamageMaterials()
    {
        damageMaterialsDictionary = new Dictionary<DamageType, Material>();
        foreach (var item in damageMaterialsList)
        {
            damageMaterialsDictionary.Add(item.damageType, item.material);
        }
    }

    public static IEnumerator ChangeValueSmoothly(Action<float> value, float startValue, float endValue, float duration, bool unscaled = false)
    {
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            if (PauseMenu.IsGamePaused)
                yield return null;

            var x = Mathf.Lerp(startValue, endValue, elapsed / duration);
            value(x);

            if (!PauseMenu.IsGamePaused)
                elapsed += unscaled ? Time.unscaledDeltaTime : Time.deltaTime;

            yield return null;
        }
        value(endValue);
    }
}
