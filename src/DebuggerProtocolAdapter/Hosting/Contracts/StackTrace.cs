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
    public class StackTraceRequest
    {
        public static readonly
            RequestType<StackTraceRequest, StackTraceResponse> Type =
            RequestType<StackTraceRequest, StackTraceResponse>.Create("stackTrace");

        /// <summary>
        /// Retrieve the stacktrace for this thread.
        /// </summary>
        public int ThreadId
        {
            get;set;
        }

        /// <summary>
        /// The index of the first frame to return; if omitted frames start at 0.
        /// </summary>
        public int StartFrame
        {
            get;set;
        }

        /// <summary>
        /// The maximum number of frames to return. If levels is not specified or 0, all frames are returned.
        /// </summary>
        public int Levels
        {
            get;set;
        }

        /// <summary>
        /// Specifies details on how to format the stack frames.
        /// </summary>
        public StackFrameFormat Format
        {
            get;set;
        }
    }

    public class StackTraceResponse
    {

        /// <summary>
        /// The frames of the stackframe. If the array has length zero, there are no stackframes available.
        /// This means that there is no location information available.
        /// </summary>
        public List<StackFrame> StackFrames
        {
            get; set;
        }

        /// <summary>
        /// The total number of frames available.
        /// </summary>
        public int TotalFrames
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        public StackTraceResponse()
        {
            this.StackFrames = new List<StackFrame>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stackFrames"></param>
        public StackTraceResponse(List<StackFrame> stackFrames)
        {
            this.StackFrames = stackFrames;
        }

    }

    /// <summary>
    /// Provides formatting information for a value.
    /// </summary>
    public class ValueFormat
    {
        /// <summary>
        /// Display the value in hex.
        /// </summary>
        public bool Hex
        {
            get;set;
        }
    }

    /** Provides formatting information for a stack frame. */
    public class StackFrameFormat : ValueFormat
    {
        /// <summary>
        /// Displays parameters for the stack frame.
        /// </summary>
        bool Parameters
        {
            get; set;
        }

        /// <summary>
        /// Displays the types of parameters for the stack frame.
        /// </summary>
        bool ParameterTypes
        {
            get; set;
        }

        /// <summary>
        /// Displays the names of parameters for the stack frame.
        /// </summary>
        bool ParameterNames
        {
            get; set;
        }

        /// <summary>
        /// Displays the values of parameters for the stack frame.
        /// </summary>
        bool ParameterValues
        {
            get; set;
        }

        /// <summary>
        /// Displays the line number of the stack frame.
        /// </summary>
        bool Line
        {
            get; set;
        }

        /// <summary>
        /// Displays the module of the stack frame. 
        /// </summary>
        bool Module
        {
            get; set;
        }
    }


    /// <summary>
    /// A Source is a descriptor for source code. It is returned from the debug adapter as part 
    /// of a StackFrame and it is used by clients when specifying breakpoints.
    /// </summary>
    public class Source
    {
        /// <summary>
        /// The short name of the source. Every source returned from the debug adapter has a name. 
        /// When sending a source to the debug adapter this name is optional.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// The path of the source to be shown in the UI. It is only used to locate and load the content of 
        /// the source if no sourceReference is specified (or its vaule is 0).
        /// </summary>
        public string Path
        {
            get; set;
        }

        /// <summary>
        /// If sourceReference > 0 the contents of the source must be retrieved through the SourceRequest (even if a path is specified). 
        /// A sourceReference is only valid for a session, so it must not be used to persist a source. 
        /// </summary>
        public int SourceReference
        {
            get; set;
        }

        /// <summary>
        /// Enum of how to present the source in the UI
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PresentationHints
        {
            [EnumMember(Value = "emphasize")]
            Emphasize,
            [EnumMember(Value = "deemphasize")]
            Deemphasize
        }

        /// <summary>
        /// An optional hint for how to present the source in the UI. A value of 'deemphasize' can be used to indicate that the source 
        /// is not available or that it is skipped on stepping.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PresentationHints PresentationHint
        {
            get;set;
        }


        /// <summary>
        /// The (optional) origin of this source: possible values 'internal module', 'inlined content from source map', etc.
        /// </summary>
        public string Origin
        {
            get; set;
        }

        /// <summary>
        /// Optional data that a debug adapter might want to loop through the client. 
        /// The client should leave the data intact and persist it across sessions. The client should not interpret the data. */
        /// </summary>
        public object AdapterData
        {
            get; set;
        }

        /// <summary>
        /// The checksums associated with this file.
        /// </summary>
        public List<ChecksumItem> Checksums
        {
            get; set;
        }
    }

    /** A Stackframe contains the source location. */
    public class StackFrame
    {
        /// <summary>
        /// An identifier for the stack frame. It must be unique across all threads. 
        /// This id can be used to retrieve the scopes of the frame with the 'scopesRequest' or to restart the execution of a stackframe.
        /// </summary>
        public int Id
        {
            get; set;
        }

        /// <summary>
        /// The name of the stack frame, typically a method name.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// The optional source of the frame.
        /// </summary>
        public Source Source
        {
            get; set;
        }

        /// <summary>
        /// The line within the file of the frame. If source is null or doesn't exist, line is 0 and must be ignored.
        /// </summary>
        public int Line
        {
            get; set;
        }

        /// <summary>
        /// The column within the line. If source is null or doesn't exist, column is 0 and must be ignored.
        /// </summary>
        public int Column
        {
            get; set;
        }

        /// <summary>
        /// An optional end line of the range covered by the stack frame.
        /// </summary>
        public int EndLine
        {
            get; set;
        }

        /// <summary>
        /// An optional end column of the range covered by the stack frame.
        /// </summary>
        public int EndColumn
        {
            get; set;
        }

        /// <summary>
        /// The module associated with this frame, if any.
        /// </summary>
        public string ModuleId
        {
            get; set;
        }
    }


    /// <summary>
    /// The checksum of an item calculated by the specified algorithm.
    /// </summary>
    public class ChecksumItem
    {
        /// <summary>
        /// The algorithm used to calculate this checksum. 
        /// </summary>
        public ChecksumAlgorithm Algorithm
        {
            get; set;
        }

        /// <summary>
        /// Value of the checksum.
        /// </summary>
        public string Checksum
        {
            get; set;
        }
    }


}

