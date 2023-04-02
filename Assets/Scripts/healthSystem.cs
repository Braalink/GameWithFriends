using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthSystem : MonoBehaviour{
    [SerializeField] GameObject healthPrefab;
    [SerializeField] GameObject noHealthPrefab;

    public void DrawHearts (int hp, int maxHp){
        foreach (Transform child in transform){
            Destroy(child.gameObject);
        }
        for (int i = 0; i < maxHp; i++){
            if (i+1 <= hp){
                GameObject heart = Instantiate(healthPrefab, transform.position, Quaternion.identity);
                heart.transform.parent = transform;
            }else{
                GameObject heart = Instantiate(noHealthPrefab, transform.position, Quaternion.identity);
                heart.transform.parent = transform;
            }
        }
    }
}
