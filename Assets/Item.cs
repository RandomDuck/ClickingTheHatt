using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {
	// Base class data storrage.
	Item thisItem;
	itemController itemCon;
	// these items will be set insde the class. see increseItemAmmount (); and updateTrueCost ();
	int[,] itemTrueCost = new int[1,2] { {0,0} };
	int itemAmmount; 

	// These public varibales will be set in the Unity Hirearcy.
	[Header("Name")]
	public string itemName;
	[Header("ID")]
	public int itemID;	
	[Header("Stats")]
	public int itemCostBase;
	public float itemCostMod = 1;
	public float baseScorePerSec;
	[TextArea]
	public string Description;

	// Initialize and find the components necissary.
	void Start() {
		thisItem = this.gameObject.GetComponent<Item>();
		itemCon = GameObject.FindObjectOfType<itemController>();
		// add the itemID to the itemCon's data array.
		itemCon.addItem(itemID, thisItem);
		updateTrueCost ();

	}
	// returns estamated cost to buy (ammount) of item(s)
	public int[,] purcheseCheck (int ammount) {
		int[,] testScore = new int[1, 2];
		int testAmmount;
		testAmmount = itemAmmount + ammount;
		testScore = itemTrueCost;

		if (testAmmount < 2){
			testScore [0, 0] = itemCostBase;
			return testScore;
		}
		testScore[0,0] = itemCostBase * (int)((itemCostMod * (float)testAmmount - 1) * (float)testAmmount);
		if (testScore[0,0] > 1000000) {
			testScore [0, 1]++;
			testScore [0, 0] /= 1000000;
		}

		int[,] cost = new int[1, 2];
		cost [0, 0] = itemCostBase * (int)((itemCostMod * (float)testAmmount - 1) * (float)testAmmount);
		cost [0, 1] = testScore [0, 1];
		return cost;

	}
	// increase the ammount of items owned.
	public void increaseItemAmmount (int ammount) {
		itemAmmount += ammount;
		updateTrueCost ();
	}
	// increse/decrease itemTrueCost.
	void updateTrueCost () {
		if (itemAmmount < 2) {
			itemTrueCost [0, 0] = itemCostBase;
		}
		itemTrueCost[0,0] = itemCostBase * (int)((itemCostMod * (float)itemAmmount) * (float)itemAmmount);
		if (itemTrueCost[0,0] > 1000000) {
			itemTrueCost [0, 1]++;
			itemTrueCost [0, 0] /= 1000000;
		}
	}
	/* //// REDACTED \\\\ TODO: make this give apropriate ammount of money back.
	// decrease the ammount of items owned.
	public void decreaseItemAmmount (int ammount) {
		itemAmmount -= ammount;
		updateTrueCost ();
	}
	*/

	// update the name in hierarcy and in ingame text
	public void nameUpdate() {
		string itemInfo = itemName + "\n" + Description;
		this.gameObject.name = itemName;
		this.gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = itemInfo;

	}

	// get itemTrueCost variable.
	public int[,] getTrueCost () {
		updateTrueCost ();
		return itemTrueCost;
	}
	// get itemAmmount variable
	public int getItemAmmount () {
		return itemAmmount;
	}
	// get the score "increment per second" modifier for this item (do not simply call BaseScorePerSec)
	public float getScorePS () {
		return baseScorePerSec * itemAmmount;
	}
}

