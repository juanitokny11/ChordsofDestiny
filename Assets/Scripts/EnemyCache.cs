using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCache
{
	public EnemyBehaviour[] enemies;
	public int currentEnemy=0;

	public EnemyCache (GameObject meteorprefab, Vector3 iniPos , Transform parent, int maxEnemy){
        enemies = new EnemyBehaviour[maxEnemy];
		Vector3 tmpOrigin = iniPos;
		for (int i = 0; i < maxEnemy; i++) {
			tmpOrigin.x = iniPos.x + i * 0.5f;
			GameObject go = GameObject.Instantiate (meteorprefab, tmpOrigin, Quaternion.identity, parent);
			go.name = "Groupie" + i;
            enemies[i] = go.GetComponent<EnemyBehaviour> ();
		}
        currentEnemy = 0;
	}
	public EnemyBehaviour GetEnemy(){
		if (currentEnemy > enemies.Length -1) {
            currentEnemy = 0;
		}
		return enemies[currentEnemy++];
	}

}
