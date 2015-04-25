#if UNITY_WIIU
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InControl
{
	public class WiiUDevice : InputDevice
	{
		public int DeviceIndex { get; private set; }

        public WiiUClassic classic;
        public WiiURemote remote;
        public WiiUProController pro;

        public WiiUDevType devType;

		public WiiUDevice ( int deviceIndex )
			: base( "Wii U Controller" )
		{
			DeviceIndex = deviceIndex;
			SortOrder   = deviceIndex;

			Meta = "Wii U Device #" + deviceIndex;

			AddControl( InputControlType.LeftStickX, "LeftStickX" );
			AddControl( InputControlType.LeftStickY, "LeftStickY" );
			AddControl( InputControlType.RightStickX, "RightStickX" );
			AddControl( InputControlType.RightStickY, "RightStickY" );

			AddControl( InputControlType.LeftTrigger, "LeftTrigger" );
			AddControl( InputControlType.RightTrigger, "RightTrigger" );

			AddControl( InputControlType.DPadUp, "DPadUp" );
			AddControl( InputControlType.DPadDown, "DPadDown" );
			AddControl( InputControlType.DPadLeft, "DPadLeft" );
			AddControl( InputControlType.DPadRight, "DPadRight" );

			AddControl( InputControlType.Action1, "Action1" );
			AddControl( InputControlType.Action2, "Action2" );
			AddControl( InputControlType.Action3, "Action3" );
			AddControl( InputControlType.Action4, "Action4" );

			AddControl( InputControlType.LeftBumper, "LeftBumper" );
			AddControl( InputControlType.RightBumper, "RightBumper" );

			AddControl( InputControlType.Start, "Start" );
			AddControl( InputControlType.Back, "Back" );

		}


		public override void Update( ulong updateTick, float deltaTime )
		{
			QueryState();

            switch (devType)
            {
                case WiiUDevType.Classic:
                    UpdateClassic(updateTick, deltaTime);
                    break;
                case WiiUDevType.Core:
                    UpdateRemote(updateTick, deltaTime);
                    break;
                default:
                    break;
            }

		}

        


		public override void Vibrate( float leftMotor, float rightMotor )
		{
		}

        void UpdateClassic(ulong updateTick, float deltaTime)
        {

            //Debug.Log("updating classic");

			UpdateWithValue( InputControlType.LeftStickX, classic.leftStick.x, updateTick );
			UpdateWithValue( InputControlType.LeftStickY, classic.leftStick.y, updateTick );
			UpdateWithValue( InputControlType.RightStickX, classic.rightStick.x, updateTick );
			UpdateWithValue( InputControlType.RightStickY, classic.rightStick.y, updateTick );

			UpdateWithValue( InputControlType.LeftTrigger, classic.leftTrigger, updateTick );
			UpdateWithValue( InputControlType.RightTrigger, classic.rightTrigger, updateTick );

			UpdateWithState( InputControlType.DPadUp, classic.GetButton(WiiUButton.ClassicUp), updateTick );
			UpdateWithState( InputControlType.DPadDown, classic.GetButton(WiiUButton.ClassicDown), updateTick );
			UpdateWithState( InputControlType.DPadLeft, classic.GetButton(WiiUButton.ClassicLeft), updateTick );
			UpdateWithState( InputControlType.DPadRight, classic.GetButton(WiiUButton.ClassicRight), updateTick );

			UpdateWithState( InputControlType.Action1, classic.GetButton(WiiUButton.ClassicA), updateTick );
			UpdateWithState( InputControlType.Action2, classic.GetButton(WiiUButton.ClassicB), updateTick );
			UpdateWithState( InputControlType.Action3, classic.GetButton(WiiUButton.ClassicX), updateTick );
			UpdateWithState( InputControlType.Action4, classic.GetButton(WiiUButton.ClassicY), updateTick );

			UpdateWithState( InputControlType.LeftBumper, classic.GetButton(WiiUButton.ClassicL), updateTick );
			UpdateWithState( InputControlType.RightBumper, classic.GetButton(WiiUButton.ClassicR), updateTick );

			UpdateWithState( InputControlType.Start, classic.GetButton(WiiUButton.ClassicPlus), updateTick );
			UpdateWithState( InputControlType.Back, classic.GetButton(WiiUButton.ClassicMinus), updateTick );
        }

        void UpdateRemote(ulong updateTick, float deltaTime)
        {
			UpdateWithState( InputControlType.DPadUp, remote.GetButton(WiiUButton.ButtonRight), updateTick );
			UpdateWithState( InputControlType.DPadDown, remote.GetButton(WiiUButton.ButtonLeft), updateTick );
			UpdateWithState( InputControlType.DPadLeft, remote.GetButton(WiiUButton.ButtonUp), updateTick );
			UpdateWithState( InputControlType.DPadRight, remote.GetButton(WiiUButton.ButtonDown), updateTick );
			UpdateWithState( InputControlType.Action1, remote.GetButton(WiiUButton.Button2) || remote.GetButtonDown(WiiUButton.ButtonA), updateTick );
			UpdateWithState( InputControlType.Action2, remote.GetButton(WiiUButton.Button1), updateTick );

			UpdateWithState( InputControlType.Start, remote.GetButton(WiiUButton.ButtonPlus), updateTick );
			UpdateWithState( InputControlType.Back, remote.GetButton(WiiUButton.ButtonMinus), updateTick );
        }

		public virtual void QueryState()
		{
            WiiUInput.Probe((uint)DeviceIndex, out devType);
            switch(devType){
                case WiiUDevType.Classic:
                    classic = WiiUInput.GetClassic((uint)DeviceIndex);
                    break;
                case WiiUDevType.Core:
                    remote = WiiUInput.GetRemote((uint)DeviceIndex);
                    break;
                default:
                    break;
            }
		}


		public bool IsConnected
		{
			get { return !(devType == WiiUDevType.NotFound || devType == WiiUDevType.NotSupported || devType == WiiUDevType.Unknown); }
		}
	}
}
#endif