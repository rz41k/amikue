using UnityEngine;
using System.Collections;

public class PlayerMain : MonoBehaviour {
    
	// === 内部パラメータ ======================================
	PlayerController 	playerCtrl;
	
	bool 				actionEtcRun = true;

	// === コード（Monobehaviour基本機能の実装） ================
	void Awake () {
		playerCtrl 		= GetComponent<PlayerController>();
	
	}

	void Update () {
		// 操作可能か？
		if (!playerCtrl.activeSts) {
			return;
		}



		// 移動
		float joyMv = Input.GetAxis ("Horizontal");
//		joyMv = Mathf.Pow(Mathf.Abs(joyMv),3.0f) * Mathf.Sign(joyMv);

		
		
		playerCtrl.ActionMove (joyMv);


		// ジャンプ
		if (Input.GetButtonDown ("Jump") || vpad_btnA == zFOXVPAD_BUTTON.DOWN) {
			playerCtrl.ActionJump ();
			return;
		}

	
	}
}
