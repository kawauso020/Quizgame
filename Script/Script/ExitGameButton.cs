using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
   
    // このメソッドをボタンの OnClick に登録してください
    public void ExitGame()
    {
        // エディタ上では止める、ビルド版では終了する
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

