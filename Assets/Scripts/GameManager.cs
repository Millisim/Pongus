using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int scoreOfPlayer1, scoreOfPlayer2;
    public event Action<int, int> OnScore;  // notify when scores change
    public event Action<int> OnWin;         // notify when someone wins

    [SerializeField] private int winScore = 4;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetScores(string zoneTag)
    {
        if (zoneTag == "L-Zone")
            scoreOfPlayer2++;
        else if (zoneTag == "R-Zone")
            scoreOfPlayer1++;

        // Notify listeners
        OnScore?.Invoke(scoreOfPlayer1, scoreOfPlayer2);

        // Check for win condition
        int winnerId = scoreOfPlayer1 == winScore ? 1 : scoreOfPlayer2 == winScore ? 2 : 0;
        if (winnerId != 0)
        {
            OnWin?.Invoke(winnerId);
        }
    }
}
