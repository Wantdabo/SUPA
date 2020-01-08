using System.Collections;
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
        SetAssetBox(assetBundle);

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
            assetBundleCreateRequest.completed += LoadAsyncHandle;
        }
    }

    public void LoadAsyncHandle(AsyncOperation _ao)
    {
        _ao.completed -= LoadAsyncHandle;
        assetBundleCreateRequest = _ao as AssetBundleCreateRequest;
        assetBundle = assetBundleCreateRequest.assetBundle;
        LoadAsyncNotice(SetAssetBox(assetBundle));
    }

    public void LoadAsyncNotice(AssetBox _assetBox)
    {
        eventList = waitingDict[_assetBox.assetBundleName];
        waitingDict.Remove(_assetBox.assetBundleName);

        foreach (EVENT_DEL_VOID_ASSETBOX even in eventList)
        {
            even(_assetBox);
        }
        eventList = null;
    }

    private AssetBox SetAssetBox(AssetBundle _assetBundle)
    {
        assetBox.name = Utils.AssetBundleNameToAssetName(_assetBundle.name);
        assetBox.assetBundleName = _assetBundle.name;
        assetBox.assetBundle = _assetBundle;
        if (!assetBundle.isStreamedSceneAssetBundle)
            assetBox.obj = _assetBundle.LoadAsset(assetBox.name);
        Add(assetBox);

        return assetBox;
    }
}
