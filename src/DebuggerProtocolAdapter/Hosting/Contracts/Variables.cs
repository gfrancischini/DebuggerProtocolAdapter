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
    public class VariablesRequest
    {
        public static readonly
            RequestType<VariablesRequest, VariablesResponse> Type =
            RequestType<VariablesRequest, VariablesResponse>.Create("variables");

        /// <summary>
        /// The Variable reference.
        /// </summary>
        public int VariablesReference
        {
            get;set;
        }

        /** Optional filter to limit the child variables to either named or indexed. If ommited, both types are fetched. */
        //filter?: 'indexed' | 'named';

        /// <summary>
        /// The index of the first variable to return; if omitted children start at 0.
        /// </summary> 
        public int Start
        {
            get;set;
        }

        /// <summary>
        /// The number of variables to return. If count is missing or 0, all variables are returned.
        /// </summary>
        public int Count
        {
            get; set;
        }

        /// <summary>
        /// Specifies details on how to format the Variable values. 
        /// </summary>
        public ValueFormat Format
        {
            get; set;
        }
    }

    public class VariablesResponse
    {
        /// <summary>
        /// All (or a range) of variables for the given variable reference.
        /// </summary>
        public List<Variable> Variables
        {
            get;set;
        }

        public VariablesResponse()
        {
            this.Variables = new List<Variable>();
        }

        public VariablesResponse(List<Variable> variables)
        {
            this.Variables = variables;
        }
    }

    public class Variable
    {
        /// <summary>
        /// The variable's name.
        /// </summary>
        public string Name
        {
            get;set;
        }

        /// <summary>
        /// The variable's value. This can be a multi-line text, e.g. for a function the body of a function.
        /// </summary>
        public string Value
        {
            get; set;
        }

        /// <summary>
        ///  The type of the variable's value. Typically shown in the UI when hovering over the value.
        /// </summary>
        public string Type
        {
            get; set;
        }

        /// <summary>
        /// Properties of a variable that can be used to determine how to render the variable in the UI. Format of the string value: TBD.
        /// </summary>
        public string Kind
        {
            get; set;
        }

        /// <summary>
        /// Optional evaluatable name of this variable which can be passed to the 'EvaluateRequest' to fetch the variable's value.
        /// </summary>
        public string EvaluateName
        {
            get; set;
        }

        /// <summary>
        /// If variablesReference is > 0, the variable is structured and its children can be retrieved by passing variablesReference to the VariablesRequest.
        /// </summary>
        public int VariablesReference
        {
            get; set;
        }

        /// <summary>
        /// The number of named child variables.
        /// The client can use this optional information to present the children in a paged UI and fetch them in chunks.
        /// </summary>
        public int? NamedVariables
        {
            get; set;
        }

        /// <summary>
        /// The number of indexed child variables.
        /// The client can use this optional information to present the children in a paged UI and fetch them in chunks.
        /// </summary>
        public int? IndexedVariables
        {
            get; set;
        }
    }

}

