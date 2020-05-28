using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClickReceiver
{
    // out-> 콜바이 레퍼런스. 레이를쐈을 시 성공유무를 리턴하고, 성공 정보 자체는 hits에 값이 담김.
    public static bool GetClickedObjectsToArray(out RaycastHit[] hits, int layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        hits = Physics.RaycastAll(ray, 100f, 1<<layerMask);

        if (hits.Length == 0)
            return false;
        else
            return true;
    }

    public static bool GetClickedObject(out RaycastHit hitInfo, int layerMask)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out hitInfo, 100f, layerMask);
        
    }
    public static bool GetClickedObject(out RaycastHit hitInfo)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        return Physics.Raycast(ray, out hitInfo, 100f);
    }
}