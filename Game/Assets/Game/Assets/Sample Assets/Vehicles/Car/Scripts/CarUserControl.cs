using UnityEngine;

[RequireComponent(typeof(CarController))]
public class CarUserControl : MonoBehaviour,
OuyaSDK.IMenuButtonUpListener,
OuyaSDK.IMenuAppearingListener,
OuyaSDK.IPauseListener,
OuyaSDK.IResumeListener
{
		private CarController car;  // the car controller we want to use
		private PlayerController player;
		public float joystickDeadzone = 0.25f;
		public OuyaSDK.OuyaPlayer controllerIndex = OuyaSDK.OuyaPlayer.player1;

		void Awake ()
		{
				// get the car controller
				car = GetComponent<CarController> ();
				player = this.gameObject.GetComponent<PlayerController>();
		
				OuyaSDK.registerMenuButtonUpListener (this);
				OuyaSDK.registerMenuAppearingListener (this);
				OuyaSDK.registerPauseListener (this);
				OuyaSDK.registerResumeListener (this);
				Input.ResetInputAxes ();
		}

		void OnDestroy ()
		{
				OuyaSDK.unregisterMenuButtonUpListener (this);
				OuyaSDK.unregisterMenuAppearingListener (this);
				OuyaSDK.unregisterPauseListener (this);
				OuyaSDK.unregisterResumeListener (this);
				Input.ResetInputAxes ();
		}
	
	#region Ouya Listeners
		public void OuyaMenuButtonUp ()
		{
		}
	
		public void OuyaMenuAppearing ()
		{
		}
	
		public void OuyaOnPause ()
		{
		}
	
		public void OuyaOnResume ()
		{
		}
	#endregion

		void Update ()
		{
			if (OuyaExampleCommon.GetButton (OuyaSDK.KeyEnum.BUTTON_O, controllerIndex)) 
			{
				player.UsePowerUp();
			}
		}
	
		void FixedUpdate ()
		{
				// pass the input to the car!

				Vector2 leftStick = new Vector2 (0f, 0f);

				// PC Controller
				leftStick.x = Input.GetAxis ("Horizontal");
				leftStick.y = -Input.GetAxis ("Vertical");

				// Ouya Controller
				// Left/Right
				if (Mathf.Abs (OuyaExampleCommon.GetAxis (OuyaSDK.KeyEnum.AXIS_LSTICK_X, controllerIndex)) > joystickDeadzone) {
						leftStick.x = OuyaExampleCommon.GetAxis (OuyaSDK.KeyEnum.AXIS_LSTICK_X, controllerIndex);
				}
				// Forward/Reverse
				/*
				if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, controllerIndex)) > joystickDeadzone)
				{
					leftStick.y = OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, controllerIndex);
				}
				*/

				float forward = OuyaExampleCommon.GetAxis (OuyaSDK.KeyEnum.BUTTON_RT, controllerIndex);
				float reverse = OuyaExampleCommon.GetAxis (OuyaSDK.KeyEnum.BUTTON_LT, controllerIndex);
				
				float y = -(forward - reverse);
				if (Mathf.Abs(y) > joystickDeadzone) {
					leftStick.y = y;
				}
		
				car.Move (leftStick.x, -leftStick.y); //-y because Unity 4.3 inverts it
		}
}