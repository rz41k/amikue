using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseCharacterController : MonoBehaviour {
    
	// === 外部パラメータ（インスペクタ表示） =====================
	public Vector2			velocityMin				 = new Vector2(-100.0f,-100.0f);
	public Vector2			velocityMax				 = new Vector2(+100.0f,+50.0f);
//	public bool				superArmor				 = false;
//	public bool				superArmor_jumpAttackDmg = true;
//	public GameObject[] 	fireObjectList;

	// === 外部パラメータ ======================================
	[System.NonSerialized] public float		hpMax			= 100.0f;
	[System.NonSerialized] public float		hp	 			= 100.0f;
    [System.NonSerialized] public float mp = 100.0f;
    [System.NonSerialized] public float mpMax = 100.0f;

    [System.NonSerialized] public float		dir	 			= 1.0f;
	[System.NonSerialized] public float		speed			= 6.0f;
	[System.NonSerialized] public float 	basScaleX		= 1.0f;
	[System.NonSerialized] public bool		activeSts 		= false;
	[System.NonSerialized] public bool 		jumped 			= false;
	[System.NonSerialized] public bool 		grounded 		= false;
	[System.NonSerialized] public bool 		groundedPrev 	= false;
//	[System.NonSerialized] public bool 		freez			= false;
//	[System.NonSerialized] public float		freezStartTime 	= 0.0f;

	// === キャッシュ ==========================================
	[System.NonSerialized] public Animator	animator;
	protected Transform		groundCheck_L;
	protected Transform 	groundCheck_C;
	protected Transform 	groundCheck_R;
    protected Transform groundChecks;
  

	// === 内部パラメータ ======================================
	
   
    protected float 	 	speedVx 			= 0.0f;
	protected float 		speedVxAddPower		= 0.0f;

	protected GameObject	groundCheck_OnRoadObject;
	protected GameObject	groundCheck_OnMoveObject;
	protected GameObject	groundCheck_OnEnemyObject;

	protected float			jumpStartTime		= 0.0f;

	//protected float 		gravityScale 		= 10.0f;


    /*
    protected bool			addForceVxEnabled	= false;
	protected float			addForceVxStartTime = 0.0f;

	protected bool			addVelocityEnabled	= false;
	protected float			addVelocityVx 		= 0.0f;
	protected float			addVelocityVy 		= 0.0f;

	protected bool			setVelocityVxEnabled= false;
	protected bool			setVelocityVyEnabled= false;
	protected float			setVelocityVx 		= 0.0f;
	protected float			setVelocityVy 		= 0.0f;

	protected AudioSource[]	seAnimationList;

    */

        // === mp管理用 ===================

  
  protected Dictionary<string, float> mpConsumeTable = new Dictionary<string, float>();
   //mp消費量管理用テーブル
   //Tkey: string 消費アクション名
   //Tvalue: float mp消費量 




	// === コード（Monobehaviour基本機能の実装） ================

    
	protected virtual void Awake () {
        //animator 			= GetComponent <Animator>();

        groundChecks = transform.Find(NameManager.GroundCheck.GroundChecks.ToString());
        groundCheck_L = groundChecks.Find(NameManager.GroundCheck.GroundCheck_L.ToString());
		groundCheck_C = groundChecks.Find(NameManager.GroundCheck.GroundCheck_C.ToString());
		groundCheck_R = groundChecks.Find(NameManager.GroundCheck.GroundCheck_R.ToString());
		
        
		dir 				= (transform.localScale.x > 0.0f) ? 1 : -1;
		basScaleX 			= transform.localScale.x * dir;
		transform.localScale = new Vector3 (basScaleX, transform.localScale.y, transform.localScale.z);

		activeSts 			= true;
		//gravityScale 		= GetComponent<Rigidbody2D>().gravityScale;
         
	}
    

	protected virtual void Start () {
	}
	
	protected virtual void Update () {	
		// 落下チェック
		if (transform.position.y < -30.0f) {
			if (activeSts) {
				Dead(false); // 死亡
			}
		}
	}

	protected virtual void FixedUpdate () {	
		// 地面チェック
		groundedPrev = grounded;
		grounded 	 = false;

		groundCheck_OnRoadObject  = null;
		groundCheck_OnMoveObject  = null;
		groundCheck_OnEnemyObject = null;

		Collider[][] groundCheckCollider = new Collider[3][];
		groundCheckCollider [0] = Physics.OverlapSphere(groundCheck_L.position,0.1f);
		groundCheckCollider [1] = Physics.OverlapSphere(groundCheck_C.position,0.1f);
		groundCheckCollider [2] = Physics.OverlapSphere(groundCheck_R.position,0.1f);

		foreach(Collider[] groundCheckList in groundCheckCollider) {
			foreach(Collider groundCheck in groundCheckList) {
				if (groundCheck != null) {
					if (!groundCheck.isTrigger) {
						if (groundCheck.tag == TagName.Road) {
							groundCheck_OnRoadObject = groundCheck.gameObject;
                            grounded = true;
                        } else 
						if (groundCheck.tag == TagName.Others) {
							groundCheck_OnMoveObject = groundCheck.gameObject;
						} else 
						if (groundCheck.tag == TagName.Others) {
							groundCheck_OnEnemyObject = groundCheck.gameObject;
						}
					}
				}
			}
		}
        //移動計算
        if (activeSts) {
            GetComponent<Rigidbody>().velocity = new Vector3(speedVx, GetComponent<Rigidbody>().velocity.y, 0.0f);
        }

        // キャラ個別の処理を実行
        //Debug.Log(string.Format("ChrCom FixedUpdate {0} {1}",name,grounded));
        FixedUpdateCharacter (); 

        /*
		// 乗り物チェック
		if (grounded) {
			speedVxAddPower = 0.0f;
			if (groundCheck_OnMoveObject != null) {
				Rigidbody2D rb2D = groundCheck_OnMoveObject.GetComponentInParent<Rigidbody2D>();
				speedVxAddPower = rb2D.velocity.x;
			}
		}
         * */

        /*
		// 移動計算
		if (addForceVxEnabled) {
			// 移動計算は物理演算にまかせる
			if (Time.fixedTime - addForceVxStartTime > 0.5f) {
				addForceVxEnabled = false;
			}
		} else {
			// 移動計算
			//Debug.Log (">>>> " + string.Format("speedVx {0} y {1} g{2}",speedVx,rigidbody2D.velocity.y,grounded));
			GetComponent<Rigidbody2D>().velocity = new Vector2 (speedVx + speedVxAddPower, GetComponent<Rigidbody2D>().velocity.y);
		}

		// 最終的なVelocity計算
		if (addVelocityEnabled) {
			addVelocityEnabled = false;
			GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x + addVelocityVx, GetComponent<Rigidbody2D>().velocity.y + addVelocityVy);
		}

		// 強制的にVelocityの値をセット
		if (setVelocityVxEnabled) {
			setVelocityVxEnabled = false;
			GetComponent<Rigidbody2D>().velocity = new Vector2 (setVelocityVx, GetComponent<Rigidbody2D>().velocity.y);
		}
		if (setVelocityVyEnabled) {
			setVelocityVyEnabled = false;
			GetComponent<Rigidbody2D>().velocity = new Vector2 (GetComponent<Rigidbody2D>().velocity.x,setVelocityVy);
		}
        */

		// Veclocityの値をチェック
		float vx = Mathf.Clamp (GetComponent<Rigidbody>().velocity.x, velocityMin.x, velocityMax.x);
		float vy = Mathf.Clamp (GetComponent<Rigidbody>().velocity.y, velocityMin.y, velocityMax.y);
		GetComponent<Rigidbody>().velocity = new Vector3(vx,vy,0.0f);
	
        
    }
	protected virtual void FixedUpdateCharacter () {	
	}

    /*
	// === コード（アニメーションイベント用コード） ===============
	public virtual void AddForceAnimatorVx(float vx) {
		//Debug.Log (string.Format("--- AddForceAnimatorVx {0} ----------------",vx));
		if (vx != 0.0f) {
			GetComponent<Rigidbody>().AddForce (new Vector3(vx * dir,0.0f,0.0f));
		addForceVxEnabled	= true;
			addForceVxStartTime = Time.fixedTime;
		}
	}
	
	public virtual void AddForceAnimatorVy(float vy) {
		//Debug.Log (string.Format("--- AddForceAnimatorVy {0} ----------------",vy));
		if (vy != 0.0f) {
			GetComponent<Rigidbody2D>().AddForce (new Vector2(0.0f,vy));
			jumped = true;
			jumpStartTime = Time.fixedTime;
		}
	}
	
	public virtual void AddVelocityVx(float vx) {
		//Debug.Log (string.Format("--- AddVelocityVx {0} ----------------",vx));
		addVelocityEnabled = true;
		addVelocityVx = vx * dir;
	}
	public virtual void AddVelocityVy(float vy) {
		//Debug.Log (string.Format("--- AddVelocityVy {0} ----------------",vy));
		addVelocityEnabled = true;
		addVelocityVy = vy;
	}
	
	public virtual void SetVelocityVx(float vx) {
		//Debug.Log (string.Format("--- setelocityVx {0} ----------------",vx));
		setVelocityVxEnabled = true;
		setVelocityVx = vx * dir;
	}
	public virtual void SetVelocityVy(float vy) {
		//Debug.Log (string.Format("--- setVelocityVy {0} ----------------",vy));
		setVelocityVyEnabled = true;
		setVelocityVy = vy;
	}
	
	public virtual void SetLightGravity() {
		//Debug.Log ("--- SetLightGravity ----------------");
		GetComponent<Rigidbody2D>().velocity 	 = Vector2.zero;
		GetComponent<Rigidbody2D>().gravityScale = 0.1f;
	}
	
	public void EnableFreez() {
		//Debug.Log ("--- EnableFreez ----------------");
		//freez = true;
		//freezStartTime = Time.fixedTime;
	}
	public void DisableFreez() {
		//Debug.Log ("--- DisableFreez ----------------");
		//freez = false;
	}
	
	public void EnableSuperArmor() {
		//Debug.Log ("--- EnableSuperArmor ----------------");
		superArmor = true;
	}
	public void DisableSuperArmor() {
		//Debug.Log ("--- DisableSuperArmor ----------------");
		superArmor = false;
	}
	
	public virtual void PlayAnimationSE(int i) {
		//Debug.Log (string.Format("--- PlayAnimationSE {0} ----------------",i));
		seAnimationList [i].Play ();
	}
     * 
     * */

	// === コード（基本アクション） =============================
	public virtual void ActionMove(float n) {
		if (n != 0.0f) {
			dir 	= Mathf.Sign(n);
			speedVx = speed * n;
			//animator.SetTrigger("Run");
		} else {
			speedVx = 0;
			//animator.SetTrigger("Idle");
		}
	}


    public virtual void ActionDamage(float damage) {
        // Debug:無敵モード
        //if (SaveData.debug_Invicible) {
        //	return;
        //}
        // ダメージ処理をしてもいいか？
        if (!activeSts) {
            return;
        }



        //animator.SetTrigger ("DMG_A");
        //speedVx = 0;
        //GetComponent<Rigidbody2D>().gravityScale = gravityScale;




        if (SetHP(hp - damage, hpMax)) {
            Dead(true); // 死亡
        }
    }


    /*
	public void ActionFire() {
		Transform goFire = transform.Find ("Muzzle");
		foreach(GameObject fireObject in fireObjectList) {
			GameObject go = Instantiate (fireObject,goFire.position,Quaternion.identity) as GameObject;
			go.GetComponent<FireBullet>().ownwer = transform;
		}
	}
	
	public bool ActionLookup(GameObject go,float near) {
		if (Vector3.Distance(transform.position,go.transform.position) > near) {
			dir = (transform.position.x < go.transform.position.x) ? +1 : -1;
			return true;
		}
		return false;
	}
	
	public bool ActionMoveToNear(GameObject go,float near) {
		if (Vector3.Distance(transform.position,go.transform.position) > near) {
			ActionMove( (transform.position.x < go.transform.position.x) ? +1.0f : -1.0f );
			return true;
		}
		return false;
	}
	
	public bool ActionMoveToFar(GameObject go,float far) {
		if (Vector3.Distance(transform.position,go.transform.position) < far) {
			ActionMove( (transform.position.x > go.transform.position.x) ? +1.0f : -1.0f );
			return true;
		}
		return false;
	}
     * 
     * */

	// === コード（その他） ====================================
	public virtual void Dead (bool gameOver) {
	//	animator.SetTrigger ("Dead");
		activeSts = false;
	}

	public virtual bool SetHP(float _hp,float _hpMax) {
		hp 	  		= _hp;
		hpMax 		= _hpMax;

        if (hp < 0) hp = 0;
		return (hp <= 0);
	}
    public virtual bool SetMP(float _mp, float _mpMax)
    {
        mp = _mp;
        mpMax = _mpMax;
        return (mp <= 0);
    }


    //actionが発動できる状態ならtrueを返し、できなけらばfalseを返す
    public virtual bool ConsumeMp(string mpUseAction)
    {
        if (!activeSts)
        {
            return false;
        }

        float mpConsumption = mpConsumeTable[mpUseAction];

        if(mp-mpConsumption < 0)
        {
            return false;
        }

        mp -= mpConsumption;
        return true;

    }

    //--------tools----------------
   

}
