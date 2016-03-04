using UnityEngine;
using System.Collections;
using DigitalRuby.PyroParticles;

public class EnemyPillarOfDoomThrowFire : MonoBehaviour {

	public GameObject player;
	public float      speed;
	public GameObject fireBall;

	private Vector3 direction;

	void Start () {
		player = GameObject.FindWithTag("Player");
		InvokeRepeating("BlowUp",  7, 7);
		Invoke("ThrowFire",  5);
	}

	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
	}

	void BlowUp() {
		Collider[] colliders = Physics.OverlapSphere (transform.position, 1);
		foreach (Collider c in colliders) {
			Rigidbody rb = c.GetComponent<Rigidbody> ();
			if (rb == null)	continue;
			if (rb.gameObject.name != "Ground") {
				rb.AddExplosionForce (1000, new Vector3(transform.position.x, 0.0f, transform.position.z - 1.0f), 1.5f, 0.5f, ForceMode.Force);
			}
		}
	}

	private void ThrowFire()
	{
		GameObject fireBallObject = Instantiate(fireBall, transform.position, transform.rotation) as GameObject;
		FireProjectileScript projectileScript = fireBallObject.GetComponentInChildren<FireProjectileScript>();
		projectileScript.ProjectileExplosionForce = 1000;
	}
}
