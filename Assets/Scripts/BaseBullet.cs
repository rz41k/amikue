using UnityEngine;
using System.Collections;

public class BaseBullet : MonoBehaviour
{

    //===外部パラメーター======
    public float damage = 10;
    public float speed = 10f;
    public float angle = 0f;
    public float surviveSeconds = 3f;

    //====プロパティ=====
    public FireBullet ownerFireBullet{get; set;}

    //===内部パラメーター======
    

    //===キャッシュ=======
    Collider bulletCollider;
    Rigidbody bulletRigidbody;



    void Awake() {
        bulletCollider = GetComponent<Collider>();
        bulletRigidbody = GetComponent<Rigidbody>();

    }


    //====衝突判定====

    void OnTriggerEnter(Collider other) {
        Debug.Log("Ontirigerenter");
        if (other.tag == TagName.Enemy) {
            other.GetComponent<BaseCharacterController>().ActionDamage(damage);
            Destroy(gameObject);

        }


    }


    //===コマンド====

    public void Fired(float dir) {

        Quaternion qua = Quaternion.AngleAxis(angle * dir, Vector3.forward);
        bulletRigidbody.velocity = qua * new Vector3(dir * speed, 0, 0);
        StartCoroutine(DestroyTimer()); 
    
    
    }


    //===etc=======
    IEnumerator DestroyTimer(){
        yield return new WaitForSeconds(surviveSeconds);
        Destroy(gameObject);
    }
}
