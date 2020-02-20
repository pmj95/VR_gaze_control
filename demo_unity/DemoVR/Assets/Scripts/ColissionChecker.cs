using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ColissionChecker
{
    public static void checkCollision(Vector3 origin, Vector3 direction)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit))
        {
            GameObject currObject = hit.collider.gameObject;

            if (currObject.TryGetComponent<Slider>(out Slider slider))
            {
                slider.value = slider.value == 0 ? 1 : 0;
            }
            else if (currObject.TryGetComponent<GeneralButton>(out GeneralButton button))
            {
                button.DoAction();
            }
        }
    }
}
