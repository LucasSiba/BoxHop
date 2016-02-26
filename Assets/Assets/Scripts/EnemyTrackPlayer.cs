using UnityEngine;
using System.Collections;

public class EnemyTrackPlayer : MonoBehaviour {

	public GameObject player;
	public float      speed;

	private Vector3 direction;

	void Start () {
		player = GameObject.FindWithTag("Player");
		Invoke("BlowUp",  7);
		InvokeRepeating("SlowDown",  1, 1);
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

	void SlowDown() {
		speed = speed - 0.2f;
		if (speed < 0.0) {
			Destroy (transform.gameObject);
		}
	}

	void BlowUp() {
		Collider[] colliders = Physics.OverlapSphere (transform.position, 1);
		foreach (Collider c in colliders) {
			Rigidbody rb = c.GetComponent<Rigidbody> ();
			if (rb == null)	continue;
			if (rb.gameObject.name != "Ground") {
				rb.AddExplosionForce (1000, transform.position + new Vector3(Random.Range(-2.0f, 2.0f), 0.0f, Random.Range(-2.0f, 2.0f)), 1, 1.0f, ForceMode.Force);
			}
		}
	}

}
