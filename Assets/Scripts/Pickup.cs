using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameObject pickupPrefab = null;
	[SerializeField] VoidEvent addTimeEvent;

	private void OnCollisionEnter(Collision collision)
	{
        print(collision.gameObject.name);
	}

	private void OnTriggerEnter(Collider other)
	{
        if (gameObject.tag == "coin" && other.gameObject.TryGetComponent(out Player player))
        {
			player.AddPoints(10);
			Instantiate(pickupPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		if (gameObject.tag == "vase" && other.gameObject.TryGetComponent(out player))
		{
			player.AddHealth();
			Instantiate(pickupPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		if (gameObject.tag == "pipe" && other.gameObject.TryGetComponent(out player))
		{
			addTimeEvent.RaiseEvent();
			Instantiate(pickupPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		if (gameObject.tag == "crate" && other.gameObject.TryGetComponent(out player))
		{
			player.characterController.jumpForce += 10;
			Instantiate(pickupPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}

	}
}


