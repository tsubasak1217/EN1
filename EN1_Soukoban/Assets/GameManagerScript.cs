using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{

    int[] map = { 0, 2, 0, 1, 0, 2, 0, 0, 0 };


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int playerIndex = -1;
        string debugText = "";

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // プレイヤーのいる座標を取得
            playerIndex = GetPlayerIndex(map);

            // プレイヤーの進む先が配列の範囲内か
            if (playerIndex + 1 <= map.Length)
            {
                // 進む先に箱があるか
                if (map[playerIndex + 1] == 2)
                {
                    // 箱のひとつ先が空いているか
                    if (playerIndex + 2 < map.Length)
                    {
                        MoveNum(map[playerIndex + 1], map[playerIndex + 2]);
                        MoveNum(map[playerIndex], map[playerIndex + 1]);
                    }
                }
                else// 箱がないとき
                {
                    MoveNum(map[playerIndex], map[playerIndex + 1]);
                }
            }

            OutputLog();

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // プレイヤーのいる座標を取得
            playerIndex = GetPlayerIndex(map);

            // プレイヤーの進む先が配列の範囲内か
            if (playerIndex - 1 >= 0)
            {
                // 進む先に箱があるか
                if (map[playerIndex - 1] == 2)
                {
                    // 箱のひとつ先が空いているか
                    if (playerIndex - 2 >= 0)
                    {
                        MoveNum(map[playerIndex - 1], map[playerIndex - 2]);
                        MoveNum(map[playerIndex], map[playerIndex - 1]);
                    }
                }
                else // 箱がないとき
                {
                    MoveNum(map[playerIndex], map[playerIndex - 1]);
                }
            }

            // 更新した結果を出力
            OutputLog();
        }

        // 更新した結果を出力する関数
        void OutputLog()
        {
            for (int i = 0; i < map.Length; i++)
            {
                debugText += map[i].ToString();
            }

            Debug.Log(debugText);
        }

        // 数値を移動する関数
        void MoveNum(int startIdx,int goalIdx)
        {
            goalIdx = startIdx;
            startIdx = 0;
        }
    }

    // プレイヤーのいる座標を取得する関数
    int GetPlayerIndex(int[] array)
    {
        int result = -1;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == 1)
            {
                result = i;
                break;
            }
        }

        return result;
    }

}
