using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace SamCinema{
	//a vignette is a chain of actions 
	//it's all in a linked list, plays through sequentially
	//add one to SamCinemaManager to start playing it
	public class Vignette{
		private LinkedList<SamAction> actionList;

		public SamAction CurrentAction{get{return actionList.Count != 0 ? actionList.First.Value : null;}}
		public bool IsFinished{get{return actionList.Count == 0;}}
		public bool IsPlaying{get{return SamCinemaManager.main.IsRunning(this);}}
		public int RepeatTimes{get; set;}
		private int repeater;

		public Vignette(){
			actionList = new LinkedList<SamAction>();
			AddAction(new WaitAction(0f));
			RepeatTimes = 1;
		}

		public Vignette(params SamAction[] actions) : this(){
			for(int i = 0; i < actions.Length; i++){
				actionList.AddLast(actions[i]);
			}
		}

		protected Vignette(LinkedList<SamAction> actionList) : this(){
			for(LinkedListNode<SamAction> node = actionList.First; node != null; node = node.Next){
				AddAction(node.Value);
			}
		}

		public Vignette Play(){
			if (!IsPlaying)
				SamCinemaManager.main.AddVignette(this);
			return this;
		}

		public Vignette Stop(){
			SamCinemaManager.main.RemoveVignette(this);
			return this;
		}

		public Vignette Copy(){
			return new Vignette(actionList);
		}

		//called by CinemaManager.
		//Doesn't actually play the vignette
		public void InternalPlay(){
			repeater = 0;
			InternalStart();
		}

		private void InternalStart(){
		}

		//don't call this yourself.
		//called by samcinemamanager's update.
		public void Update(){
			if (CurrentAction != null){
				CurrentAction.Update();
			}
			if (CurrentAction != null && CurrentAction.IsFinished){
				NextAction();
			}
		}

		//add an action. to the back of the list.
		public void AddAction(SamAction action){
			actionList.AddLast(action);
		}
		public void AddAction(System.Action action){
			actionList.AddLast(new SamAction(0f, ()=>action()));
		}
		public void AddAction(SamActionDelegate action){
			actionList.AddLast(new SamAction(0f, action));
		}

		//remove an action.
		//will search the list.
		public void RemoveAction(SamAction action){
			actionList.Remove(action);
		}


		//insert the action NOW.
		// will interrupt current action. current action will be played after this action inserted.
		public Vignette InsertActionFirst(SamAction action, bool interrupt = true){
			if (interrupt && CurrentAction != null) CurrentAction.Interrupt();
			actionList.AddFirst(action);
			RestartCurrentAction();
			return this;
		}

		public Vignette InsertActionLast(SamAction action){
			actionList.AddLast(action);
			return this;
		}

		//warning, it will insert all repeats
		//if you have a vignette with repeat(5), it will insert it 5 times.
		public Vignette InsertVignetteFirst(Vignette v, bool interrupt = true){
			if (interrupt && CurrentAction != null) CurrentAction.Interrupt();
			InsertActionFirst(new WaitForAction(()=>{return v.IsFinished;}));
			RestartCurrentAction();
			if (!SamCinemaManager.main.IsRunning(v)) SamCinemaManager.main.AddVignette(v);
			return this;
		}

		//restart the current action
		public void RestartCurrentAction(){
//			actionIndex--;
//			NextAction();
			CurrentAction.Start();
		}

//		//internal repeat.
//		private void RepeatCurrentAction(){
//		}

		//advance the action to the next one.
		public void NextAction(){
			actionList.RemoveFirst();
//			Debug.Log("going to next action...");
			if (IsFinished) {
//				Debug.Log("no action! ending vignette.");
				return;
			}
			CurrentAction.Start();
		}

		//set repeat to -1 and it loops forevea
		public Vignette Repeat(int times){
			this.RepeatTimes = times;
			return this;
		}

	}


}