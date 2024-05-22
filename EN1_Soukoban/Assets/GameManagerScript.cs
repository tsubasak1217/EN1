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
			// プレイヤーのいる座標を取得
			playerIndex = GetPlayerIndex();

			//移動
			MoveNum(playerIndex, playerIndex + new Vector2Int(1, 0));

			// 更新した結果を出力
			OutputLog();
		} else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
			// プレイヤーのいる座標を取得
			playerIndex = GetPlayerIndex();

			//移動
			MoveNum(playerIndex, playerIndex + new Vector2Int(-1, 0));

			// 更新した結果を出力
			OutputLog();

		} else if(Input.GetKeyDown(KeyCode.UpArrow)) {
			// プレイヤーのいる座標を取得
			playerIndex = GetPlayerIndex();

			//移動
			MoveNum(playerIndex, playerIndex + new Vector2Int(0, -1));

			// 更新した結果を出力
			OutputLog();

		} else if(Input.GetKeyDown(KeyCode.DownArrow)) {
			// プレイヤーのいる座標を取得
			playerIndex = GetPlayerIndex();

			//移動
			MoveNum(playerIndex, playerIndex + new Vector2Int(0, 1));

			// 更新した結果を出力
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

	// 更新した結果を出力する関数
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

	// 数値を移動する関数
	bool MoveNum(Vector2Int startIdx, Vector2Int goalIdx) {

		if(goalIdx.x < 0 || goalIdx.x >= field.GetLength(1)) { return false; }
		if(goalIdx.y < 0 || goalIdx.y >= field.GetLength(0)) { return false; }

		Vector2Int direction = goalIdx - startIdx;
		Vector2Int tmp = direction;
		int count = 1;

		// 移動先が箱じゃなくなるまでループ
		while(map[startIdx.y + tmp.y, startIdx.x + tmp.x] == 2) {
			tmp += direction;
			count++;

			// 範囲外に出た場合は移動できない
			if(startIdx.x + tmp.x < 0 || startIdx.x + tmp.x >= field.GetLength(1)) { return false; }
			if(startIdx.y + tmp.y < 0 || startIdx.y + tmp.y >= field.GetLength(0)) { return false; }
		}

		for(int i = count; i > 0; i--) {
			map[startIdx.y + tmp.y, startIdx.x + tmp.x]
				= map[startIdx.y + tmp.y - direction.y, startIdx.x + tmp.x - direction.x];//マップ番号の移動
			map[startIdx.y + tmp.y - direction.y, startIdx.x + tmp.x - direction.x] = 0;// 空いた穴を0にする

			field[startIdx.y + tmp.y, startIdx.x + tmp.x]
				= field[startIdx.y + tmp.y - direction.y, startIdx.x + tmp.x - direction.x];//マップ番号の移動
			field[startIdx.y + tmp.y - direction.y, startIdx.x + tmp.x - direction.x] = null;// 空いた穴を0にする

			field[startIdx.y + tmp.y, startIdx.x + tmp.x].transform.position =
				new Vector3(startIdx.x + tmp.x, map.GetLength(0) - startIdx.y + tmp.y, 0);

			tmp -= direction;
		}

		return true;
	}

	// プレイヤーのいる座標を取得する関数
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