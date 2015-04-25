#if UNITY_WIIU
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

namespace InControl
{
	public class WiiUDeviceManager : InputDeviceManager
	{
		bool[] deviceConnected = new bool[] { false, false, false, false, false };


		public WiiUDeviceManager()
		{
			for (int deviceIndex = 0; deviceIndex < 4; deviceIndex++)
			{
				devices.Add( new WiiUDevice( deviceIndex ) );
			}
            devices.Add(new WiiUGamePadDevice(5));

			Update( 0, 0.0f );
		}


		public override void Update( ulong updateTick, float deltaTime )
		{
			for (int deviceIndex = 0; deviceIndex < 5; deviceIndex++)
			{
				var device = devices[deviceIndex] as WiiUDevice;

				// Unconnected devices won't be updated otherwise, so poll here.
				if (!device.IsConnected)
				{
					device.Update( updateTick, deltaTime );
				}

				if (device.IsConnected != deviceConnected[deviceIndex])
				{
					if (device.IsConnected)
					{
						InputManager.AttachDevice( device );
					}
					else
					{
						InputManager.DetachDevice( device );
					}

					deviceConnected[deviceIndex] = device.IsConnected;
				}
			}
		}


		public static bool CheckPlatformSupport( ICollection<string> errors )
		{
			if (Application.platform != RuntimePlatform.WiiU &&
			    Application.platform != RuntimePlatform.WindowsEditor)
			{
                //return false;
			}

            //try
            //{
            //    GamePad.GetState( PlayerIndex.One );
            //}
            //catch (DllNotFoundException e)
            //{
            //    if (errors != null)
            //    {
            //        errors.Add( e.Message + ".dll could not be found or is missing a dependency." );
            //    }
            //    return false;
            //}
			
			return true;
		}


		public static void Enable()
		{
			var errors = new List<string>();
			if (WiiUDeviceManager.CheckPlatformSupport( errors ))
			{
                //InputManager.HideDevicesWithProfile( typeof( Xbox360WinProfile ) );
                //InputManager.HideDevicesWithProfile( typeof( XboxOneWinProfile ) );
                //InputManager.HideDevicesWithProfile( typeof( LogitechF710ModeXWinProfile ) );
                //InputManager.HideDevicesWithProfile( typeof( LogitechF310ModeXWinProfile ) );
                //InputManager.AddDeviceManager( new XInputDeviceManager() );
                InputManager.AddDeviceManager(new WiiUDeviceManager());
			}
			else
			{
				foreach (var error in errors)
				{
					Logger.LogError( error );
				}
			}
		}
	}
}
#endif

