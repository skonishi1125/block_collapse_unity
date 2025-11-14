using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIController : MonoBehaviour
{
    // ゲーム開始ボタンに充てるメソッド
    public void OnClickStartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
