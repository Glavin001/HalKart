using UnityEngine;

[RequireComponent(typeof(CarController))]
public class CarUserControl : MonoBehaviour,
OuyaSDK.IMenuButtonUpListener,
OuyaSDK.IMenuAppearingListener,
OuyaSDK.IPauseListener,
OuyaSDK.IResumeListener
{
	private CarController car;  // the car controller we want to use
	public float joystickDeadzone = 0.25f;
	public OuyaSDK.OuyaPlayer controllerIndex = OuyaSDK.OuyaPlayer.player1;
	
	void Awake()
	{
		// get the car controller
		car = GetComponent<CarController>();
		
		OuyaSDK.registerMenuButtonUpListener(this);
		OuyaSDK.registerMenuAppearingListener(this);
		OuyaSDK.registerPauseListener(this);
		OuyaSDK.registerResumeListener(this);
		Input.ResetInputAxes();
	}
	void OnDestroy()
	{
		OuyaSDK.unregisterMenuButtonUpListener(this);
		OuyaSDK.unregisterMenuAppearingListener(this);
		OuyaSDK.unregisterPauseListener(this);
		OuyaSDK.unregisterResumeListener(this);
		Input.ResetInputAxes();
	}
	
	#region Ouya Listeners
	public void OuyaMenuButtonUp()
	{
	}
	
	public void OuyaMenuAppearing()
	{
	}
	
	public void OuyaOnPause()
	{
	}
	
	public void OuyaOnResume()
	{
	}
	#endregion
	
	void FixedUpdate()
	{
		// pass the input to the car!
		
		Vector2 leftStick = new Vector2(0f,0f);
		
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_X, controllerIndex)) > joystickDeadzone)
		{
			leftStick.x = OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_X, controllerIndex);
		}
		
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, controllerIndex)) > joystickDeadzone)
		{
			leftStick.y = OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, controllerIndex);
		}
		
		car.Move(leftStick.x, -leftStick.y); //-y because Unity 4.3 inverts it
	}
}