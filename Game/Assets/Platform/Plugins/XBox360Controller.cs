﻿/*
 * Copyright (C) 2012, 2013 OUYA, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using UnityEngine;

public class XBox360Controller : OuyaControllerCommon,
    IOuyaController
{
    readonly string[] m_supportedJoysticks =
        {
            "CONTROLLER (AFTERGLOW GAMEPAD FOR XBOX 360)",
            "CONTROLLER (ROCK CANDY GAMEPAD FOR XBOX 360)",
            "CONTROLLER (XBOX 360 WIRELESS RECEIVER FOR WINDOWS)",
            "MICROSOFT X-BOX 360 PAD",
            "CONTROLLER (XBOX 360 FOR WINDOWS)",
            "CONTROLLER (XBOX360 GAMEPAD)",
            "XBOX 360 FOR WINDOWS (CONTROLLER)",
            "MICROSOFT WIRELESS 360 CONTROLLER",
        };

    public string[] GetSupportedJoysicks()
    {
        return m_supportedJoysticks;
    }

    public OuyaSDK.KeyEnum[] GetSupportedAxises()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
                return new OuyaSDK.KeyEnum[]
                {
                        OuyaSDK.KeyEnum.AXIS_LSTICK_X,
                        OuyaSDK.KeyEnum.AXIS_LSTICK_Y,
                        OuyaSDK.KeyEnum.AXIS_RSTICK_X,
                        OuyaSDK.KeyEnum.AXIS_RSTICK_Y,
                        OuyaSDK.KeyEnum.BUTTON_LT,
                        OuyaSDK.KeyEnum.BUTTON_RT,
                        OuyaSDK.KeyEnum.BUTTON_DPAD_LEFT,
                        OuyaSDK.KeyEnum.BUTTON_DPAD_RIGHT,
                        OuyaSDK.KeyEnum.BUTTON_DPAD_UP,
                        OuyaSDK.KeyEnum.BUTTON_DPAD_DOWN,
                   };
#else
        if (Application.platform == RuntimePlatform.OSXEditor)
        {
            return new OuyaSDK.KeyEnum[]
                   {
                       OuyaSDK.KeyEnum.AXIS_LSTICK_X,
                       OuyaSDK.KeyEnum.AXIS_LSTICK_Y,
                       OuyaSDK.KeyEnum.AXIS_RSTICK_X,
                       OuyaSDK.KeyEnum.AXIS_RSTICK_Y,
                       OuyaSDK.KeyEnum.BUTTON_LT,
                       OuyaSDK.KeyEnum.BUTTON_RT,
                   };
        }
        else
        {
            return new OuyaSDK.KeyEnum[]
                   {
                       OuyaSDK.KeyEnum.AXIS_LSTICK_X,
                       OuyaSDK.KeyEnum.AXIS_LSTICK_Y,
                       OuyaSDK.KeyEnum.AXIS_RSTICK_X,
                       OuyaSDK.KeyEnum.AXIS_RSTICK_Y,
                       OuyaSDK.KeyEnum.BUTTON_LT,
                       OuyaSDK.KeyEnum.BUTTON_RT,
                       OuyaSDK.KeyEnum.BUTTON_DPAD_LEFT,
                       OuyaSDK.KeyEnum.BUTTON_DPAD_RIGHT,
                       OuyaSDK.KeyEnum.BUTTON_DPAD_UP,
                       OuyaSDK.KeyEnum.BUTTON_DPAD_DOWN,
                   };
        }
#endif
    }

    public OuyaSDK.KeyEnum[] GetSupportedButtons()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
                return new OuyaSDK.KeyEnum[]
                {
                    OuyaSDK.KeyEnum.BUTTON_LB,
                    OuyaSDK.KeyEnum.BUTTON_RB,
                    OuyaSDK.KeyEnum.BUTTON_O,
                    OuyaSDK.KeyEnum.BUTTON_U,
                    OuyaSDK.KeyEnum.BUTTON_Y,
                    OuyaSDK.KeyEnum.BUTTON_A,
                    OuyaSDK.KeyEnum.BUTTON_L3,
                    OuyaSDK.KeyEnum.BUTTON_R3,
                };
#else
        if (Application.platform == RuntimePlatform.OSXEditor)
        {
            return new OuyaSDK.KeyEnum[]
                   {
                       OuyaSDK.KeyEnum.BUTTON_LB,
                       OuyaSDK.KeyEnum.BUTTON_RB,
                       OuyaSDK.KeyEnum.BUTTON_O,
                       OuyaSDK.KeyEnum.BUTTON_U,
                       OuyaSDK.KeyEnum.BUTTON_Y,
                       OuyaSDK.KeyEnum.BUTTON_A,
                       OuyaSDK.KeyEnum.BUTTON_L3,
                       OuyaSDK.KeyEnum.BUTTON_R3,
                       OuyaSDK.KeyEnum.BUTTON_DPAD_UP,
                       OuyaSDK.KeyEnum.BUTTON_DPAD_DOWN,
                       OuyaSDK.KeyEnum.BUTTON_DPAD_LEFT,
                       OuyaSDK.KeyEnum.BUTTON_DPAD_RIGHT,
                   };
        }
        else
        {
            return new OuyaSDK.KeyEnum[]
                   {
                       OuyaSDK.KeyEnum.BUTTON_LB,
                       OuyaSDK.KeyEnum.BUTTON_RB,
                       OuyaSDK.KeyEnum.BUTTON_O,
                       OuyaSDK.KeyEnum.BUTTON_U,
                       OuyaSDK.KeyEnum.BUTTON_Y,
                       OuyaSDK.KeyEnum.BUTTON_A,
                       OuyaSDK.KeyEnum.BUTTON_L3,
                       OuyaSDK.KeyEnum.BUTTON_R3,
                       OuyaSDK.KeyEnum.BUTTON_DPAD_UP
                   };
        }
#endif
    }

    public bool HasAxis(OuyaSDK.KeyEnum keyCode)
    {
        OuyaSDK.KeyEnum[] axises = GetSupportedAxises();
        return HasKeyCode(axises, keyCode);
    }

    public bool HasButton(OuyaSDK.KeyEnum keyCode)
    {
        OuyaSDK.KeyEnum[] buttons = GetSupportedButtons();
        return HasKeyCode(buttons, keyCode);
    }

    public bool GetAxisInverted(OuyaSDK.KeyEnum keyCode)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        switch (keyCode)
        {
            case OuyaSDK.KeyEnum.AXIS_LSTICK_X:
                return false;
            case OuyaSDK.KeyEnum.AXIS_LSTICK_Y:
                return false;
            case OuyaSDK.KeyEnum.AXIS_RSTICK_X:
                return false;
            case OuyaSDK.KeyEnum.AXIS_RSTICK_Y:
                return false;
            case OuyaSDK.KeyEnum.BUTTON_LT:
                return false;
            case OuyaSDK.KeyEnum.BUTTON_RT:
                return false;
            default:
                return false;
        }
#else
        switch (keyCode)
        {
            case OuyaSDK.KeyEnum.AXIS_LSTICK_X:
                return false;
            case OuyaSDK.KeyEnum.AXIS_LSTICK_Y:
                return false;
            case OuyaSDK.KeyEnum.AXIS_RSTICK_X:
                return false;
            case OuyaSDK.KeyEnum.AXIS_RSTICK_Y:
                return false;
            case OuyaSDK.KeyEnum.BUTTON_LT:
                return false;
            case OuyaSDK.KeyEnum.BUTTON_RT:
                return false;
            default:
                return false;
        }
#endif
    }

    public string GetUnityAxisName(OuyaSDK.KeyEnum keyCode, OuyaSDK.OuyaPlayer player)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        switch (keyCode)
        {
            case OuyaSDK.KeyEnum.AXIS_LSTICK_X:
                return string.Format("Joy{0} Axis 1", (int)player);
            case OuyaSDK.KeyEnum.AXIS_LSTICK_Y:
                return string.Format("Joy{0} Axis 2", (int)player);
            case OuyaSDK.KeyEnum.AXIS_RSTICK_X:
                return string.Format("Joy{0} Axis 3", (int)player);
            case OuyaSDK.KeyEnum.AXIS_RSTICK_Y:
                return string.Format("Joy{0} Axis 4", (int)player);
            case OuyaSDK.KeyEnum.BUTTON_LT:
                return string.Format("Joy{0} Axis 7", (int)player);
            case OuyaSDK.KeyEnum.BUTTON_RT:
                return string.Format("Joy{0} Axis 8", (int)player);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_LEFT:
                return string.Format("Joy{0} Axis 5", (int)player);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_RIGHT:
                return string.Format("Joy{0} Axis 5", (int)player);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_UP:
                return string.Format("Joy{0} Axis 6", (int)player);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_DOWN:
                return string.Format("Joy{0} Axis 6", (int)player);
            default:
                return string.Empty;
        }

#else
        if (Application.platform == RuntimePlatform.OSXEditor)
        {
            switch (keyCode)
            {
                case OuyaSDK.KeyEnum.AXIS_LSTICK_X:
                    return string.Format("Joy{0} Axis 1", (int)player);
                case OuyaSDK.KeyEnum.AXIS_LSTICK_Y:
                    return string.Format("Joy{0} Axis 2", (int)player);
                case OuyaSDK.KeyEnum.AXIS_RSTICK_X:
                    return string.Format("Joy{0} Axis 3", (int)player);
                case OuyaSDK.KeyEnum.AXIS_RSTICK_Y:
                    return string.Format("Joy{0} Axis 4", (int)player);
                case OuyaSDK.KeyEnum.BUTTON_LT:
                    return string.Format("Joy{0} Axis 5", (int)player);
                case OuyaSDK.KeyEnum.BUTTON_RT:
                    return string.Format("Joy{0} Axis 6", (int)player);
                default:
                    return string.Empty;
            }
        }
        else
        {
            switch (keyCode)
            {
                case OuyaSDK.KeyEnum.AXIS_LSTICK_X:
                    return string.Format("Joy{0} Axis 1", (int)player);
                case OuyaSDK.KeyEnum.AXIS_LSTICK_Y:
                    return string.Format("Joy{0} Axis 2", (int)player);
                case OuyaSDK.KeyEnum.AXIS_RSTICK_X:
                    return string.Format("Joy{0} Axis 4", (int)player);
                case OuyaSDK.KeyEnum.AXIS_RSTICK_Y:
                    return string.Format("Joy{0} Axis 5", (int)player);
                case OuyaSDK.KeyEnum.BUTTON_LT:
                    return string.Format("Joy{0} Axis 9", (int)player);
                case OuyaSDK.KeyEnum.BUTTON_RT:
                    return string.Format("Joy{0} Axis 10", (int)player);
                case OuyaSDK.KeyEnum.BUTTON_DPAD_LEFT:
                    return string.Format("Joy{0} Axis 6", (int)player);
                case OuyaSDK.KeyEnum.BUTTON_DPAD_RIGHT:
                    return string.Format("Joy{0} Axis 6", (int)player);
                case OuyaSDK.KeyEnum.BUTTON_DPAD_UP:
                    return string.Format("Joy{0} Axis 7", (int)player);
                case OuyaSDK.KeyEnum.BUTTON_DPAD_DOWN:
                    return string.Format("Joy{0} Axis 7", (int)player);
                default:
                    return string.Empty;
            }
        }
        #endif
    }

    public KeyCode GetUnityKeyCode(OuyaSDK.KeyEnum keyCode, OuyaSDK.OuyaPlayer player)
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        switch (keyCode)
        {
            case OuyaSDK.KeyEnum.BUTTON_LB:
                return CommonGetUnityKeyCode(6, player);
            case OuyaSDK.KeyEnum.BUTTON_RB:
                return CommonGetUnityKeyCode(7, player);
            case OuyaSDK.KeyEnum.BUTTON_O:
                return CommonGetUnityKeyCode(0, player);
            case OuyaSDK.KeyEnum.BUTTON_U:
                return CommonGetUnityKeyCode(3, player);
            case OuyaSDK.KeyEnum.BUTTON_Y:
                return CommonGetUnityKeyCode(4, player);
            case OuyaSDK.KeyEnum.BUTTON_A:
                return CommonGetUnityKeyCode(1, player);
            case OuyaSDK.KeyEnum.BUTTON_L3:
                return CommonGetUnityKeyCode(13, player);
            case OuyaSDK.KeyEnum.BUTTON_R3:
                return CommonGetUnityKeyCode(14, player);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_UP:
                return (KeyCode)(-1);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_DOWN:
                return (KeyCode)(-1);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_LEFT:
                return (KeyCode)(-1);
            case OuyaSDK.KeyEnum.BUTTON_DPAD_RIGHT:
                return (KeyCode)(-1);
            case OuyaSDK.KeyEnum.BUTTON_LT:
                return (KeyCode)(-1);
            case OuyaSDK.KeyEnum.BUTTON_RT:
                return (KeyCode)(-1);
            default:
                return (KeyCode)(-1);
        }
#else
        if (Application.platform == RuntimePlatform.OSXEditor)
        {
            switch (keyCode)
            {
                case OuyaSDK.KeyEnum.BUTTON_LB:
                    return CommonGetUnityKeyCode(13, player);
                case OuyaSDK.KeyEnum.BUTTON_RB:
                    return CommonGetUnityKeyCode(14, player);
                case OuyaSDK.KeyEnum.BUTTON_O:
                    return CommonGetUnityKeyCode(16, player);
                case OuyaSDK.KeyEnum.BUTTON_U:
                    return CommonGetUnityKeyCode(18, player);
                case OuyaSDK.KeyEnum.BUTTON_Y:
                    return CommonGetUnityKeyCode(19, player);
                case OuyaSDK.KeyEnum.BUTTON_A:
                    return CommonGetUnityKeyCode(17, player);
                case OuyaSDK.KeyEnum.BUTTON_L3:
                    return CommonGetUnityKeyCode(11, player);
                case OuyaSDK.KeyEnum.BUTTON_R3:
                    return CommonGetUnityKeyCode(12, player);
                case OuyaSDK.KeyEnum.BUTTON_DPAD_UP:
                    return CommonGetUnityKeyCode(5, player);
                case OuyaSDK.KeyEnum.BUTTON_DPAD_DOWN:
                    return CommonGetUnityKeyCode(6, player);
                case OuyaSDK.KeyEnum.BUTTON_DPAD_LEFT:
                    return CommonGetUnityKeyCode(7, player);
                case OuyaSDK.KeyEnum.BUTTON_DPAD_RIGHT:
                    return CommonGetUnityKeyCode(8, player);
                case OuyaSDK.KeyEnum.BUTTON_LT:
                    return KeyCode.None;
                case OuyaSDK.KeyEnum.BUTTON_RT:
                    return KeyCode.None;
                default:
                    return KeyCode.None;
            }
        }
        else
        {
            switch (keyCode)
            {
                case OuyaSDK.KeyEnum.BUTTON_LB:
                    return CommonGetUnityKeyCode(4, player);
                case OuyaSDK.KeyEnum.BUTTON_RB:
                    return CommonGetUnityKeyCode(5, player);
                case OuyaSDK.KeyEnum.BUTTON_O:
                    return CommonGetUnityKeyCode(0, player);
                case OuyaSDK.KeyEnum.BUTTON_U:
                    return CommonGetUnityKeyCode(2, player);
                case OuyaSDK.KeyEnum.BUTTON_Y:
                    return CommonGetUnityKeyCode(3, player);
                case OuyaSDK.KeyEnum.BUTTON_A:
                    return CommonGetUnityKeyCode(1, player);
                case OuyaSDK.KeyEnum.BUTTON_L3:
                    return CommonGetUnityKeyCode(8, player);
                case OuyaSDK.KeyEnum.BUTTON_R3:
                    return CommonGetUnityKeyCode(9, player);
                case OuyaSDK.KeyEnum.BUTTON_DPAD_UP:
                    return KeyCode.None;
                case OuyaSDK.KeyEnum.BUTTON_DPAD_DOWN:
                    return KeyCode.None;
                case OuyaSDK.KeyEnum.BUTTON_DPAD_LEFT:
                    return KeyCode.None;
                case OuyaSDK.KeyEnum.BUTTON_DPAD_RIGHT:
                    return KeyCode.None;
                case OuyaSDK.KeyEnum.BUTTON_LT:
                    return KeyCode.None;
                case OuyaSDK.KeyEnum.BUTTON_RT:
                    return KeyCode.None;
                default:
                    return KeyCode.None;
            }
        }
#endif
    }
}