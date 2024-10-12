using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour
{
    [SerializeField] private int remainingClicks = 10;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private GameObject remainTextPanel;
    [SerializeField] private TextMeshProUGUI remainText;


    private List<GameObject> frogs = new List<GameObject>();

    private void OnEnable()
    {
        FrogEvents.OnFrogSpawned += RegisterFrog;
        FrogEvents.OnFrogDestroyed += UnregisterFrog;
    }

    private void OnDisable()
    {
        FrogEvents.OnFrogSpawned -= RegisterFrog; 
        FrogEvents.OnFrogDestroyed -= UnregisterFrog; 
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);
        winnerPanel.SetActive(false);
        remainTextPanel.SetActive(true);
        remainText.text = remainingClicks.ToString();
    }

    private void RegisterFrog(GameObject frog)
    {
        frogs.Add(frog);
    }

    private void UnregisterFrog(GameObject frog)
    {
        frogs.Remove(frog); 

        if (frogs.Count <= 0)
        {
            Winner();  
        }
    }

    public int GetRemainingClicks()
    {
        return remainingClicks;
    }

    public void DecreaseClickCount()
    {
        remainingClicks--;
        remainText.text = remainingClicks.ToString();
        Debug.Log("Kalan tıklama hakkı: " + remainingClicks);

        if (remainingClicks <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        remainTextPanel.SetActive(false);
    }

    public void Winner()
    {
        winnerPanel.SetActive(true);
        remainTextPanel.SetActive(false);
    }

    public void RestartBtn()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeScene);
    }

}
