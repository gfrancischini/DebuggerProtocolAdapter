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

using System.Collections.Generic;
using HostingAdapter.Hosting.Contracts;

namespace DebuggerProtocolAdapter.Hosting.Contracts
{
    public class InitializeRequest
    {
        public static readonly
            RequestType<InitializeRequest, InitializeResponse> Type =
            RequestType<InitializeRequest, InitializeResponse>.Create("initialize");

        /// <summary>
        /// Gets or sets the root path of the editor's open workspace.
        /// If null it is assumed that a file was opened without having
        /// a workspace open.
        /// </summary>
        public string RootPath { get; set; }

        /// <summary>
        /// The ID of the (frontend) client using this adapter.
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// The ID of the debug adapter.
        /// </summary>
        public string AdapterID { get; set; }

        /// <summary>
        /// If true all line numbers are 1-based (default).
        /// </summary>
        public bool LinesStartAt1 { get; set; }

        /// <summary>
        /// If true all column numbers are 1-based (default).
        /// </summary>
        public bool ColumnsStartAt1 { get; set; }

        /// <summary>
        /// Determines in what format paths are specified. 
        /// Possible values are 'path' or 'uri'. The default is 'path', which is the native format.
        /// </summary>
        public string PathFormat { get; set; }

        /// <summary>
        /// Client supports the optional type attribute for variables.
        /// </summary>
        public bool SupportsVariableType { get; set; }

        /// <summary>
        /// Client supports the paging of variables.
        /// </summary>
        public bool SupportsVariablePaging { get; set; }

        /// <summary>
        /// Client supports the runInTerminal request.
        /// </summary>
        public bool SupportsRunInTerminalRequest { get; set; }
    }

    public class InitializeResponse
    {
        /// <summary>
        /// The debug adapter supports the configurationDoneRequest.
        /// </summary>
        public bool SupportsConfigurationDoneRequest
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports functionBreakpoints.
        /// </summary>
        public bool SupportsFunctionBreakpoints
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports conditionalBreakpoints.
        /// </summary>
        public bool SupportsConditionalBreakpoints
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports breakpoints that break execution after a specified number of hits.
        /// </summary>
        public bool SupportsHitConditionalBreakpoints
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports a (side effect free) evaluate request for data hovers.
        /// </summary>
        public bool SupportsEvaluateForHovers
        {
            get; set;
        }

        /// <summary>
        /// Available filters  or options for the setExceptionBreakpoints request.
        /// </summary>
        public List<ExceptionBreakpointsFilter> ExceptionBreakpointFilters
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports stepping back via the stepBack and reverseContinue requests.
        /// </summary>
        public bool SupportsStepBack
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports setting a variable to a value.
        /// </summary>
        public bool SupportsSetVariable
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports restarting a frame.
        /// </summary>
        public bool SupportsRestartFrame
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports the gotoTargetsRequest.
        /// </summary>
        public bool SupportsGotoTargetsRequest
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports the stepInTargetsRequest.
        /// </summary>
        public bool SupportsStepInTargetsRequest
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports the completionsRequest.
        /// </summary>
        public bool SupportsCompletionsRequest
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports the modules request.
        /// </summary>
        public bool SupportsModulesRequest
        {
            get; set;
        }

        /// <summary>
        /// The set of additional module information exposed by the debug adapter.
        /// </summary>
        public List<ColumnDescriptor> AdditionalModuleColumns
        {
            get; set;
        }

        /// <summary>
        /// Checksum algorithms supported by the debug adapter.
        /// </summary>
        public List<ChecksumAlgorithm> supportedChecksumAlgorithms
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports the RestartRequest. 
        /// In this case a client should not implement 'restart' by terminating and relaunching the adapter but by calling the RestartRequest.
        /// </summary>
        public bool SupportsRestartRequest
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports 'exceptionOptions' on the setExceptionBreakpoints request.
        /// </summary>
        public bool SupportsExceptionOptions
        {
            get; set;
        }

        /// <summary>
        /// The debug adapter supports a 'format' attribute on the stackTraceRequest, variablesRequest, and evaluateRequest.
        /// </summary>
        public bool SupportsValueFormattingOptions
        {
            get; set;
        }

        /// <summary>
        /// Construct the Initialize Response
        /// </summary>
        public InitializeResponse()
        {
            this.ExceptionBreakpointFilters = new List<ExceptionBreakpointsFilter>();
        }
    }




    /// <summary>
    /// Initialize event notification
    /// </summary>
    public class InitializedEvent
    {
        public static readonly
            EventType<InitializedEvent> Type =
            EventType<InitializedEvent>.Create("initialized");
    }
}

