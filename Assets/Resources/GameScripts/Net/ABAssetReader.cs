﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABAssetReader : AssetReader
{
    private AssetBundleCreateRequest assetBundleCreateRequest;
    private Dictionary<string, List<EVENT_DEL_VOID_ASSETBOX>> waitingDict;
    private List<EVENT_DEL_VOID_ASSETBOX> eventList;

    public ABAssetReader()
    {
        waitingDict = new Dictionary<string, List<EVENT_DEL_VOID_ASSETBOX>>();
    }

    public override AssetBox LoadSync(string _assetBundleName)
    {
        if (Has(_assetBundleName))
            return Get(_assetBundleName);

        assetBundle = AssetBundle.LoadFromFile(GlobalValues.GET_AB_LOAD_PATH + _assetBundleName);
        assetBox.name = Utils.AssetBundleNameToAssetName(assetBundle.name);
        assetBox.assetBundleName = assetBundle.name;
        assetBox.assetBundle = assetBundle;
        if (!assetBundle.isStreamedSceneAssetBundle)
            assetBox.obj = assetBundle.LoadAsset(assetBox.name);
        Add(assetBox);

        return assetBox;
    }

    public override void LoadAsync(string _assetBundleName, EVENT_DEL_VOID_ASSETBOX _callback)
    {
        if (Has(_assetBundleName))
        {
            _callback(Get(_assetBundleName));
        }
        else if (waitingDict.ContainsKey(_assetBundleName))
        {
            eventList = waitingDict[_assetBundleName];
            eventList.Add(_callback);
        }
        else
        {
            eventList = new List<EVENT_DEL_VOID_ASSETBOX>
            {
                _callback
            };
            waitingDict.Add(_assetBundleName, eventList);

            assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(GlobalValues.GET_AB_LOAD_PATH + _assetBundleName);
            assetBundleCreateRequest.completed += LoadAsyncFinishUnpack;
        }
    }

    public void LoadAsyncFinishUnpack(AsyncOperation _ao)
    {
        _ao.completed -= LoadAsyncFinishUnpack;
        assetBundleCreateRequest = _ao as AssetBundleCreateRequest;

        assetBundle = assetBundleCreateRequest.assetBundle;
        assetBox.name = Utils.AssetBundleNameToAssetName(assetBundle.name);
        assetBox.assetBundleName = assetBundle.name;
        assetBox.assetBundle = assetBundle;
        if (!assetBundle.isStreamedSceneAssetBundle)
            assetBox.obj = assetBundle.LoadAsset(assetBox.name);
        Add(assetBox);

        LoadAsyncFinishNotice(assetBox);
    }

    public void LoadAsyncFinishNotice(AssetBox _assetBox)
    {
        eventList = waitingDict[_assetBox.assetBundleName];
        waitingDict.Remove(_assetBox.assetBundleName);

        foreach (EVENT_DEL_VOID_ASSETBOX even in eventList)
        {
            even(_assetBox);
        }
    }
}
