using UnityEngine;
using System.Collections;

public class ExplodeOnImpact : MonoBehaviour {

	public int radius = 10;
	public int force  = 10;

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.name == "Player") {
			Collider[] colliders = Physics.OverlapSphere (collision.contacts [0].point, radius);
			foreach (Collider c in colliders) {
				Rigidbody rb = c.GetComponent<Rigidbody> ();
				if (rb == null)
					continue; 
				rb.AddExplosionForce (force, collision.contacts [0].point, radius, 0.5f, ForceMode.Force);
			}
		}
    }
}
