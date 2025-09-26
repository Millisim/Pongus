using System;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null) {
                GameObject obj = new GameObject("GameManager");
                _instance = obj.AddComponent<GameManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
        
    }

    private int scoreOfPlayer1, scoreOfPlayer2;
    public event Action<int, int> OnScore;  // notify when scores change
    public event Action<int> OnWin;         // notify when someone wins

    [SerializeField] private int winScore = 4;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    public void SetScores(string zoneTag)
    {
        if (zoneTag == "L-Zone")
            scoreOfPlayer2++;
        else if (zoneTag == "R-Zone")
            scoreOfPlayer1++;

        // Notify subscribers that score changed (Observer pattern)
        OnScore?.Invoke(scoreOfPlayer1, scoreOfPlayer2);

        // Check if someone won
        int winnerId = scoreOfPlayer1 == winScore ? 1 : scoreOfPlayer2 == winScore ? 2 : 0;
        if (winnerId != 0)
        {
            OnWin?.Invoke(winnerId); // Notify subscribers of win
        }
    }
    public void ResetScores()
    {
        scoreOfPlayer1 = 0;
        scoreOfPlayer2 = 0;

        // Notify subscribers so the UI resets
        OnScore?.Invoke(scoreOfPlayer1, scoreOfPlayer2);
    }

}
