using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraShake : MonoBehaviour {

	//public bool isShaking = false;

	void Start()
	{
		Vector3 originalPos = transform.localPosition;
	}


	/*public void OnCollisionEnter (Collision collision) {

		if (collision.gameObject.CompareTag("Sawblade")) {
			StartCoroutine(Shake(0.15f, 0.08f));
		}
	}

	public void OnCollisionExit (Collision collision) {

		if (collision.gameObject.CompareTag("Sawblade")) {
			StopCoroutine(Shake(0.15f, 0.08f));
		}
	}*/
	void Update()
	{
        if (Input.GetKeyDown("p"))
        {
            StartCoroutine(Shake(5f, 1.00f));
        }
	}
	public void triggerShakeBig()
	{
		StartCoroutine(Shake(5f, 1.00f));
		Debug.Log("Big Quake");
	}

	public void triggerShakeSmall()
	{
		StartCoroutine(Shake(0.5f, 2.5f));
		Debug.Log("Small Quake");
	}

	//Camera shake function.
	public IEnumerator Shake (float duration, float magnitude)
	{
		Vector3 originalPos = transform.localPosition;

		float elapsed = 0.0f;

		while (elapsed < duration)
		{
			float x = Random.Range (-3, 3) * magnitude;
			float y = Random.Range (-3, 3) * magnitude;

			transform.localPosition = new Vector3 (originalPos.x + x, originalPos.y + y, originalPos.z);

			elapsed += Time.deltaTime;

			yield return null;
		}

		transform.localPosition = new Vector3 (0,0,0);
	}
}
