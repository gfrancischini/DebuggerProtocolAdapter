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
using System.Runtime.Serialization;
using HostingAdapter.Hosting.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DebuggerProtocolAdapter.Hosting.Contracts
{
    public class SetExceptionBreakpointsRequest
    {
        public static readonly
            RequestType<AttachRequest, AttachResponse> Type =
            RequestType<AttachRequest, AttachResponse>.Create("setExceptionBreakpoints");

        /// <summary>
        /// IDs of checked exception options. The set of IDs is returned via the 'exceptionBreakpointFilters' capability.
        /// </summary>
        public List<string> filters
        {
            get; set;
        }

        /// <summary>
        /// Configuration options for selected exceptions.
        /// </summary>
        public List<ExceptionOptions> ExceptionOptions
        {
            get;set;
        }
    }

    public class SetExceptionBreakpointsResponse
    {
        //empty
    }

    /// <summary>
    /// An ExceptionOptions assigns configuration options to a set of exceptions.
    /// </summary>
    public class ExceptionOptions
    {


        /// <summary>
        /// This enumeration defines all possible conditions when a thrown exception should result in a break.
        /// </summary>
        public enum ExceptionBreakMode
        {
            /// <summary>
            /// never breaks
            /// </summary>
            [EnumMember(Value = "never")]
            Never,

            /// <summary>
            /// always breaks
            /// </summary>
            [EnumMember(Value = "always")]
            Always,

            /// <summary>
            /// breaks when exception unhandled
            /// </summary>
            [EnumMember(Value = "unhandled")]
            Unhandled,

            /// <summary>
            /// breaks if the exception is not handled by user code.
            /// </summary>
            [EnumMember(Value = "userUnhandled")]
            UserUnhandled,
        }

        /// <summary>
        /// A path that selects a single or multiple exceptions in a tree. If 'path' is missing, the whole tree is selected. 
        /// By convention the first segment of the path is a category that is used to group exceptions in the UI. 
        /// </summary>
        public List<ExceptionPathSegment> Path
        {
            get;set;
        }

        /// <summary>
        /// Condition when a thrown exception should result in a break.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ExceptionBreakMode BreakMode
        {
            get;set;
        }
    }


    //export type ExceptionBreakMode = 'never' | 'always' | 'unhandled' | 'userUnhandled';

    /// <summary>
    /// An ExceptionPathSegment represents a segment in a path that is used to match leafs or nodes in a tree of exceptions. 
    /// If a segment consists of more than one name, it matches the names provided if 'negate' is false or
    /// missing or it matches anything except the names provided if 'negate' is true. 
    /// </summary>
    public class ExceptionPathSegment
    {
        /// <summary>
        /// If false or missing this segment matches the names provided, otherwise it matches anything except the names provided.
        /// </summary>
        public bool Negate
        {
            get; set;
        }

        /// <summary>
        /// Depending on the value of 'negate' the names that should match or not match.
        /// </summary>
        public List<string> Names
        {
            get; set;
        }
    }

}

