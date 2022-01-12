using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static Vector3 Wrap(Vector3 v, Vector3 min, Vector3 max)
    {
        Vector3 result = v;

        if (result.x > max.x + 1) result.x = min.x - 1;
        if (result.x < min.x - 1) result.x = max.x + 1;
        
        if (result.y > max.y + 1) result.y = min.y - 1;
        if (result.y < min.y - 1) result.y = max.y + 1;

        if (result.z > max.z + 1) result.z = min.z - 1;
        if (result.z < min.z - 1) result.z = max.z + 1;

        return result;
    }

}
