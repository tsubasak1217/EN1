using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManagerScript : MonoBehaviour {

	int[,] map;
	public GameObject PlayerPrefab;
	GameObject[,] field;
	int blockSize;
	Vector2Int playerIndex = new Vector2Int(-1, -1);
	string debugText = "";

	// Start is called before the first frame update
	void Start() {

		map = new int[,] {
			{ 1, 0, 2, 2, 0, 0, 2, 0, 2, 2, 0, 0 },
			{ 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
			{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0 }
		};

		blockSize = 64;

		field = new GameObject[
			map.GetLength(0),
			map.GetLength(1)
			];

		for(int i = 0; i < map.GetLength(0); i++) {
			for(int j = 0; j < map.GetLength(1); j++) {
				if(map[i, j] == 1) {
					field[i, j] = Instantiate(
						PlayerPrefab,
						new Vector3(j, map.GetLength(0) - i, 0),
						Quaternion.identity
					);
				}
			}
		}
	}

	// Update is called once per frame
	void Update() {

		if(Input.GetKeyDown(KeyCode.RightArrow)) {
			// �v���C���[�̂�����W���擾
			playerIndex = GetPlayerIndex();

			//�ړ�
			MoveNum(playerIndex, playerIndex + new Vector2Int(1, 0));

			// �X�V�������ʂ��o��
			OutputLog();
		} else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			// �v���C���[�̂�����W���擾
			playerIndex = GetPlayerIndex();

			//�ړ�
			MoveNum(playerIndex, playerIndex + new Vector2Int(-1, 0));

			// �X�V�������ʂ��o��
			OutputLog();

		} else if(Input.GetKeyDown(KeyCode.UpArrow)) {
			// �v���C���[�̂�����W���擾
			playerIndex = GetPlayerIndex();

			//�ړ�
			MoveNum(playerIndex, playerIndex + new Vector2Int(0, -1));

			// �X�V�������ʂ��o��
			OutputLog();

		} else if(Input.GetKeyDown(KeyCode.DownArrow)) {
			// �v���C���[�̂�����W���擾
			playerIndex = GetPlayerIndex();

			//�ړ�
			MoveNum(playerIndex, playerIndex + new Vector2Int(0, 1));

			// �X�V�������ʂ��o��
			OutputLog();
		}


		/*field = new GameObject[
			map.GetLength(0),
			map.GetLength(1)
			];*/

		/*for(int i = 0; i < map.GetLength(0); i++) {
			for(int j = 0; j < map.GetLength(1); j++) {
				if(map[i, j] == 1) {
					field[i, j] = Instantiate(
					   PlayerPrefab,
						new Vector3(j, map.GetLength(0) - i, 0),
						Quaternion.identity
					);
				}
			}
		}*/
	}

	// �X�V�������ʂ��o�͂���֐�
	void OutputLog() {
		for(int i = 0; i < map.GetLength(0); i++) {
			debugText += "\n";

			for(int j = 0; j < map.GetLength(1); j++) {
				debugText += map[i, j].ToString();
			}
		}

		Debug.Log(debugText);
		debugText = "";
	}

	// ���l���ړ�����֐�
	bool MoveNum(Vector2Int startIdx, Vector2Int goalIdx) {

		if(goalIdx.x < 0 || goalIdx.x >= field.GetLength(1)) { return false; }
		if(goalIdx.y < 0 || goalIdx.y >= field.GetLength(0)) { return false; }

		Vector2Int direction = goalIdx - startIdx;
		Vector2Int tmp = direction;
		int count = 1;

		// �ړ��悪������Ȃ��Ȃ�܂Ń��[�v
		while(map[startIdx.y + tmp.y, startIdx.x + tmp.x] == 2) {
			tmp += direction;
			count++;

			// �͈͊O�ɏo���ꍇ�͈ړ��ł��Ȃ�
			if(startIdx.x + tmp.x < 0 || startIdx.x + tmp.x >= field.GetLength(1)) { return false; }
			if(startIdx.y + tmp.y < 0 || startIdx.y + tmp.y >= field.GetLength(0)) { return false; }
		}

		for(int i = count; i > 0; i--) {
			map[startIdx.y + tmp.y, startIdx.x + tmp.x]
				= map[startIdx.y + tmp.y - direction.y, startIdx.x + tmp.x - direction.x];//�}�b�v�ԍ��̈ړ�
			map[startIdx.y + tmp.y - direction.y, startIdx.x + tmp.x - direction.x] = 0;// �󂢂�����0�ɂ���

			field[startIdx.y + tmp.y, startIdx.x + tmp.x]
				= field[startIdx.y + tmp.y - direction.y, startIdx.x + tmp.x - direction.x];//�}�b�v�ԍ��̈ړ�
			field[startIdx.y + tmp.y - direction.y, startIdx.x + tmp.x - direction.x] = null;// �󂢂�����0�ɂ���

			field[startIdx.y + tmp.y, startIdx.x + tmp.x].transform.position =
				new Vector3(startIdx.x + tmp.x, map.GetLength(0) - startIdx.y + tmp.y, 0);

			tmp -= direction;
		}

		return true;
	}

	// �v���C���[�̂�����W���擾����֐�
	Vector2Int GetPlayerIndex() {

		for(int i = 0; i < field.GetLength(0); i++) {
			for(int j = 0; j < field.GetLength(1); j++) {

				if(field[i, j] == null) { continue; }

				if(field[i, j].tag == "Player") {
					return new Vector2Int(j, i);
				}
			}
		}

		return new Vector2Int(-1, -1);
	}

}