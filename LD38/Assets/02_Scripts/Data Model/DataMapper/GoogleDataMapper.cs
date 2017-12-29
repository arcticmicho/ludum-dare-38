using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoogleDataMapper : MonoBehaviour
{
    [Header("Common")]
    [SerializeField][ReadOnly]
    private string _googleApiKey;

    [SerializeField][ReadOnly]
    private string _dataSpreadSheet;

    [SerializeField]
    private bool _showResponse;

    public string DataSpreadSheet
    {
        get { return _dataSpreadSheet; }
    }

    public string GoogleApiKey
    {
        get { return _googleApiKey; }
    }

    public bool ShowResponse
    {
        get { return _showResponse; }
    }
}
