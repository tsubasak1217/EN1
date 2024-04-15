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
            // �v���C���[�̂�����W���擾
            playerIndex = GetPlayerIndex(map);

            // �v���C���[�̐i�ސ悪�z��͈͓̔���
            if (playerIndex + 1 <= map.Length)
            {
                // �i�ސ�ɔ������邩
                if (map[playerIndex + 1] == 2)
                {
                    // ���̂ЂƂ悪�󂢂Ă��邩
                    if (playerIndex + 2 < map.Length)
                    {
                        MoveNum(map[playerIndex + 1], map[playerIndex + 2]);
                        MoveNum(map[playerIndex], map[playerIndex + 1]);
                    }
                }
                else// �����Ȃ��Ƃ�
                {
                    MoveNum(map[playerIndex], map[playerIndex + 1]);
                }
            }

            OutputLog();

        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // �v���C���[�̂�����W���擾
            playerIndex = GetPlayerIndex(map);

            // �v���C���[�̐i�ސ悪�z��͈͓̔���
            if (playerIndex - 1 >= 0)
            {
                // �i�ސ�ɔ������邩
                if (map[playerIndex - 1] == 2)
                {
                    // ���̂ЂƂ悪�󂢂Ă��邩
                    if (playerIndex - 2 >= 0)
                    {
                        MoveNum(map[playerIndex - 1], map[playerIndex - 2]);
                        MoveNum(map[playerIndex], map[playerIndex - 1]);
                    }
                }
                else // �����Ȃ��Ƃ�
                {
                    MoveNum(map[playerIndex], map[playerIndex - 1]);
                }
            }

            // �X�V�������ʂ��o��
            OutputLog();
        }

        // �X�V�������ʂ��o�͂���֐�
        void OutputLog()
        {
            for (int i = 0; i < map.Length; i++)
            {
                debugText += map[i].ToString();
            }

            Debug.Log(debugText);
        }

        // ���l���ړ�����֐�
        void MoveNum(int startIdx,int goalIdx)
        {
            goalIdx = startIdx;
            startIdx = 0;
        }
    }

    // �v���C���[�̂�����W���擾����֐�
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
