using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using API.VO;

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

    public string loginUrl = "/account/login";
    public string createAccountUrl = "/account/create";

    // Start is called before the first frame update
    void Start()
    {
        serverUrl = "localhost:8001";
    }


    public void btnLogin()
    {
        StartCoroutine("coroutinLogin");
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

        LoginRequestVO param = new LoginRequestVO();
        param.account_name = txtAccount.text;
        param.password = txtPassword.text;
        Debug.Log("before json:" + param);
        Debug.Log("To Json String:"+JsonUtility.ToJson(param));

        WWWForm formData = new WWWForm();
        formData.AddField("account_name", param.account_name);
        formData.AddField("password", param.password);

        //UnityWebRequest request = UnityWebRequest.Post(serverUrl + loginUrl, JsonUtility.ToJson(param));
        Debug.Log("URL:" + serverUrl + loginUrl + "?account_name=" + param.account_name + "&password=" + param.password);
        UnityWebRequest request = UnityWebRequest.Post(serverUrl +  "/account/login?account_name="+param.account_name+"&password="+param.password, "");
        Debug.Log(request);
        request.SetRequestHeader("Content-Type", "application/json");

        //request.SendWebRequest();
        yield return request.SendWebRequest();

        Debug.Log("Status_code:" + request.responseCode);
        
    }
}
