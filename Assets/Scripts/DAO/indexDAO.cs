using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class indexDAO : MonoBehaviour
{
    [Header("Login Panel")]
    public InputField txtAccount;
    public InputField txtPassword;

    [Header("Account Create")]
    public InputField txtNewAccount;
    public InputField txtNewPassword;

    public enum indexState { login = 1 , create=2 };

    public indexState state;

    public string serverUrl;

    public string loginUrl = "";
    public string createAccountUrl = "";

    // Start is called before the first frame update
    void Start()
    {
        serverUrl = "localhost:3000";
    }


    public void btnLogin()
    {
        StartCoroutine("coroutineLogin");
        Debug.Log(txtAccount.text);
        Debug.Log(txtPassword.text);
    }

    public void btnCreateAccount()
    {
        Debug.Log(txtNewAccount.text);
        Debug.Log(txtNewPassword.text);
    }

    IEnumerator coroutinLogin()
    {
        Debug.Log(txtAccount.text);
        Debug.Log(txtPassword.text);

        yield return null;
    }
}
