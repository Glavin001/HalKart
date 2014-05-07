using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour,
OuyaSDK.IMenuButtonUpListener,
OuyaSDK.IMenuAppearingListener,
OuyaSDK.IPauseListener,
OuyaSDK.IResumeListener
{
	public float joystickDeadzone = 0.25f;
	public float playerSpeed = 5f;
	public OuyaSDK.OuyaPlayer controllerIndex = OuyaSDK.OuyaPlayer.player1;
	
	void Awake()
	{
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

	//For physics calculations. If not needed, use Update() instead
	void FixedUpdate()
	{
		#region Movement

		Vector2 leftStick = new Vector2(0f,0f);
//PC Input
//		leftStick.x = Input.GetAxis("Horizontal");
//		leftStick.y = Input.GetAxis("Vertical");

		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_X, controllerIndex)) > joystickDeadzone)
		{
			leftStick.x = OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_X, controllerIndex);
		}
		
		if (Mathf.Abs(OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, controllerIndex)) > joystickDeadzone)
		{
			leftStick.y = OuyaExampleCommon.GetAxis(OuyaSDK.KeyEnum.AXIS_LSTICK_Y, controllerIndex);
		}

		Vector3 movement = new Vector3(leftStick.x, 0f, -leftStick.y); // (-) Because Unity 4.3 Maps the Ouyas joystick as inverted by default
		rigidbody.AddForce(movement * playerSpeed);
		#endregion
		
		#region Button Presses
		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_O, controllerIndex))
		{
			rigidbody.AddForce(new Vector3(0f, 100f, 0f));
		}
		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_U, controllerIndex))
		{
			rigidbody.AddForce(new Vector3(0f, 200f, 0f));
		}
		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_Y, controllerIndex))
		{
			rigidbody.AddForce(new Vector3(0f, 400f, 0f));
		}
		if (OuyaExampleCommon.GetButtonDown(OuyaSDK.KeyEnum.BUTTON_A, controllerIndex))
		{
			rigidbody.AddForce(new Vector3(0f, 800f, 0f));
		}
		#endregion
	}
}
