#if UNITY_WIIU
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InControl
{
	public class WiiUGamePadDevice : WiiUDevice 
	{
        public WiiUGamePad gamepad;

		public WiiUGamePadDevice ( int deviceIndex )
			: base( deviceIndex )
		{
		}


		public override void Update( ulong updateTick, float deltaTime )
		{
			QueryState();

			UpdateWithValue( InputControlType.LeftStickX, gamepad.leftStick.x, updateTick );
			UpdateWithValue( InputControlType.LeftStickY, gamepad.leftStick.y, updateTick );
			UpdateWithValue( InputControlType.RightStickX, gamepad.rightStick.x, updateTick );
			UpdateWithValue( InputControlType.RightStickY, gamepad.rightStick.y, updateTick );

			UpdateWithValue( InputControlType.LeftTrigger, gamepad.GetButton(WiiUGamePadButton.ButtonZL)?1:0, updateTick );
			UpdateWithValue( InputControlType.RightTrigger, gamepad.GetButton(WiiUGamePadButton.ButtonZR)?1:0, updateTick );

			UpdateWithState( InputControlType.DPadUp, gamepad.GetButton(WiiUGamePadButton.ButtonUp), updateTick );
			UpdateWithState( InputControlType.DPadDown, gamepad.GetButton(WiiUGamePadButton.ButtonDown), updateTick );
            UpdateWithState(InputControlType.DPadLeft, gamepad.GetButton(WiiUGamePadButton.ButtonLeft), updateTick);
            UpdateWithState(InputControlType.DPadRight, gamepad.GetButton(WiiUGamePadButton.ButtonRight), updateTick);

            UpdateWithState(InputControlType.Action1, gamepad.GetButton(WiiUGamePadButton.ButtonA), updateTick);
            UpdateWithState(InputControlType.Action2, gamepad.GetButton(WiiUGamePadButton.ButtonB), updateTick);
            UpdateWithState(InputControlType.Action3, gamepad.GetButton(WiiUGamePadButton.ButtonX), updateTick);
            UpdateWithState(InputControlType.Action4, gamepad.GetButton(WiiUGamePadButton.ButtonY), updateTick);

			UpdateWithState( InputControlType.LeftBumper, gamepad.GetButton(WiiUGamePadButton.ButtonL), updateTick );
			UpdateWithState( InputControlType.RightBumper, gamepad.GetButton(WiiUGamePadButton.ButtonR), updateTick );

			UpdateWithState( InputControlType.Start, gamepad.GetButton(WiiUGamePadButton.ButtonPlus), updateTick );
			UpdateWithState( InputControlType.Back, gamepad.GetButton(WiiUGamePadButton.ButtonMinus), updateTick );
		}

        


		public override void Vibrate( float leftMotor, float rightMotor )
		{
		}


		public override void QueryState()
		{
            gamepad = WiiUInput.GetGamePad();
		}


		new public bool IsConnected
		{
			get { return gamepad != null; }
		}
	}
}
#endif