using UnityEngine;
using System.Collections;

public class FireBullet : MonoBehaviour {
    //===外部パラメータ====
    public GameObject bullet;

    
    //===内部パラメータ====
    GameObject[] bulletsList = new GameObject[20];
    PlayerController playerCtrl;



    void Awake() {
        playerCtrl = transform.parent.GetComponent<PlayerController>();
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void Fire() {
        GameObject obj = (GameObject)Instantiate(bullet,transform.position, transform.rotation);
        BaseBullet bulletScript =  obj.GetComponent<BaseBullet>();
        bulletScript.Fired(playerCtrl.dir);

    }

}
