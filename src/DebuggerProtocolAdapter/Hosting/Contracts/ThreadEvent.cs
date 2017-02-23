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
    public class ThreadParams
    {
        public enum Reasons
        {
            [EnumMember(Value = "started")]
            Started,
            [EnumMember(Value = "exited")]
            Exited
        }

        /// <summary>
        /// The reason for the event (such as: 'started', 'exited').
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Reasons Reason
        {
            get;set;
        }

        /// <summary>
        /// The identifier of the thread.
        /// </summary>
        public int ThreadId
        {
            get; set;
        }
    }

    public class ThreadEvent
    {
        public static readonly
            EventType<ThreadParams> Type =
            EventType<ThreadParams>.Create("thread");

    }

}
