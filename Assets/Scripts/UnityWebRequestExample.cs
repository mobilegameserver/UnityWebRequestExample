using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;
using UnityEngine.Networking;

enum MsgType
{
	CREATE_USER_REQ = 11101,
	CREATE_USER_ACK = 11102,
}

class CreateUserReq
{
	public MsgType msgType = MsgType.CREATE_USER_REQ;

	public string userName;
	public string passwd;
}

class CreateUserAck
{
	public ErrCode errCode;
}

public class UnityWebRequestExample : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {
		StartCoroutine(Post());
	}

	IEnumerator Post()
	{
		var req = new CreateUserReq();
		{
			req.userName = "userName";
			req.passwd = "passwd";
		}
		
		var www = UnityWebRequest.Post("http://127.0.0.1:20000/", JsonUtility.ToJson(req));
		yield return www.Send();

		if (www.isError)
		{
			text.text = www.error;
		}
		else
		{
			var str = Encoding.UTF8.GetString(www.downloadHandler.data);
			var type = str.Substring(2, 5);

			var ack = JsonUtility.FromJson<CreateUserAck>(str.Substring(9, str.Length - 10));
			text.text = ack.errCode.ToString();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
