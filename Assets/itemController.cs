using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemController : MonoBehaviour {

	#region ClassVars

	char[,] numberModText = new char[1,2] {{'a','a'}};
	float[,] score = new float[1,2];
	float MainButtonAdding = 1f;
	float SPS = 0f;
	Item[] items = new Item[8];

	int BuyingAmmount = 1;
	#endregion

	#region UpdateingThings
	void Start() {
		score [0, 0] = 0;
		score [0, 1] = 0;
	}
	void Update() {
		AddScorePS ();
	}

	// called to update and then return your current score.
	public string updateScore() {
		
		if (score[0,0] > 1000000f){
			score [0, 1]++;
			score [0, 0] /= 1000000f;
			UpdateScorePerSec ();
		}
		if (score[0,1] == 0) {
			
			return score [0, 0].ToString ("#0.00");
		}

		return score [0, 0].ToString ("#0.00") + " " + numberModText[0,0] + "" + numberModText[0,1]  ;
	}

	// called whenver an item is baught.
	void UpdateScorePerSec () {
		float SPSTemp = 0;
		foreach (var item in items) {
			SPSTemp += item.getScorePS ();
		}
		if (score [0, 1] > 0f) {
			SPS = SPSTemp / (1000000f * score [0, 1]);
		} else {
			SPS = SPSTemp;
		}
	}
	#endregion

	#region ItemAmmounts 
	// addding items to the item array. This is called in the Item.cs script.
	public void addItem(int id, Item type) {
		items [id] = type;

	}
	// Changes the buying ammount to specified number allowing the player to buy in bulk. (defaults to 1)
	public void AmmountToBuy(int ammount) {
		BuyingAmmount = ammount;
	}


	// calls the buy function from the item defined via the ID. And then changes the cost and decreases current money through the purchase.
	public void buy(int id) {
		int[,] test = items [id].purcheseCheck (BuyingAmmount);
		float cost = calcCost (test);
		if ((score[0,0] - cost) >= 0) {
			items [id].increaseItemAmmount (BuyingAmmount);
			score [0, 0] -= cost;
			Debug.Log("purchase Sucess");
		}

		Debug.Log ("{ " + test[0,0].ToString() + " }:{ " + test[0,1].ToString() + " }\n" + "cost: " + cost.ToString());
		UpdateScorePerSec ();

	}
	#endregion

	#region ScoreControll
	// add's curency through clicks. It can later be modified to have greater impact.
	public void UseButtonMain () {
		if (score [0, 1] == 0) {
			score [0, 0] += MainButtonAdding;
		} else {
			score [0, 0] += (MainButtonAdding / (1000000f * score [0, 1]));
		}
	}


	// called from Update() to lineraly increment score over time.
	void AddScorePS(){
		score[0,0] += SPS * Time.deltaTime;

	}

	// calculates cost of a baught item
	float calcCost (int[,] X) {
		
		if (score[0,1] > X[0,1]) {
			return (X [0, 0] / (1000000f * (score [0, 1] - X [0, 1])));
		} else if (score[0,1] < X[0,1]) {
			return (X [0, 0] / (1000000f / (X [0, 1] - score [0, 1])));
		} else {
			return X [0, 0];
		}
			
	}
	#endregion
}
