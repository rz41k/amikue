using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public abstract class Mp : MonoBehaviour {

    /*
    mp declare format
     * 
    string decStr = 
    "
    name, consume mp value, failure probability(float)(0-1), Out of control probability(0-1)|
    sample, 10, 0.3, 0.2|
     * 
     * 
     * 
     * 
    "
    


    */

    //====variables======


    List<MpEntity> MpEntitiyList;
    Dictionary<string,int> MpActionTable;


    //====classes=====
/**
    public abstract class MpActionNames{
        /*
         * public const string ActionName = "ActionName"
         * 
         * 
         * 
    }


    public abstract enum MpActionNames{


        }

*/

    //====sttracts



    protected struct MpEntity{
        public string ActionName;
        public float mpConsumption;
        public float failureProbability;
        public float outOfControlProbability;

        public MpEntity(string ActionNm, float mpCon, float failureProbj, float outOfCtrl) {
            ActionName = ActionNm;
            mpConsumption = mpCon;
            failureProbability = failureProbj;
            outOfControlProbability = outOfCtrl;
        }
    }

    

    //====initialaize===



    protected Mp() {
        MpEntitiyList = new List<MpEntity>();
        init();
        MpActionTable = MpEntitiyList.ToDictionary(MpEntity.action);

    }




    //===functionsf=====


    protected abstract void init() {
    /*
      
     * 
     * 
     * MpEntitiyList.Add(new MpEntity(ActionName, float MpConsumption,
     *   float failureProbability, float outOfConrolProbability))
     * *******
     * 
     */ 
    }


    

    /*
    private float generateMpEntity(string decStr) {
        string[] strEntityArray = decStr.Trim().Split('|');

        foreach(string str in strEntityArray){
            string[] strArray = str.Trim().Split(',');


        }


    }
     * 
     * */




    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
