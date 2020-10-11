using System.Threading.Tasks;

namespace Communication.Common
{
    /// <summary>
    ///     ������Э�����ӽӿ�
    /// </summary>
    public interface IConnector<TParamIn, TParamOut>
    {
        /// <summary>
        ///     ��ʶConnector�����ӹؼ���
        /// </summary>
        string ConnectionToken { get; }

        /// <summary>
        ///     �Ƿ�������״̬
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        ///     ����PLC
        /// </summary>
        /// <returns>�Ƿ����ӳɹ�</returns>
        bool Connect();

        /// <summary>
        ///     ����PLC���첽
        /// </summary>
        /// <returns>�Ƿ����ӳɹ�</returns>
        Task<bool> ConnectAsync();

        /// <summary>
        ///     �Ͽ�PLC
        /// </summary>
        /// <returns>�Ƿ�Ͽ��ɹ�</returns>
        bool Disconnect();

        /// <summary>
        ///     �޷��ط�������
        /// </summary>
        /// <param name="message">��Ҫ���͵�����</param>
        /// <returns>�Ƿ��ͳɹ�</returns>
        bool SendMsgWithoutReturn(TParamIn message);

        /// <summary>
        ///     �޷��ط�������
        /// </summary>
        /// <param name="message">��Ҫ���͵�����</param>
        /// <returns>�Ƿ��ͳɹ�</returns>
        Task<bool> SendMsgWithoutReturnAsync(TParamIn message);

        /// <summary>
        ///     �����ط�������
        /// </summary>
        /// <param name="message">��Ҫ���͵�����</param>
        /// <returns>�Ƿ��ͳɹ�</returns>
        TParamOut SendMsg(TParamIn message);

        /// <summary>
        ///     �����ط�������
        /// </summary>
        /// <param name="message">��Ҫ���͵�����</param>
        /// <returns>�Ƿ��ͳɹ�</returns>
        Task<TParamOut> SendMsgAsync(TParamIn message);
    }
}