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

using System.Runtime.Serialization;
using HostingAdapter.Hosting.Contracts;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DebuggerProtocolAdapter.Hosting.Contracts
{
    public class OutputEvent
    {
        public static readonly
           EventType<OutputEvent> Type =
           EventType<OutputEvent>.Create("output");

        /// <summary>
        /// The category of output
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum Categories
        {
            [EnumMember(Value = "console")]
            Console,
            [EnumMember(Value = "stdout")]
            StdOut,
            [EnumMember(Value = "stderr")]
            StdErr,
            [EnumMember(Value = "telemetry")]
            Telemetry
        }

        /// <summary>
        /// The category of output (such as: 'console', 'stdout', 'stderr', 'telemetry'). If not specified, 'console' is assumed.
        /// </summary>
        public Categories Category
        {
            get;set;
        }

        /// <summary>
        /// The output to report.
        /// </summary>
        public string Output
        {
            get;set;
        }

        /// <summary>
        /// If an attribute 'variablesReference' exists and its value is > 0,
        /// the output contains objects which can be retrieved by passing variablesReference to the VariablesRequest.
        /// </summary>
        public int VariablesReference
        {
            get;set;
        }

        /// <summary>
        /// Optional data to report. For the 'telemetry' category the data will be sent to 
        /// telemetry, for the other categories the data is shown in JSON format.
        /// </summary>
        public object Data
        {
            get;set;
        }
    }
}
