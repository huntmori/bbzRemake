using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using API.VO;
using UnityEngine.SceneManagement;


public class indexDAO : MonoBehaviour
{
    [Header("Login Panel")]
    public InputField txtAccount;
    public InputField txtPassword;

    [Header("Account Create")]
    public InputField txtNewAccount;
    public InputField txtNewPassword;

    public Text txtMessage;

    public enum indexState { login = 1 , create=2 };

    public indexState state;

    public string serverUrl;

    public string loginUrl = "/account/login";
    public string createAccountUrl = "/account/create";

    // Start is called before the first frame update
    void Start()
    {
        serverUrl = "https://bbzclient.run.goorm.io";
        //serverUrl = "http://localhost:8080";
    }


    public void btnLogin()
    {

        if (txtAccount.text == "")
        {
            txtMessage.text = "계정명을 입력해 주세요.";
            return;
        }
        if (txtPassword.text == "") 
        {
            txtMessage.text = "비밀번호를 입력해 주세요.";
            return;
        }
        
        else
        {
            StartCoroutine("coroutinLogin");
        }
    }

    public void btnCreateAccount()
    {
        if (txtAccount.text == "")
        {
            txtMessage.text = "계정명을 입력해 주세요.";
            return;
        }
        if (txtPassword.text == "")
        {
            txtMessage.text = "비밀번호를 입력해 주세요.";
            return;
        }
        
        else
        {
            StartCoroutine("coroutineCreate");
        }

    }

    public void btnTest()
    {
        Debug.Log("btnTest clicked");
        StartCoroutine("test");
    }

    IEnumerator test()
    {
        /*List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("account_name=saddummy&password=1234"));

        UnityWebRequest www = UnityWebRequest.Post(serverUrl + "/account/body_test", formData);

        yield return www.SendWebRequest();

        if(www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("form success :"+ www.responseCode+"\t");
        }*/

        LoginRequestVO param = new LoginRequestVO();
        param.account_name = "saddummy";
        param.password = "1234";

        Util.JsonWebRequest wrapper = new Util.JsonWebRequest(serverUrl + "/account/body_test/", "POST", param);
        UnityWebRequest request = wrapper.req;

        //request.SendWebRequest();
        yield return request.SendWebRequest();

        //Debug.Log("Status_code:" + request.responseCode);

        if( request.isHttpError || request.isNetworkError)
        {
            //Debug.Log("error"+request.error);

        }
        else
        {
            //Debug.Log("success");
            //Debug.Log(request.downloadHandler.text);
        }
    }

    IEnumerator coroutinLogin()
    {
        //Debug.Log(txtAccount.text);
        //Debug.Log(txtPassword.text);

        LoginRequestVO param = new LoginRequestVO();
        param.account_name = txtAccount.text;
        param.password = txtPassword.text;


        Util.JsonWebRequest wrapper = new Util.JsonWebRequest(serverUrl + "/account/login/", "POST", param);
        UnityWebRequest request = wrapper.req;
        //request.SendWebRequest();
        yield return request.SendWebRequest();

//        Debug.Log("Status_code:" + request.responseCode);

        if (request.responseCode == 200)
        {
            txtMessage.text = "로그인에 성공하였습니다";
            SceneManager.LoadScene("00001.Loby");            
        }
        else
        {
            txtMessage.text = "로그인에 실패하였습니다";
        }
        
    }

    IEnumerator coroutineCreate()
    {
        //Debug.Log(txtAccount.text);
        //Debug.Log(txtPassword.text);

        LoginRequestVO param = new LoginRequestVO();
        param.account_name = txtAccount.text;
        param.password = txtPassword.text;

        Util.JsonWebRequest wrapper = new Util.JsonWebRequest(serverUrl + "/account/create/", "POST", param);
        UnityWebRequest request = wrapper.req;
        //request.SendWebRequest();
        yield return request.SendWebRequest();

        //Debug.Log("Status_code:" + request.responseCode);

        if(request.responseCode == 201)
        {
            txtMessage.text = "계정생성에 성공하였습니다.";
        }
        else
        {
            txtMessage.text = "계정생성에 실패하였습니다.";
        }
    }

    public void btnTest2()
    {
        StartCoroutine("coroutineCreateByMapper");
    }

    IEnumerator coroutineCreateByMapper()
    {
        Debug.Log(txtAccount.text);
        Debug.Log(txtPassword.text);

        LoginRequestVO param = new LoginRequestVO();
        param.account_name = txtAccount.text;
        param.password = txtPassword.text;
        Debug.Log("before json:" + param);
        Debug.Log("To Json String:" + JsonUtility.ToJson(param));

        WWWForm formData = new WWWForm();
        formData.AddField("account_name", param.account_name);
        formData.AddField("password", param.password);

        //UnityWebRequest request = UnityWebRequest.Post(serverUrl + loginUrl, JsonUtility.ToJson(param));
        Debug.Log("URL:" + serverUrl + loginUrl + "?account_name=" + param.account_name + "&password=" + param.password);
        UnityWebRequest request = UnityWebRequest.Post(serverUrl + "/account/create?account_name=" + param.account_name + "&password=" + param.password, "");
        Debug.Log(request);
        request.SetRequestHeader("Content-Type", "application/json");

        //request.SendWebRequest();
        yield return request.SendWebRequest();

        Debug.Log("Status_code:" + request.responseCode);

        if (request.responseCode == 201)
        {
            txtMessage.text = "계정생성에 성공하였습니다.";
        }
        else
        {
            txtMessage.text = "계정생성에 실패하였습니다.";
        }
    }
}
