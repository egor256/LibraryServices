using UnityEngine;

namespace Assets
{
    public static class Utils
    {
        public static float ExpDist(float lambda_r)
        {
            float r = Random.value;
            float expDist = -Mathf.Log(r) / lambda_r;
            return expDist;
        }
    }
}
