using UnityEngine;
using System.Collections;

public class EnemyTrackPlayer : MonoBehaviour {

	public GameObject player;
	public float      speed = 1.6f;

	private Vector3 direction;

	void Start () {
		player = GameObject.FindWithTag("Player");
	}

	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.name != "Ground") {
			Collider[] colliders = Physics.OverlapSphere (collision.contacts [0].point, 1);
			foreach (Collider c in colliders) {
				Rigidbody rb = c.GetComponent<Rigidbody> ();
				if (rb == null)	continue;
				if (rb.gameObject.name != "Ground") {
					rb.AddExplosionForce (500, collision.contacts [0].point, 1, 0, ForceMode.Force);
				}
			}
		}
	}
}
