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
    public class ThreadsRequest
    {
        public static readonly
            RequestType<ThreadsRequest, ThreadsResponse> Type =
            RequestType<ThreadsRequest, ThreadsResponse>.Create("threads");

        //empty
    }

    public class ThreadsResponse
    {
        /// <summary>
        /// All threads.
        /// </summary>
        public List<Thread> Threads
        {
            get;set;
        }

        public ThreadsResponse()
        {
            this.Threads = new List<Thread>();
        }

        public ThreadsResponse(List<Thread> threads)
        {
            this.Threads = threads;   
        }
    }

   public class Thread
    {
        /// <summary>
        /// Unique identifier for the thread.
        /// </summary>
        public int Id
        {
            get;set;
        }

        /// <summary>
        /// A name of the thread.
        /// </summary>
        public string Name
        {
            get; set;
        }
    }

}

