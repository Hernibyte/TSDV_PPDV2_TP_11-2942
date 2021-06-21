using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Spawners;
    [SerializeField] Meteorite Meteorite;
    //bool bossPhase = false;

    void Awake(){
        foreach(GameObject spawn in Spawners){
            spawn.SetActive(false);
        }
    }

    void Start(){
        StartCoroutine(MeteoriteSystem());
    }

    IEnumerator MeteoriteSystem(){
        yield return new WaitForSeconds(5f);

        for(int i = 0; i < Random.Range(50, 85); i++){
            Meteorite meteor = Instantiate(Meteorite, new Vector3(Random.Range(-1030, 1030), 770, 0), Quaternion.identity);
            float size = Random.Range(500, 850);
            meteor.transform.localScale = new Vector3(size, size, 0f);
            meteor.localDamage = 40f;
            yield return new WaitForSeconds(0.3f);
        }

        yield return new WaitForSeconds(10f);

        foreach(GameObject spawn in Spawners){
            spawn.SetActive(true);
        }

        yield return null;
    }
}
