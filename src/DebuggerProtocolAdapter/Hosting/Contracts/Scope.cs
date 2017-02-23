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
    public class ScopeRequest
    {
        public static readonly
            RequestType<ScopeRequest, ScopeResponse> Type =
            RequestType<ScopeRequest, ScopeResponse>.Create("scopes");

        /// <summary>
        /// Retrieve the scopes for this stackframe. 
        /// </summary>
        public int FrameId
        {
            get;set;
        }
    }

    public class ScopeResponse
    {
        /** The scopes of the stackframe. If the array has length zero, there are no scopes available. */
        public List<Scope> Scopes
        {
            get;set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ScopeResponse()
        {
            this.Scopes = new List<Scope>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scopes"></param>
        public ScopeResponse(List<Scope> scopes)
        {
            this.Scopes = scopes;
        }
    }

    /// <summary>
    /// A Scope is a named container for variables. Optionally a scope can map to a source or a range within a source.
    /// </summary>
    public class Scope
    {
        /// <summary>
        /// Name of the scope such as 'Arguments', 'Locals'.
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// The variables of this scope can be retrieved by passing the value of variablesReference to the VariablesRequest.
        /// </summary>
        public int VariablesReference
        {
            get; set;
        }

        /// <summary>
        /// The number of named variables in this scope.
        /// The client can use this optional information to present the variables in a paged UI and fetch them in chunks.
        /// </summary>
        public int NamedVariables
        {
            get; set;
        }

        /// <summary>
        /// The number of indexed variables in this scope.
        /// The client can use this optional information to present the variables in a paged UI and fetch them in chunks.
        /// </summary>
        public int IndexedVariables
        {
            get; set;
        }

        /// <summary>
        /// If true, the number of variables in this scope is large or expensive to retrieve.
        /// </summary>
        public bool Expensive
        {
            get; set;
        }

        /// <summary>
        ///  Optional source for this scope. 
        /// </summary>
        public Source Source
        {
            get; set;
        }

        /// <summary>
        /// Optional start line of the range covered by this scope.
        /// </summary>
        public int Line
        {
            get; set;
        }

        /// <summary>
        /// Optional start column of the range covered by this scope.
        /// </summary>
        public int Column
        {
            get; set;
        }

        /// <summary>
        /// Optional end line of the range covered by this scope.
        /// </summary>
        public int EndLine
        {
            get; set;
        }

        /// <summary>
        /// Optional end column of the range covered by this scope.
        /// </summary>
        public int EndColumn
        {
            get; set;
        }
    }


}

