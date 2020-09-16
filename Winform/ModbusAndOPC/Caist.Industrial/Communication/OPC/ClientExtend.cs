﻿using Hylasoft.Opc.Common;
using Hylasoft.Opc.Da;
using Hylasoft.Opc.Ua;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Communication.OPC
{
    /// <summary>
    ///     Opc Client Extend interface, Unified for DA and UA
    /// </summary>
    public interface IClientExtend : IDisposable
    {
        /// <summary>
        ///     Unified Root Node
        /// </summary>
        Node RootNodeBase { get; }

        /// <summary>
        ///     Connect the client to the OPC Server
        /// </summary>
        void Connect();

        /// <summary>
        ///     Read a tag
        /// </summary>
        /// <typeparam name="T">The type of tag to read</typeparam>
        /// <param name="tag">
        ///     The fully-qualified identifier of the tag. You can specify a subfolder by using a comma delimited name.
        ///     E.g: the tag `foo.bar` reads the tag `bar` on the folder `foo`
        /// </param>
        /// <returns>The value retrieved from the OPC</returns>
        T Read<T>(string tag);

        /// <summary>
        ///     Write a value on the specified opc tag
        /// </summary>
        /// <typeparam name="T">The type of tag to write on</typeparam>
        /// <param name="tag">
        ///     The fully-qualified identifier of the tag. You can specify a subfolder by using a comma delimited name.
        ///     E.g: the tag `foo.bar` writes on the tag `bar` on the folder `foo`
        /// </param>
        /// <param name="item"></param>
        void Write<T>(string tag, T item);

        /// <summary>
        ///     Read a tag asynchronusly
        /// </summary>
        Task<T> ReadAsync<T>(string tag);

        /// <summary>
        ///     Write a value on the specified opc tag asynchronously
        /// </summary>
        Task WriteAsync<T>(string tag, T item);

        /// <summary>
        ///     Finds a node on the Opc Server asynchronously
        /// </summary>
        Task<Node> FindNodeAsync(string tag);

        /// <summary>
        ///     Explore a folder on the Opc Server asynchronously
        /// </summary>
        Task<IEnumerable<Node>> ExploreFolderAsync(string tag);
    }

    /// <summary>
    ///     UaClient Extend
    /// </summary>
    public class MyDaClient : DaClient, IClientExtend
    {
        /// <summary>
        ///     UaClient Extend
        /// </summary>
        /// <param name="serverUrl">Url address of Opc UA server</param>
        public MyDaClient(Uri serverUrl) : base(serverUrl)
        {
        }

        /// <summary>
        ///     Unified root node
        /// </summary>
        public Node RootNodeBase => RootNode;

    }

    /// <summary>
    ///     DaClient Extend
    /// </summary>
    public class MyUaClient : UaClient, IClientExtend
    {
        /// <summary>
        ///     DaClient Extend
        /// </summary>
        public MyUaClient(Uri serverUrl) : base(serverUrl)
        {
        }

        /// <summary>
        ///     Unified root node
        /// </summary>
        public Node RootNodeBase => RootNode;

    }

    /// <summary>
    ///     Param input of OpcConnector
    /// </summary>
    public class OpcParamIn
    {
        /// <summary>
        ///     Is the action read (not is write)
        /// </summary>
        public bool IsRead { get; set; }

        /// <summary>
        ///     Tag of a node
        /// </summary>
        public string[] Tag { get; set; }

        /// <summary>
        ///     Tag splitter of a node
        /// </summary>
        public char Split { get; set; }

        /// <summary>
        ///     The value set to node(only available when IsRead is false
        /// </summary>
        public object SetValue { get; set; }
    }

    /// <summary>
    ///     Param output of OpcConnector
    /// </summary>
    public class OpcParamOut
    {
        /// <summary>
        ///     Is the action success
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        ///     Action return values
        /// </summary>
        public byte[] Value { get; set; }
    }
}