using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Player Player { get; private set; }
    public int remainBlocks { get; private set; }

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

    // Debug時など、一時的にSceneを動作させたい場合にGameManagerが存在しない問題を防ぐ
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void AutoCreate()
    {
        if (Instance == null)
        {
            var prefab = Resources.Load<GameManager>("GameManager");
            if (prefab != null)
                Instantiate(prefab);
        }
    }

    // 登録はPlayer側のAwake()など、呼び出し側でやってもらう
    public void RegisterPlayer(Player player) => Player = player;

    public void RegisterBlock()
    {
        remainBlocks++;
    }

    public void CollapseBlock()
    {
        remainBlocks--;
        if (remainBlocks <= 0)
        {
            Debug.Log("CLEAR!");
            GameClear();
        }
    }


    public void GameStart()
    {
        Debug.Log($"ブロックの数: {remainBlocks} コ");
        isGameStarted = true;
    } 


    public void GameClear() => isGameClear = true;

    public void GameOver() => isGameOver = true;


}
