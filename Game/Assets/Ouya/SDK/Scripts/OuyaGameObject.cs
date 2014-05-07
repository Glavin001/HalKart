/*
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

#if !UNITY_WP8
using OuyaSDK_LitJson;
#endif

using System.Collections.Generic;
using UnityEngine;

public class OuyaGameObject : MonoBehaviour
{
    #region Public Visible Variables

    public string DEVELOPER_ID = "310a8f51-4d6e-4ae5-bda0-b93878e5f5d0";
    public bool debugOff = false;

    [@HideInInspector]
    private static string m_inputData = null;
    public static string InputData{
        get{
            return m_inputData;
        }
        set{
            m_inputData = value;
        }
    }
    #endregion

    #region Private Variables
    private static OuyaGameObject m_instance = null;
#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3
    private bool m_sentMenuButtonUp = false;
#endif
    #endregion

    #region Singleton Accessor Class
    /// <summary>
    /// Singleton interface
    /// </summary>
    public static OuyaGameObject Singleton
    {
        get
        {
            if (null == m_instance)
            {
                GameObject ouyaGameObject = GameObject.Find("OuyaGameObject");
                if (ouyaGameObject)
                {
                    m_instance = ouyaGameObject.GetComponent<OuyaGameObject>();
                }
            }
            return m_instance;
        }
    }
    #endregion 
     
    #region Java To Unity Event Handlers

    public void onMenuButtonUp(string ignore)
    {
#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3
        if (m_sentMenuButtonUp)
        {
            return;
        }
        m_sentMenuButtonUp = true;
#endif

        //Debug.Log("OuyaGameObject: onMenuButtonUp");
        foreach (OuyaSDK.IMenuButtonUpListener listener in OuyaSDK.getMenuButtonUpListeners())
        {
            //Debug.Log("OuyaGameObject: Invoke OuyaMenuButtonUp");
            listener.OuyaMenuButtonUp();
        }
    }

    public void onMenuAppearing(string ignore)
    {
        //Debug.Log("onMenuAppearing");
        foreach (OuyaSDK.IMenuAppearingListener listener in OuyaSDK.getMenuAppearingListeners())
        {
            listener.OuyaMenuAppearing();
        }
    }

    public void onPause()
    {
        //Debug.Log("onPause");
        foreach (OuyaSDK.IPauseListener listener in OuyaSDK.getPauseListeners())
        {
            listener.OuyaOnPause();
        }
    }

    public void onResume()
    {
        //Debug.Log("onResume");
        foreach (OuyaSDK.IResumeListener listener in OuyaSDK.getResumeListeners())
        {
            listener.OuyaOnResume();
        }
    }

    #endregion
    
    #region JSON Data Listeners

    public void FetchGamerInfoSuccessListener(string jsonData)
    {
#if !UNITY_WP8
        //Debug.Log(string.Format("FetchGamerInfoSuccessListener jsonData={0}", jsonData));
        OuyaSDK.GamerInfo gamerInfo = JsonMapper.ToObject<OuyaSDK.GamerInfo>(jsonData);
        InvokeOuyaFetchGamerInfoOnSuccess(gamerInfo.uuid, gamerInfo.username);
#endif
    }
    public void FetchGamerInfoFailureListener(string jsonData)
    {
        Debug.LogError(string.Format("FetchGamerInfoFailureListener jsonData={0}", jsonData));
        InvokeOuyaFetchGamerInfoOnFailure(0, jsonData);
    }
    public void FetchGamerInfoCancelListener(string ignore)
    {
        InvokeOuyaFetchGamerInfoOnCancel();
    }

    private List<OuyaSDK.Product> m_products = new List<OuyaSDK.Product>();

    public void ProductListClearListener(string ignore)
    {
        m_products.Clear();
    }
    public void ProductListListener(string jsonData)
    {
#if !UNITY_WP8
        if (string.IsNullOrEmpty(jsonData))
        {
            Debug.Log("OuyaSDK.ProductListListener: received empty jsondata");
            return;
        }

        Debug.Log(string.Format("OuyaSDK.ProductListListener: jsonData={0}", jsonData));
        OuyaSDK.Product product = JsonMapper.ToObject<OuyaSDK.Product>(jsonData);
        m_products.Add(product);
#endif
    }
    public void ProductListFailureListener(string jsonData)
    {
        Debug.LogError(string.Format("ProductListFailureListener jsonData={0}", jsonData));
        InvokeOuyaGetReceiptsOnFailure(0, jsonData);
    }
    public void ProductListCompleteListener(string ignore)
    {
        foreach (OuyaSDK.Product product in m_products)
        {
            Debug.Log(string.Format("ProductListCompleteListener Product id={0} name={1} price={2}",
                product.identifier, product.name, product.priceInCents));
        }
        InvokeOuyaGetProductsOnSuccess(m_products);
    }

    public void PurchaseSuccessListener(string jsonData)
    {
#if !UNITY_WP8
        Debug.Log(string.Format("PurchaseSuccessListener jsonData={0}", jsonData));
        OuyaSDK.Product product = JsonMapper.ToObject<OuyaSDK.Product>(jsonData);
        InvokeOuyaPurchaseOnSuccess(product);
#endif
    }
    public void PurchaseFailureListener(string jsonData)
    {
        Debug.LogError(string.Format("PurchaseFailureListener jsonData={0}", jsonData));
        InvokeOuyaPurchaseOnFailure(0, jsonData);
    }
    public void PurchaseCancelListener(string ignore)
    {
        InvokeOuyaPurchaseOnCancel();
    }
    
    private List<OuyaSDK.Receipt> m_receipts = new List<OuyaSDK.Receipt>();

    public void ReceiptListClearListener(string ignore)
    {
        m_receipts.Clear();
    }
    public void ReceiptListListener(string jsonData)
    {
#if !UNITY_WP8
        if (string.IsNullOrEmpty(jsonData))
        {
            Debug.Log("OuyaSDK.ReceiptListListener: received empty jsondata");
            return;
        }

        Debug.Log(string.Format("OuyaSDK.ReceiptListListener: jsonData={0}", jsonData));
        OuyaSDK.Receipt receipt = JsonMapper.ToObject<OuyaSDK.Receipt>(jsonData);
        m_receipts.Add(receipt);
#endif
    }
    public void ReceiptListCompleteListener(string ignore)
    {
        foreach (OuyaSDK.Receipt receipt in m_receipts)
        {
            Debug.Log(string.Format("ReceiptListCompleteListener Receipt id={0} price={1} purchaseDate={2} generatedDate={3}",
                receipt.identifier,
                receipt.priceInCents,
                receipt.purchaseDate,
                receipt.generatedDate));
        }
        InvokeOuyaGetReceiptsOnSuccess(m_receipts);
    }
    public void ReceiptListFailureListener(string jsonData)
    {
        Debug.LogError(string.Format("ReceiptListFailureListener jsonData={0}", jsonData));
        InvokeOuyaGetReceiptsOnFailure(0, jsonData);
    }
    public void ReceiptListCancelListener(string ignore)
    {
        InvokeOuyaGetReceiptsOnCancel();
    }
    
    #endregion

    #region UNITY Awake, Start & Update
    void Awake()
    {
        m_instance = this;
    }
    void Start()
    {
        Input.ResetInputAxes();
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(transform.gameObject);

        #region Init OUYA

        try
        {
            //Initialize OuyaSDK with your developer ID
            //Get your developer_id from the ouya developer portal @ http://developer.ouya.tv
            OuyaSDK.initialize(DEVELOPER_ID);
        }
        catch (System.Exception ex)
        {
            Debug.LogError(string.Format("Failed to initialize OuyaSDK exception={0}", ex));
        }

        #endregion
    }
    #endregion

    #region IAP

    public void RequestUnityAwake(string ignore)
    {
        OuyaSDK.initialize(DEVELOPER_ID);
    }

    public void SendIAPInitComplete(string ignore)
    {
        OuyaSDK.setIAPInitComplete();
    }

    public void InvokeOuyaFetchGamerInfoOnSuccess(string uuid, string username)
    {
        foreach (OuyaSDK.IFetchGamerInfoListener listener in OuyaSDK.getFetchGamerInfoListeners())
        {
            if (null != listener)
            {
                listener.OuyaFetchGamerInfoOnSuccess(uuid, username);
            }
        }
    }

    public void InvokeOuyaFetchGamerInfoOnFailure(int errorCode, string errorMessage)
    {
        Debug.LogError(string.Format("InvokeOuyaFetchGamerInfoOnFailure: error={0} errorMessage={1}", errorCode, errorMessage));
        foreach (OuyaSDK.IFetchGamerInfoListener listener in OuyaSDK.getFetchGamerInfoListeners())
        {
            if (null != listener)
            {
                listener.OuyaFetchGamerInfoOnFailure(errorCode, errorMessage);
            }
        }
    }

    public void InvokeOuyaFetchGamerInfoOnCancel()
    {
        //Debug.Log("InvokeOuyaFetchGamerInfoOnCancel");
        foreach (OuyaSDK.IFetchGamerInfoListener listener in OuyaSDK.getFetchGamerInfoListeners())
        {
            if (null != listener)
            {
                listener.OuyaFetchGamerInfoOnCancel();
            }
        }
    }

    public void InvokeOuyaGetProductsOnSuccess(List<OuyaSDK.Product> products)
    {
        foreach (OuyaSDK.IGetProductsListener listener in OuyaSDK.getGetProductsListeners())
        {
            if (null != listener)
            {
                listener.OuyaGetProductsOnSuccess(products);
            }
        }
    }

    public void InvokeOuyaGetProductsOnFailure(int errorCode, string errorMessage)
    {
        Debug.LogError(string.Format("InvokeOuyaGetProductsOnFailure: error={0} errorMessage={1}", errorCode, errorMessage));
        foreach (OuyaSDK.IGetProductsListener listener in OuyaSDK.getGetProductsListeners())
        {
            if (null != listener)
            {
                listener.OuyaGetProductsOnFailure(errorCode, errorMessage);
            }
        }
    }

    public void InvokeOuyaPurchaseOnSuccess(OuyaSDK.Product product)
    {
        foreach (OuyaSDK.IPurchaseListener listener in OuyaSDK.getPurchaseListeners())
        {
            if (null != listener)
            {
                listener.OuyaPurchaseOnSuccess(product);
            }
        }
    }

    public void InvokeOuyaPurchaseOnFailure(int errorCode, string errorMessage)
    {
        Debug.LogError(string.Format("InvokeOuyaPurchaseOnFailure: error={0} errorMessage={1}", errorCode, errorMessage));
        foreach (OuyaSDK.IPurchaseListener listener in OuyaSDK.getPurchaseListeners())
        {
            if (null != listener)
            {
                listener.OuyaPurchaseOnFailure(errorCode, errorMessage);
            }
        }
    }

    public void InvokeOuyaPurchaseOnCancel()
    {
        //Debug.Log("InvokeOuyaPurchaseOnCancel");
        foreach (OuyaSDK.IPurchaseListener listener in OuyaSDK.getPurchaseListeners())
        {
            if (null != listener)
            {
                listener.OuyaPurchaseOnCancel();
            }
        }
    }

    public void InvokeOuyaGetReceiptsOnSuccess(List<OuyaSDK.Receipt> receipts)
    {
        foreach (OuyaSDK.IGetReceiptsListener listener in OuyaSDK.getGetReceiptsListeners())
        {
            if (null != listener)
            {
                listener.OuyaGetReceiptsOnSuccess(receipts);
            }
        }
    }

    public void InvokeOuyaGetReceiptsOnFailure(int errorCode, string errorMessage)
    {
        Debug.LogError(string.Format("InvokeOuyaGetReceiptsOnFailure: error={0} errorMessage={1}", errorCode, errorMessage));
        foreach (OuyaSDK.IGetReceiptsListener listener in OuyaSDK.getGetReceiptsListeners())
        {
            if (null != listener)
            {
                listener.OuyaGetReceiptsOnFailure(errorCode, errorMessage);
            }
        }
    }

    public void InvokeOuyaGetReceiptsOnCancel()
    {
        //Debug.Log("InvokeOuyaGetReceiptsOnCancel");
        foreach (OuyaSDK.IGetReceiptsListener listener in OuyaSDK.getGetReceiptsListeners())
        {
            if (null != listener)
            {
                listener.OuyaGetReceiptsOnCancel();
            }
        }
    }

    #endregion

    #region Controllers

#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3

    public void Update()
    {
        Event e = Event.current;
        if (null == e)
        {
            return;
        }
        
        if (e.isKey &&
            e.keyCode == KeyCode.Menu &&
            e.type == EventType.KeyUp)
        {
            onMenuButtonUp(string.Empty);
            e.Use();
        }
    }

    public void LateUpdate()
    {
        m_sentMenuButtonUp = false;
    }

#endif

    private void FixedUpdate()
    {
        OuyaSDK.UpdateJoysticks();
    }

    #endregion

    #region Debug Logs from Java
    public void DebugLog(string message)
    {
        Debug.Log(message);
    }

    public void DebugLogError(string message)
    {
        Debug.LogError(message);
    }
    #endregion

}