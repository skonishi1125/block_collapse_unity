using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player Player { get; private set; }
    public bool isGameStarted {  get; private set; }
    public bool isGameClear {  get; private set; }
    public bool isGameOver { get; private set; }

    private void Awake()
    {
        // シングルトンパターンで設計
        // 他に同じInstanceが存在していたら、消すという設計（1個だけ残す）
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);// シーンをまたいで残したい場合は有効化
    }

    // 登録はPlayer側のAwake()など、呼び出し側でやってもらう
    public void RegisterPlayer(Player player) => Player = player;

    public void GameStart() => isGameStarted = true;
    public void GameClear() => isGameClear = true;
    public void GameOver() => isGameOver = true;


}
