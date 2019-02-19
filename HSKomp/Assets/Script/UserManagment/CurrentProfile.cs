using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentProfile : MonoBehaviour
{

    [SerializeField] string profileName;

    void Update()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SetProfileName(string name)
    {
        profileName = name;
    }
    public string GetProfileName()
    {
        return profileName;
    }
}
