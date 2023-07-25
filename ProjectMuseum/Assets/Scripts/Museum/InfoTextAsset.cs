using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "New InfoTextData", menuName = "AssetInfoText")]
public class InfoTextAsset : ScriptableObject
{
    public string Creator;

    public string AssetName;

    public string InfoText;
}
