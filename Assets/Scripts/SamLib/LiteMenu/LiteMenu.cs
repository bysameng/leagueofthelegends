using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LiteMenu{

	public int Count{get{return items.Count;}}
	public LiteItem CurrentSelection{get{return items[CurrentSelectionIndex];}}

	private int _currentSelectionIndex = 0;
	public int CurrentSelectionIndex{
		get{return _currentSelectionIndex;} 
		set{_currentSelectionIndex = value;
			if (_currentSelectionIndex < 0) _currentSelectionIndex = Count - 1;
			if (_currentSelectionIndex > Count-1) _currentSelectionIndex = 0;
		}
	}

	private List<LiteItem> items;

	public LiteMenu(params LiteItem[] items){
		this.items = new List<LiteItem>();
		for(int i = 0; i < items.Length; i++){
			this.items.Add(items[i]);
		}
	}

	public void Reset(){
		CurrentSelectionIndex = 0;
		CurrentSelection.RollOver();
	}

	public LiteItem Next(){
		CurrentSelection.RollOff();
		CurrentSelectionIndex++;
		CurrentSelection.RollOver();
		return CurrentSelection;
	}

	public LiteItem Prev(){
		CurrentSelection.RollOff();
		CurrentSelectionIndex--;
		CurrentSelection.RollOver();
		return CurrentSelection;
	}

	public void Select(){
		CurrentSelection.Select();
	}

	public void AddItem(LiteItem item){
		items.Add(item);
	}

	public void AddItem(string name, Action onStart, Action onRollOver = null, Action onRollOff = null){
		items.Add(new LiteItem(name, onStart, onRollOver, onRollOff));
	}



}