using UnityEngine;
using System.Collections;

public class EnemyBouncePlayer : MonoBehaviour {

	public GameObject player;
	public float      speed = 3;
	public AudioClip  bounceSound;

	private AudioSource audioSource;

	private Vector3 direction;

	void Start () {
		player = GameObject.FindWithTag("Player");
		audioSource = GetComponent<AudioSource> ();
		Invoke("BlowUp",  7);
		audioSource.clip = bounceSound;
	}

	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.name != "Ground"
		&&  collision.gameObject.name != "EnemyBounceBox") {
			Collider[] colliders = Physics.OverlapSphere (collision.contacts [0].point, 2);
			foreach (Collider c in colliders) {
				Rigidbody rb = c.GetComponent<Rigidbody> ();
				if (rb == null)	continue;
				if (rb.gameObject.name != "Ground") {
					rb.AddExplosionForce (150, collision.contacts [0].point, 100, 0.5f, ForceMode.Force);
					audioSource.Play ();
				}
			}
		}
	}
		
	void BlowUp() {
		Rigidbody rb = GetComponent<Rigidbody> ();
		rb.AddExplosionForce (1000, new Vector3(transform.position.x, transform.position.y, transform.position.z - Random.Range(-1.0f, 1.0f)), 100, 1f, ForceMode.Force);
	}

}