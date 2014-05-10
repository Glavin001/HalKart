using UnityEngine;

public class MenuController : MonoBehaviour,
    OuyaSDK.IMenuButtonUpListener,
    OuyaSDK.IMenuAppearingListener,
    OuyaSDK.IPauseListener,
    OuyaSDK.IResumeListener
{
	public string nextLevel = "nextSceneAsText";

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

    void Update()
    {
		if (OuyaExampleCommon.GetButton(OuyaSDK.KeyEnum.BUTTON_O, OuyaSDK.OuyaPlayer.player1) ||
		    OuyaExampleCommon.GetButton(OuyaSDK.KeyEnum.BUTTON_O, OuyaSDK.OuyaPlayer.player2) ||
		    OuyaExampleCommon.GetButton(OuyaSDK.KeyEnum.BUTTON_O, OuyaSDK.OuyaPlayer.player3) ||
		    OuyaExampleCommon.GetButton(OuyaSDK.KeyEnum.BUTTON_O, OuyaSDK.OuyaPlayer.player4))
		{
			Application.LoadLevel(nextLevel);
		}
    }
}