using System;
using UnityEngine;
using System.Collections;

public class LiteItem {
	public string ItemName{get; set;}
	public Action OnSelection{get; set;}
	public Action OnRollOver{get; set;}
	public Action OnRollOff{get; set;}

	public LiteItem(string itemName, Action onSelection)
	: this(itemName, onSelection, null, null){}

	public LiteItem(string itemName, Action onSelection, Action onRollOver = null, Action onRollOff = null){
		this.ItemName = itemName;
		this.OnSelection = onSelection;
		this.OnRollOver = onRollOver;
		this.OnRollOff = onRollOff;
	}


	public void Select(){
		if (OnSelection != null) OnSelection();
	}


	public void RollOver(){
		if (OnRollOver != null) OnRollOver();
	}


	public void RollOff(){
		if (OnRollOff != null) OnRollOff();
	}

}