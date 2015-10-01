using UnityEngine;
using System.Collections;

public class EnemyController : BaseCharacterController {

    // === 外部パラメータ（インスペクタ表示） =====================
    public float initHpMax = 100f;
    public float initMpMax = 100f;
    [Range(0.1f, 100.0f)]
    public float initSpeed = 12.0f;


    protected override void Awake() {
        base.Awake();
        speed = initSpeed;
        SetHP(initHpMax, initHpMax);
        SetMP(initMpMax, initMpMax);
    }


    public override void Dead(bool gameOver) {
        //	animator.SetTrigger ("Dead");
        activeSts = false;
        //StartCoroutine(DeadTest());
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().velocity = new Vector3(3f, 3f, 0f);
        GetComponent<Rigidbody>().AddTorque(10f,10f,-100f);

    }

    IEnumerator DeadTest(){
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        yield return null;

        GetComponent<Rigidbody>().velocity= new Vector3(100f,10f,0f);
        //GetComponent<Rigidbody>().AddTorque(100f,100f,100f);
    }
    

}
