using System;
using UnityEditorInternal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    // Selected gamemode for vs Player or vs CPU
    public PlayMode playMode;

    private int scoreOfPlayer1, scoreOfPlayer2; // current scores for each player
    public event Action<int, int> OnScore;  // notify when scores change
    public event Action<int> OnWin;         // notify when someone wins
    public int winScore = 8;                    // threshold for victory

    public enum PlayMode
    {
        PlayerVsPlayer,
        PlayerVsCPU,
        CPUVsCPU
    }

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

        int winnerId = 0;
        if (scoreOfPlayer1 == winScore) winnerId = 1;
        if (scoreOfPlayer2 == winScore) winnerId = 2;

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
