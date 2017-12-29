using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MiniJSON;

public enum GoogleDataDirection
{
    ROWS =0,
    COLUMNS=1,
}

public class GoogleSheet
{
    public string sheet;
    public string key;

    public GoogleSheet(string sheet,string key)
    {
        this.sheet = sheet;
        this.key = key;
    }
}

public class GoogleDataManager
{
    private static string sSpreadSheetUrl = "https://sheets.googleapis.com/v4/spreadsheets/";

    public static void RequestData(GoogleSheet sheetData, string range, GoogleDataDirection direction, System.Action<bool, string, Dictionary<string,object>> callback,bool showResponse = false)
    {
        var url = GetRangeValueURL(sheetData, direction, range);

        if(showResponse)
        {
            Debug.LogWarning(url);
        }

        EditorWWW.RequestSync(url, (bool success, string response) => 
        {
            Dictionary<string, object> data = null;
            if (success)
            {
                if(showResponse)
                {
                    Debug.LogWarning(response);
                }

                data = Json.Deserialize(response) as Dictionary<string, object>;
            }
            else
            {
                Debug.LogError("[Error] Request Data Range: "+ range + " Response: " + response);
            }

            callback.SafeInvoke(success, range, data);
        });
    }

    private static string GetRangeValueURL(GoogleSheet sheetData, GoogleDataDirection direction, string range)
    {
        return sSpreadSheetUrl + sheetData.sheet + "/values/" + range + "?key=" + sheetData.key + "&majorDimension=" + direction;
    }

}
