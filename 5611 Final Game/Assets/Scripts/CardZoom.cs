using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardZoom : MonoBehaviour
{
	public GameObject canvas;

	private GameObject zoomCard;

	public void Awake() {
		canvas = GameObject.Find("Canvas");
	}

	public void OnHoverEnter() {
		zoomCard = Instantiate(gameObject, gameObject.transform.position + new Vector3(0, 270), Quaternion.identity);
		zoomCard.transform.SetParent(canvas.transform, false);
		zoomCard.layer = LayerMask.NameToLayer("Zoom");

		RectTransform rect = zoomCard.GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(240, 344);
	}

	public void OnHoverExit() {
		Destroy(zoomCard);
	}
}
