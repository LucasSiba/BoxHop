using UnityEngine;
using System.Collections;

public class EnemyTrackPlayer : MonoBehaviour {

	public GameObject player;
	public float        speed = 3.0f;

	private Rigidbody rb;
	private Vector3 movement;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		player = GameObject.FindWithTag("Player");

	}

	void Update () {
		movement.Set (player.transform.position.x, 0, player.transform.position.z);
		movement = movement.normalized * speed * Time.deltaTime;
		movement.y = 2 * speed * Time.deltaTime; // Always Jumping
		rb.MovePosition (transform.position + movement);
	}
}
