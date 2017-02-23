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
using Newtonsoft.Json;

namespace DebuggerProtocolAdapter.Hosting.Contracts
{
    public class Capabilities
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


    }

    /// <summary>
    /// An ExceptionBreakpointsFilter is shown in the UI as an option for configuring how exceptions are dealt with.
    /// </summary>
    public class ExceptionBreakpointsFilter
    {
        /// <summary>
        /// The internal ID of the filter. This value is passed to the setExceptionBreakpoints request.
        /// </summary>
        public string Filter
        {
            get; set;
        }

        /// <summary>
        /// The name of the filter. This will be shown in the UI.
        /// </summary>
        public string Label
        {
            get; set;
        }

        /// <summary>
        /// Initial value of the filter. If not specified a value 'false' is assumed.
        /// </summary>
        public bool Default
        {
            get; set;
        }
    }

    /// <summary>
    /// A ColumnDescriptor specifies what module attribute to show in a column of the ModulesView, 
    /// how to format it, and what the column's label should be.
    /// It is only used if the underlying UI actually supports this level of customization.
    /// </summary>
    public class ColumnDescriptor
    {
        /// <summary>
        /// Name of the attribute rendered in this column.
        /// </summary>
        public string AttributeName
        {
            get; set;
        }

        /// <summary>
        /// Header UI label of column.
        /// </summary>
        public string Label
        {
            get; set;
        }

        /// <summary>
        /// Format to use for the rendered values in this column. TBD how the format strings looks like.
        /// </summary>
        public string Format
        {
            get; set;
        }

        /** Datatype of values in this column.  Defaults to 'string' if not specified. */
//type?: 'string' | 'number' | 'boolean' | 'unixTimestampUTC';

/// <summary>
/// Width of this column in characters (hint only).
/// </summary>
public int Width
        {
            get; set;
        }
    }

    /// <summary>
    /// Names of checksum algorithms that may be supported by a debug adapter.
    /// </summary>
    public enum ChecksumAlgorithm
    {
        MD5,
        SHA1,
        SHA256,
        timestamp
    }
    //export type ChecksumAlgorithm = 'MD5' | 'SHA1' | 'SHA256' | 'timestamp';

}

