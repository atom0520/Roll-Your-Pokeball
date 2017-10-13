using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	GameObject target;
	public Rigidbody body;
	public float speed = 2.0f;
	public float lifeDuration = 5.0f;

	bool initialized = false;

	public GameObject hitEffectPrefab;

    public int atk = 1;

	public void Init(GameObject target){
		this.target = target;

		Vector3 offset = this.target.transform.position - transform.position;

		body.velocity = Vector3.Normalize (offset) * speed;

		Destroy (gameObject, lifeDuration);

		initialized = true;
	}

	void OnCollisionEnter(Collision collision){
		
		if (initialized) {
			if (collision.gameObject.CompareTag ("Player") ) {
				
				GameObject hitEffect = Instantiate (hitEffectPrefab, collision.contacts[0].point, transform.rotation);
				Destroy (hitEffect, 1.5f);

                collision.gameObject.GetComponent<PlayerController>().DecreaseHp(atk);

                Destroy (gameObject);
			}else
            {
                GameObject hitEffect = Instantiate(hitEffectPrefab, collision.contacts[0].point, transform.rotation);
                Destroy(hitEffect, 1.5f);

                Destroy(gameObject);
            }

		}		
			
	}
}
