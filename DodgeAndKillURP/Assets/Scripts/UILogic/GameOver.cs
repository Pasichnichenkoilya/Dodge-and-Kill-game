using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject UI;

    private void Start()
    {
        GameManager.Instance.OnPlayerDie += OnPlayerDieHandler;
    }

    private void OnPlayerDieHandler(object sender, System.EventArgs e)
    {
        UI.SetActive(true);
    }

    public void RetryClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
