using UnityEngine;
using System.Collections;
using TouchScript.Gestures;
using TouchScript.Gestures.Simple;
using TouchScript.Hit;

public class InputHandler : MonoBehaviour {
	public Jogador player;
	public FollowTrackingCamera _camera;
	public float tapMaxTime = 0.3f;

	float metaGestureBegin;

	private void Start()
	{
		MetaGesture metaGesture = GetComponent<MetaGesture>();
		metaGesture.StateChanged += MetaGestureEvent;

		SimpleScaleGesture scaleGesture = GetComponent<SimpleScaleGesture>();
		scaleGesture.StateChanged += ScaleTouchEvent;

		SimpleRotateGesture rotateGesture = GetComponent<SimpleRotateGesture>();
		rotateGesture.StateChanged += RotateTouchEvent;

	}

	private void RotateTouchEvent(object sender, TouchScript.Events.GestureStateChangeEventArgs e){
		switch(e.State){
			case Gesture.GestureState.Began:
			case Gesture.GestureState.Changed: 
				SimpleRotateGesture gesture = sender as SimpleRotateGesture;
				_camera.Rotate(gesture.LocalDeltaRotation);
			//Debug.Log(gesture.LocalDeltaRotation);
			break;
		}
	}

	private void ScaleTouchEvent(object sender, TouchScript.Events.GestureStateChangeEventArgs e){
		switch(e.State){
			case Gesture.GestureState.Began:
			case Gesture.GestureState.Changed: 
				SimpleScaleGesture gesture = sender as SimpleScaleGesture;
				if(gesture.LocalDeltaScale > 1)
					_camera.Zoom(gesture.LocalDeltaScale-1);
				else if(gesture.LocalDeltaScale < 1)
					_camera.Zoom(-(1-gesture.LocalDeltaScale));
			break;
		}
	}

	private void MetaTapEvent(object sender, TouchScript.Events.GestureStateChangeEventArgs e)
	{
		player.Attack();
	}

	private void MetaGestureEvent(object sender, TouchScript.Events.GestureStateChangeEventArgs e)
	{
		var gesture = sender as MetaGesture;
		TouchHit hit;

		switch(e.State){
		case Gesture.GestureState.Began:
			metaGestureBegin = Time.timeSinceLevelLoad;
			gesture.GetTargetHitResult(out hit);
			player.TargetPosition = hit.Point;

			break;
		case Gesture.GestureState.Changed: 
			player.ShowTargetCursor();
			gesture.GetTargetHitResult(out hit);

			player.TargetPosition = hit.Point;
		break;
		case Gesture.GestureState.Ended:
			if( (Time.timeSinceLevelLoad - metaGestureBegin) < tapMaxTime)
				MetaTapEvent(sender, e);
			break;
		}
	}
}
