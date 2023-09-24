using UnityEngine;

namespace Actions.CubeActions
{
    public class CubeCanvas : MonoBehaviour
    {
        void Update()
        {
            transform.eulerAngles = new Vector3(
                90,
                0,
                0
            );
        }
    }
}
