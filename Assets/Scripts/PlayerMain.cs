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
        float joyMvRaw = Input.GetAxisRaw("Horizontal");
//		joyMv = Mathf.Pow(Mathf.Abs(joyMv),3.0f) * Mathf.Sign(joyMv);

		
		
		playerCtrl.ActionMove(joyMv,joyMvRaw);

        
		// ジャンプ
		if (Input.GetButtonDown ("Jump")) {
			playerCtrl.ActionJump ();
			return;
		}
        
	
	}
}
