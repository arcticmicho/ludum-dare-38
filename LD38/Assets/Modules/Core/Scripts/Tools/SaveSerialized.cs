using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISerialized
{
    Dictionary<string, object> Serialize();
    void Deserialize(Dictionary<string, object> data);
}
