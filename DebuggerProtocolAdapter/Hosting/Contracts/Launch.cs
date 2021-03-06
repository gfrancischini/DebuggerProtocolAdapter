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

using HostingAdapter.Hosting.Contracts;
using Newtonsoft.Json;

namespace DebuggerProtocolAdapter.Hosting.Contracts
{
    public class LaunchRequest
    {
        public static readonly
            RequestType<LaunchRequest, LaunchResponse> Type =
            RequestType<LaunchRequest, LaunchResponse>.Create("launch");

        /// <summary>
        /// If noDebug is true the launch request should launch the program without enabling debugging.
        /// </summary>
        public bool NoDebug
        {
            get;set;
        }
    }

    public class LaunchResponse
    {
        //empty
    }


}

