using System;
using System.Text;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class UnityWebRequestExample : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {
        var req = new UserCreateReq();
        {
            req.userName = "userName";
            req.passwd = "passwd";
        }
        var ackType = MsgType.CREATE_USER_ACK;

        StartCoroutine(Post<UserCreateReq, UserCreateAck>(req, (ack) => {
            if (ackType == ack.msgType) {
                text.text = ack.errCode.ToString();
            }
        }));
	}

    IEnumerator Post<T1, T2>(T1 req, Action<T2> callback)
	{
        var str = JsonUtility.ToJson(req);

        var uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(str));
        uploadHandler.contentType = "application/json";
		
        var uwr = UnityWebRequest.Post("http://127.0.0.1:20000/", string.Empty);
        uwr.uploadHandler = uploadHandler;
        uwr.timeout = 3;

        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError || uwr.isHttpError)
		{
            Debug.Log(uwr.error);
		}
		else
		{
            var ack = JsonUtility.FromJson<T2>(uwr.downloadHandler.text);
            callback(ack);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
