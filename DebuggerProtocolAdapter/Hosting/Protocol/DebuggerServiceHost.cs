/*
MIT License

Copyright (c) Gabriel Parelli Francischini 2017

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DebuggerProtocolAdapter.Hosting.Contracts;
using DebuggerProtocolAdapter.Hosting.Protocol.Serializers;
using HostingAdapter.Hosting;
using HostingAdapter.Hosting.Channel;
using HostingAdapter.Utility;

namespace DebuggerProtocolAdapter.Hosting.Protocol
{
    /// <summary>
    /// Debugger Protocol Language service host handler.
    /// </summary>
    public class DebuggerServiceHost : ServiceHostBase
    {
        #region Singleton Instance Code

        /// <summary>
        /// Singleton instance of the debugger service host
        /// </summary>
        private static readonly Lazy<DebuggerServiceHost> instance = new Lazy<DebuggerServiceHost>(() => new DebuggerServiceHost());

        /// <summary>
        /// Current instance of the DebuggerServiceHost
        /// </summary>
        public static DebuggerServiceHost Instance
        {
            get { return instance.Value; }
        }

        /// <summary>
        /// Constructs new instance of DebuggerServiceHost using the host. 
        /// Uses the SdioServerChannel together wit the V8MessageSerializer
        /// </summary>
        protected DebuggerServiceHost() : base(new StdioServerChannel(new V8MessageSerializer()))
        {
            // Initialize calbacks 
            this.initializeCallbacks = new List<InitializeCallback>();
            this.disconnectCallbacks = new List<DisconnectCallback>();
            this.launchCallbacks = new List<LaunchCallback>();
            this.configurationDoneCallbacks = new List<ConfigurationDoneCallback>();
        }

        /// <summary>
        /// Provide initialization that must occur after the service host is started
        /// </summary>
        public void Initialize()
        {            
            // Register the requests that this service host will handle
            this.SetRequestHandler(InitializeRequest.Type, this.HandleInitializeRequest);
            this.SetRequestHandler(DisconnectRequest.Type, this.HandleDisconnectRequest);
            this.SetRequestHandler(LaunchRequest.Type, this.HandleLaunchRequest);
            this.SetRequestHandler(ConfigurationDoneRequest.Type, this.HandleConfigurationDoneRequest);
        }

        #endregion

        #region Member Variables

        /// <summary>
        /// This timeout limits the amount of time that shutdown tasks can take to complete
        /// prior to the process shutting down.
        /// </summary>
        private const int ShutdownTimeoutInSeconds = 120;

        /// <summary>
        /// Delegate definition for the host initialization event
        /// </summary>
        /// <param name="startupParams"></param>
        /// <param name="requestContext"></param>
        public delegate Task InitializeCallback(InitializeRequest startupParams, RequestContext<InitializeResponse> requestContext);

        /// <summary>
        /// Delegate definition for the disconnect event
        /// </summary>
        /// <param name="disconnectParams"></param>
        /// <param name="disconnectRequestContext"></param>
        public delegate Task DisconnectCallback(DisconnectRequest disconnectParams, RequestContext<DisconnectResponse> disconnectRequestContext);

        /// <summary>
        /// Delegate definition for the launch initialization event
        /// </summary>
        /// <param name="launchParams"></param>
        /// <param name="requestContext"></param>
        public delegate Task LaunchCallback(LaunchRequest launchParams, RequestContext<LaunchResponse> requestContext);

        /// <summary>
        /// Delegate definition for the configuration done event
        /// </summary>
        /// <param name="configurationDoneParams"></param>
        /// <param name="requestContext"></param>
        public delegate Task ConfigurationDoneCallback(ConfigurationDoneRequest configurationDoneParams, RequestContext<ConfigurationDoneResponse> requestContext);

        /// <summary>
        /// 
        /// </summary>
        private readonly List<InitializeCallback> initializeCallbacks;

        /// <summary>
        /// 
        /// </summary>
        private readonly List<DisconnectCallback> disconnectCallbacks;

        /// <summary>
        /// 
        /// </summary>
        private readonly List<LaunchCallback> launchCallbacks;

        /// <summary>
        /// 
        /// </summary>
        private readonly List<ConfigurationDoneCallback> configurationDoneCallbacks;

        /// <summary>
        /// Current service version from assembly
        /// </summary>
        private static readonly Version serviceVersion = Assembly.GetEntryAssembly().GetName().Version;

        #endregion

        #region Public Callback Registers Methods

        /// <summary>
        /// Add a new method to be called when the initialize request is submitted
        /// </summary>
        /// <param name="callback">Callback to perform</param>
        public void RegisterInitializeTask(InitializeCallback callback)
        {
            initializeCallbacks.Add(callback);
        }

        /// <summary>
        /// Adds a new callback to be called when the disconnect request is submitted
        /// </summary>
        /// <param name="callback">Callback to perform</param>
        public void RegisterDisconnectTask(DisconnectCallback callback)
        {
            disconnectCallbacks.Add(callback);
        }

        /// <summary>
        /// Add a new method to be called when the launch request is submitted
        /// </summary>
        /// <param name="callback">Callback to perform</param>
        public void RegisterLaunchTask(LaunchCallback callback)
        {
            launchCallbacks.Add(callback);
        }

        /// <summary>
        /// Add a new method to be called when the configuration done request is submitted
        /// </summary>
        /// <param name="callback">Callback to perform</param>
        public void RegisterConfigurationDoneTask(ConfigurationDoneCallback callback)
        {
            configurationDoneCallbacks.Add(callback);
        }

        #endregion

        #region Request Handlers

        /// <summary>
        /// Handles the disconnect request 
        /// </summary>
        /// <param name="disconnectParams"></param>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        protected async Task HandleDisconnectRequest(DisconnectRequest disconnectParams, RequestContext<DisconnectResponse> requestContext)
        {
            Logger.Write(LogLevel.Normal, "HandleDisconnectRequest");

            // Call all the shutdown methods provided by the service components
            Task[] shutdownTasks = disconnectCallbacks.Select(t => t(disconnectParams, requestContext)).ToArray();
            TimeSpan shutdownTimeout = TimeSpan.FromSeconds(ShutdownTimeoutInSeconds);

			await this.Exit();

            // shut down once all tasks are completed, or after the timeout expires, whichever comes first.
            await Task.WhenAny(Task.WhenAll(shutdownTasks), Task.Delay(shutdownTimeout)).ContinueWith(t => Environment.Exit(0));
        }

        /// <summary>
        /// Handles the initialization request
        /// </summary>
        /// <param name="initializeParams"></param>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        protected async Task HandleInitializeRequest(InitializeRequest initializeParams, RequestContext<InitializeResponse> requestContext)
        {
            Logger.Write(LogLevel.Normal, "HandleInitializeRequest");

            // Call all tasks that registered on the initialize request
            var initializeTasks = initializeCallbacks.Select(t => t(initializeParams, requestContext));
            await Task.WhenAll(initializeTasks);
        }


        /// <summary>
        /// Handles the launch request
        /// </summary>
        /// <param name="launchParams"></param>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        protected async Task HandleLaunchRequest(LaunchRequest launchParams, RequestContext<LaunchResponse> requestContext)
        {
            Logger.Write(LogLevel.Normal, "HandleLaunchRequest");

            // Call all tasks that registered on the initialize request
            var launchTasks = launchCallbacks.Select(t => t(launchParams, requestContext));
            await Task.WhenAll(launchTasks);
        }

        /// <summary>
        /// Handles the configuration done request
        /// </summary>
        /// <param name="configurationDoneParams"></param>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        protected async Task HandleConfigurationDoneRequest(ConfigurationDoneRequest configurationDoneParams, RequestContext<ConfigurationDoneResponse> requestContext)
        {
            Logger.Write(LogLevel.Normal, "HandleConfigurationDoneRequest");

            // Call all tasks that registered on the initialize request
            var configurationDoneTasks = configurationDoneCallbacks.Select(t => t(configurationDoneParams, requestContext));
            await Task.WhenAll(configurationDoneTasks);            
        }


        #endregion
    }
}
