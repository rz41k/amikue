using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


    
	// === 外部パラメータ（インスペクタ表示） =====================
    private GameObject playerObj;

	// === 外部パラメータ ======================================
	//[System.NonSerialized] public float		hpMax			= 10.0f;
    /*
    [System.NonSerialized] public LayerName layerName { get; }
    [System.NonSerialized] public SceneName sceneName { get; }
    [System.NonSerialized] public TagName tagName { get; }
    */


	// === キャッシュ ==========================================
//	[System.NonSerialized] public Animator	animator;
    private PlayerController playerctrl;
	// === 内部パラメータ ======================================






	// === コード（Monobehaviour基本機能の実装） ================



	// Use this for initialization
	void Awake () {
        //各nameを取得
        if (playerObj == null) {
            playerObj =  GameObject.Find("Player");
        }

        playerctrl = playerObj.GetComponent<PlayerController>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
