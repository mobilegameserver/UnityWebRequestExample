[System.Serializable]
class UserCreateReq
{
    public MsgType msgType = MsgType.CREATE_USER_REQ;

    public string userName;
    public string passwd;
}

[System.Serializable]
class UserCreateAck
{
    public MsgType msgType = MsgType.CREATE_USER_ACK;
    public ErrCode errCode = ErrCode.UNKNOWN;
}