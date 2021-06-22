using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTwoManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Spawners;
    [SerializeField] Meteorite Meteorite;

    void Awake(){
        foreach(GameObject spawn in Spawners){
            spawn.SetActive(false);
        }
    }

    void Start(){
        StartCoroutine(MeteoriteSystem());
    }

    IEnumerator MeteoriteSystem(){
        Player player = FindObjectOfType<Player>();
        BlackBackground blackBackground = FindObjectOfType<BlackBackground>();
        Color tmp = blackBackground.GetSprite().color;

        for(int i = 100; i > 0; i--){
            tmp.a = (float)i / 100f;
            blackBackground.GetSprite().color = tmp;
            yield return new WaitForSeconds(0.02f);
        }

        for(int i = 0; i < 200; i++){
            player.transform.position = Vector3.Lerp(player.transform.position, new Vector3(0f, -500f, 0f), (float)i / 200f);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(3f);

        for(int i = 0; i < Random.Range(50, 85); i++){
            Meteorite meteor = Instantiate(Meteorite, new Vector3(Random.Range(-1030, 1030), 770, 0), Quaternion.identity);
            float size = Random.Range(200, 400);
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
