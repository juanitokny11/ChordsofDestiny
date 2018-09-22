using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantGenerator : MonoBehaviour {

	public GameObject creeper;
	public Transform[] points;
	public float timeToSpawn;
    private Vector3 random;
    private GameObject icreeper;

    IEnumerator Start(){
		while (true) {
			yield return new WaitForSeconds (timeToSpawn);
            random.x = Random.Range(7, 31);
            random.y = Random.Range(0, 12);
            random.z = Random.Range(-18, 18);
            icreeper = Instantiate (creeper);
			MutantBehaviour cb = icreeper.GetComponent<MutantBehaviour> ();
            this.transform.position = random;
            cb.pathNodes = points;
		}
	}
}
