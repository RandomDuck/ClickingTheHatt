using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[ExecuteInEditMode]
public class UpdateName : MonoBehaviour {
	Item[] items ;
	itemController itemCon;
	public Text ScoreText;
	int roundedScore;

	void Start () {
		items = GameObject.FindObjectsOfType<Item> ();
		itemCon = GameObject.FindObjectOfType<itemController> ();
		foreach (var item in items) {
			item.nameUpdate ();

		}
	}

	void Update() {
		ScoreText.text = "Hatts: " + (string)itemCon.updateScore();

	}
}
