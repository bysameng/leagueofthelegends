using System;
using System.Collections;
using System.Collections.Generic;


namespace InControl
{
	[AutoDiscover]
	public class WiimoteProfile: UnityInputDeviceProfile
	{
		public WiimoteProfile()
		{
			Name = "Wiimote Controller";
			Meta = "Wiimote Controller on Mac using wJoy";
			
			SupportedPlatforms = new[]
			{
				"OS X",
				"Mac"
			};
			
			JoystickRegex = new[] { "Wiimote" };
			
			Sensitivity = 1.0f;
			LowerDeadZone = 0.2f;
			
			ButtonMappings = new[]
			{

				//pro controller
				new InputControlMapping
				{
					Handle = "B",
					Target = InputControlType.Action1,
					Source = Button19	//Left bumper
				},
				new InputControlMapping
				{
					Handle = "B",
					Target = InputControlType.Action1,
					Source = Button18	//Y
				},
				new InputControlMapping
				{
					Handle = "A",
					Target = InputControlType.Action2,
					Source = Button17	//X
				},
				new InputControlMapping
				{
					Handle = "B",
					Target = InputControlType.Action2,
					Source = Button16
				},
				new InputControlMapping
				{
					Handle = "A",
					Target = InputControlType.Action1,
					Source = Button15
				},
				new InputControlMapping
				{
					Handle = "DPad Right",
					Target = InputControlType.DPadRight,
					Source = Button14
				},
				new InputControlMapping
				{
					Handle = "DPad Left",
					Target = InputControlType.DPadLeft,
					Source = Button13
				},
				new InputControlMapping
				{
					Handle = "DPad Down",
					Target = InputControlType.DPadDown,
					Source = Button12
				},
				new InputControlMapping
				{
					Handle = "DPad Up",
					Target = InputControlType.DPadUp,
					Source = Button11
				},

				//wiimote

				new InputControlMapping
				{
					Handle = "A",
					Target = InputControlType.Action1,
					Source = Button10 //big a
				},
				new InputControlMapping
				{
					Handle = "B",
					Target = InputControlType.Action2,
					Source = Button9 //big a
				},
//				new InputControlMapping
//				{
//					Handle = "A",
//					Target = InputControlType.Action1,
//					Source = Button4 //big a
//				},
//				new InputControlMapping
//				{
//					Handle = "B",
//					Target = InputControlType.Action2,
//					Source = Button5	//big b
//				},

//				new InputControlMapping
//				{
//					Handle = "A",
//					Target = InputControlType.Action1,
//					Source = Button8	//home
//				},

				//shared between pro and wiimote
				new InputControlMapping
				{
					Handle = "Start",
					Target = InputControlType.Pause,
					Source = Button6 //plus
				},

				new InputControlMapping
				{
					Handle = "DPad Left",
					Target = InputControlType.DPadLeft,
					Source = Button2
				},
				new InputControlMapping
				{
					Handle = "DPad Right",
					Target = InputControlType.DPadRight,
					Source = Button3
				},
				new InputControlMapping
				{
					Handle = "DPad Up",
					Target = InputControlType.DPadUp,
					Source = Button1
				},
				new InputControlMapping
				{
					Handle = "DPad Down",
					Target = InputControlType.DPadDown,
					Source = Button0
				}
			};
			AnalogMappings = new[]
			{
				new InputControlMapping
				{
					Handle = "Left Stick X",
					Target = InputControlType.LeftStickX,
					Scale = 2.0f,
					Source = Analog0

				},
				new InputControlMapping
				{
					Handle = "Left Stick Y",
					Target = InputControlType.LeftStickY,
					Scale = 2.0f,
					Source = Analog1,
					Invert = true
				},
				new InputControlMapping
				{
					Handle = "Right Stick X",
					Target = InputControlType.RightStickX,
					Scale = 2.0f,
					Source = Analog2
				},
				new InputControlMapping
				{
					Handle = "Right Stick Y",
					Target = InputControlType.RightStickY,
					Source = Analog3,
					Scale = 2.0f,
					Invert = true
				}
			};
		}
	}
}

