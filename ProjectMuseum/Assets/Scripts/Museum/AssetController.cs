using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssetController : MonoBehaviour
{
    public InfoTextAsset _assetData;

    [SerializeField] private TextMeshProUGUI _creator;

    [SerializeField] private TextMeshProUGUI _assetName;

    [SerializeField] private TextMeshProUGUI _assetInfo;

    public void SetText()
    {
        _creator.text = $"Creator: {_assetData.Creator}";
        _assetName.text = $"Asset: {_assetData.AssetName}";
        _assetInfo.text = $"Info: {_assetData.InfoText}";
    }
}
