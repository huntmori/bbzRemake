using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Networking;

namespace Util
{
    public class JsonWebRequest
    {
        public UnityWebRequest req;

        public JsonWebRequest()
        {
            req = new UnityWebRequest();
        }

        public JsonWebRequest(string url, string method, System.Object data)
        {
            req = UnityWebRequest.Post(url, method);
            setUploadHandler(setJsonData(data));
            setDownloadHandler();
            setJsonRequestHeader();
        }

        public byte[] setJsonData(System.Object parameter)
        {
            string jsonString = JsonUtility.ToJson(parameter);
            byte[] jsonToByteArray = new System.Text.UTF8Encoding().GetBytes(jsonString);
            return jsonToByteArray;
        }

        public void setUploadHandler(byte[] data)
        {
            req.uploadHandler = (UploadHandler)new UploadHandlerRaw(data);
        }

        public void setDownloadHandler()
        {
            req.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        }

        public void setJsonRequestHeader()
        {
            req.SetRequestHeader("COntent-Type", "application/json");
        }
    }

}