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
using Newtonsoft.Json;

namespace DebuggerProtocolAdapter.Hosting.Contracts
{
    public class SetBreakpointsRequest
    {
        public static readonly
            RequestType<SetBreakpointsRequest, SetBreakpointsResponse> Type =
            RequestType<SetBreakpointsRequest, SetBreakpointsResponse>.Create("setBreakpoints");

        /// <summary>
        /// The source location of the breakpoints; either source.path or source.reference must be specified.
        /// </summary>
        public Source Source
        {
            get;set;
        }

        /// <summary>
        /// The code locations of the breakpoints.
        /// </summary>
        public List<SourceBreakpoint> Breakpoints
        {
            get; set;
        }

        /// <summary>
        /// Deprecated: The code locations of the breakpoints.
        /// </summary>
        public List<int> Lines
        {
            get; set;
        }

        /// <summary>
        /// A value of true indicates that the underlying source has been modified which results in new breakpoint locations. 
        /// </summary>
        public bool SourceModified
        {
            get; set;
        }
    }

    public class SetBreakpointsResponse
    {
        /// <summary>
        /// Information about the breakpoints. The array elements are in the same
        /// order as the elements of the 'breakpoints' (or the deprecated 'lines') in the SetBreakpointsArguments.
        /// </summary>
        public List<Breakpoint> Breakpoints
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="breakpoints"></param>
        public SetBreakpointsResponse(List<Breakpoint> breakpoints)
        {
            this.Breakpoints = breakpoints;
        }
    }

    /// <summary>
    /// Properties of a breakpoint passed to the setBreakpoints request.
    /// </summary>
    public class SourceBreakpoint
    {
        /// <summary>
        /// The source line of the breakpoint.
        /// </summary>
        public int Line
        {
            get; set;
        }

        /// <summary>
        /// An optional source column of the breakpoint.
        /// </summary>
        public int Column
        {
            get; set;
        }

        /// <summary>
        /// An optional expression for conditional breakpoints.
        /// </summary>
        public string Condition
        {
            get; set;
        }

        /// <summary>
        /// An optional expression that controls how many hits of the breakpoint are ignored. 
        /// The backend is expected to interpret the expression as needed.
        /// </summary>
        public string HitCondition
        {
            get; set;
        }
    }

    /// <summary>
    /// Information about a Breakpoint created in setBreakpoints or setFunctionBreakpoints.
    /// </summary>
    public class Breakpoint
    {
        /// <summary>
        /// An optional unique identifier for the breakpoint.
        /// </summary>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// If true breakpoint could be set (but not necessarily at the desired location).
        /// </summary>
        public bool Verified
        {
            get; set;
        }

        /// <summary>
        /// An optional message about the state of the breakpoint. This is shown to the user and can be used to explain why a breakpoint could not be verified.
        /// </summary>
        public string Message
        {
            get; set;
        }

        /// <summary>
        /// The source where the breakpoint is located.
        /// </summary>
        public Source Source
        {
            get; set;
        }

        /// <summary>
        /// The start line of the actual range covered by the breakpoint.
        /// </summary>
        public int Line
        {
            get; set;
        }

        /// <summary>
        /// An optional start column of the actual range covered by the breakpoint.
        /// </summary>
        public int Column
        {
            get; set;
        }

        /// <summary>
        /// An optional end line of the actual range covered by the breakpoint.
        /// </summary>
        public int? EndLine
        {
            get; set;
        }

        /// <summary>
        /// An optional end column of the actual range covered by the breakpoint. 
        /// If no end line is given, then the end column is assumed to be in the start line.
        /// </summary>
        public int? EndColumn
        {
            get; set;
        }
    }


}

