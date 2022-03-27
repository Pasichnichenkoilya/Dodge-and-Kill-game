using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Dissolve : MonoBehaviour
{
    public float dissolveTime;
    public GameObject[] objToDisable;
    [SerializeField] ParticleSystem moneyParticles;
    [SerializeField] GameObject onDestroyVFX;
    [SerializeField] int moneyDropAmount = 10;
    [SerializeField] float dissolveGoal = 0.8f;

    float dissolveScale = -1;

    private void Start()
    {
        moneyParticles = GameManager.Instance.moneyDropParticles;
    }


    private void Update()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetFloat("_EffectScale", dissolveScale);
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_EdgeColor", gameObject.GetComponent<MeshRenderer>().material.color);

        if (dissolveScale >= dissolveGoal)
        {
            EndDisolve();
        }
    }

    private void EndDisolve()
    {
        var particles = Instantiate(onDestroyVFX, transform.position, transform.rotation);
        particles.GetComponent<VisualEffect>().SetVector4("ParticlesColor", gameObject.GetComponent<MeshRenderer>().material.GetColor("_Color"));
        particles.GetComponent<VisualEffect>().Play();

        if (moneyParticles != null)
        {
            var particles1 = Instantiate(moneyParticles, transform.position, transform.rotation);
            particles1.emission.SetBurst(0, new ParticleSystem.Burst(0, moneyDropAmount));
        }

        Destroy(gameObject);
    }

    public void StartDissolve()
    {
        foreach (var item in objToDisable)
        {
            item.SetActive(false);
        }
        StartCoroutine(GameManager.ChangeValueSmoothly(res => dissolveScale = res, 0, dissolveGoal, dissolveTime));
    }


}
