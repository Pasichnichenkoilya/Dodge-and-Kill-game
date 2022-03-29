using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public List<Pool> pools;
    public List<DamageMaterial> damageMaterialsList;

    public Dictionary<PooledObjectTag, ObjectPool<GameObject>> poolDictionary;
    public Dictionary<DamageType, Material> damageMaterialsDictionary;

    public Inventory Inventory { get; private set; }

    public Player Player { get; private set; }

    public PauseManager PauseManager { get; private set; }

    public PerkManager PerkManager { get; private set; }
    public WeaponManager WeaponManager { get; private set; }

    public bool IsPaused => PauseManager.IsPaused;

    public Weapon ActiveWeapon => WeaponManager.activeWeapon;


    public static GameManager Instance { get => instance; private set => instance = value; }

    public delegate void EventHandler(object? sender, EventArgs e);

    public event EventHandler OnPlayerDie;

    public int difficulty;
    public ParticleSystem moneyDropParticles;

    bool playerIsDead = false;

    private void Update()
    {
        if (!playerIsDead && Player.GetComponent<Health>().IsDead)
        {
            playerIsDead = true;
            OnPlayerDie?.Invoke(null, null);
        }
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        LoadPlayerProgress(SaveSystem.LoadPlayerProgress());
    }

    public void LoadPlayerProgress(PlayerProgress playerProgress)
    {
        if (playerProgress != null)
        {
            PerkManager.SetActivePerk(playerProgress.perkName);
            Inventory.Money = playerProgress.money;
            WeaponManager.SetActiveWeapon(playerProgress.weaponName);
            WeaponManager.SetBullet(playerProgress.weaponName, playerProgress.bulletType);

            return;
        }

        PerkManager.SetActivePerk(0);
        Inventory.Money = 0;
        WeaponManager.SetActiveWeapon(WeaponManager.weapons[0].name);
        WeaponManager.SetBullet(WeaponManager.weapons[0].name, PooledObjectTag.DefaultBullet);
    }

    private void Awake()
    {
        if (instance != null) return;
        instance = this;

        InitPools();
        InitDamageMaterials();

        PauseManager = new PauseManager();
        Inventory = new Inventory();
        PerkManager = gameObject.GetComponent<PerkManager>();
        WeaponManager = gameObject.GetComponent<WeaponManager>();
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
            if (GameManager.Instance.PauseManager.IsPaused)
                yield return null;

            var x = Mathf.Lerp(startValue, endValue, elapsed / duration);
            value(x);

            if (!GameManager.Instance.PauseManager.IsPaused)
                elapsed += unscaled ? Time.unscaledDeltaTime : Time.deltaTime;

            yield return null;
        }
        value(endValue);
    }

    public static IEnumerator WaitAndAction(float waitTime, Action actionAfter, Action actionEveryTick = null)
    {
        float time = 0;
        while (time < waitTime)
        {
            if (!GameManager.Instance.PauseManager.IsPaused)
            {
                time += Time.deltaTime;
                actionEveryTick?.Invoke();
            }
            yield return null;
        }
        actionAfter();
    }
}