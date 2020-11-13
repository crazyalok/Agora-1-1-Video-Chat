using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Permissions : MonoBehaviour
{
    private ArrayList permissionList = new ArrayList();
    void Awake()
    {
        permissionList.Add(Permission.Microphone);
        permissionList.Add(Permission.Camera);
    }
    void Update()
    {
        CheckPermissions();
    }
    private void CheckPermissions()
    {
        foreach (string permission in permissionList)
        {
            if (!Permission.HasUserAuthorizedPermission(permission))
            {
                Permission.RequestUserPermission(permission);
            }
        }
    }
}
