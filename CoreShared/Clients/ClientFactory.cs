using Com.DaacoWorks.Protocol.Logger;
using System.Collections.Generic;
using System;
using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Model;

namespace Com.DaacoWorks.Protocol.Clients
{
    /// <summary>
    /// Factory class to create client instances.
    /// </summary>
    /// <typeparam name="TClient"></typeparam>
    /// <typeparam name="TException"></typeparam>
    public abstract class ClientFactory<TClient, TException>
        where TClient : IClient
        where TException : System.Exception
    {

        private static ILogger logger = LoggerFactory.GetLogger(typeof(ClientFactory<TClient, TException>).FullName);

        private static List<TClient> clients = new List<TClient>();

        static ClientFactory()
        {
            AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;
        }

        //TODO: hold weak reference of client
        private static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            logger.Info("shutdown hook called");
            lock (clients)
            {
                int size = clients.Count - 1;
                while (size >= 0)
                {
                    TClient client = clients[size--];
                    RemoveClient(client);
                    client.Shutdown();
                }
                clients.Clear();
                if (ExecutorFactory.GetGlobalRequestExecutor() is RequestExecutor reqEx)
                    reqEx.Shutdown();
                if (ExecutorFactory.GetGlobalResponseExecutor() is ResponseExecutor resEx)
                    resEx.Shutdown();
            }
        }

        /// <summary>
        /// Adds the client into local cache if not already exists.
        /// </summary>
        /// <param name="client">the client to be added</param>
        /// <returns>added or existing client</returns>
        protected static TClient AddClient(TClient client)
        {
            lock (clients)
            {
                var index = clients.IndexOf(client);
                if (index >= 0)
                    return clients[index];
                var tempClient = client as Client;
                if (tempClient != null)
                {
                    tempClient.Init();
                    clients.Add(client);
                    return client;
                }
                else
                {
                    throw new System.Exception("Invalid client");
                }
            }
        }

        /// <summary>
        /// Removes the client from the local cache.
        /// </summary>
        /// <param name="client">the client to be removed</param>
        public static void RemoveClient(TClient client)
        {
            lock (clients)
            {
                clients.Remove(client);
            }
        }

        /// <summary>
        /// Create a client instance and returns
        /// </summary>
        /// <param name="connectionParameters">connection parameters</param>
        /// <returns>the client</returns>
        public abstract TClient Create(ConnectionParameters connectionParameters);
    }


}