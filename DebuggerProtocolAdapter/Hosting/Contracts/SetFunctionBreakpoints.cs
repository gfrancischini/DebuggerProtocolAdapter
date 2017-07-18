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
    public class SetFunctionBreakpointsRequest
    {
        public static readonly
            RequestType<AttachRequest, AttachResponse> Type =
            RequestType<AttachRequest, AttachResponse>.Create("setFunctionBreakpoints");

        /// <summary>
        /// The function names of the breakpoints.
        /// </summary>
        public List<FunctionBreakpoint> Breakpoints
        {
            get; set;
        }
    }

    public class SetFunctionBreakpointsResponse
    {
        /// <summary>
        /// Information about the breakpoints. The array elements correspond to the elements of the 'breakpoints' array.
        /// </summary>
        public List<Breakpoint> Breakpoints
        {
            get; set;
        }
    }

    /// <summary>
    /// Properties of a breakpoint passed to the setFunctionBreakpoints request. 
    /// </summary>
    public class FunctionBreakpoint
    {
        /// <summary>
        /// The name of the function.
        /// </summary>
        public string Name
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
        /// An optional expression that controls how many hits of the breakpoint are ignored. The backend is expected to interpret the expression as needed.
        /// </summary>
        public string HitCondition
        {
            get; set;
        }
    }

}

