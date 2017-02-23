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
    public class EvaluateRequest
    {
        public static readonly
            RequestType<EvaluateRequest, EvaluateResponse> Type =
            RequestType<EvaluateRequest, EvaluateResponse>.Create("evaluate");

        /// <summary>
        /// The expression to evaluate.
        /// </summary>
        public string Expression
        {
            get;set;
        }

        /// <summary>
        /// Evaluate the expression in the scope of this stack frame. If not specified, the expression is evaluated in the global scope.
        /// </summary>
        public int FrameId
        {
            get; set;
        }

        /// <summary>
        /// The context in which the evaluate request is run. Possible values are 'watch'
        /// if evaluate is run in a watch, 'repl' if run from the REPL console, or 'hover' if run from a data hover.
        /// </summary>
        public string Context
        {
            get; set;
        }

        /// <summary>
        /// Specifies details on how to format the Evaluate result.
        /// </summary>
        public ValueFormat format
        {
            get; set;
        }
    }

    public class EvaluateResponse
    {
        /// <summary>
        /// The result of the evaluate request.
        /// </summary>
        public string Result
        {
            get; set;
        }
        /// <summary>
        /// The optional type of the evaluate result. 
        /// </summary>
        public string Type
        {
            get; set;
        }

        /// <summary>
        /// If variablesReference is > 0, the evaluate result is structured and its children can be 
        /// retrieved by passing variablesReference to the VariablesRequest.
        /// </summary>
        public int VariablesReference
        {
            get; set;
        }

        /// <summary>
        /// The number of named child variables.
        /// The client can use this optional information to present the variables in a paged UI and fetch them in chunks.
        /// </summary>
        public int NamedVariables
        {
            get; set;
        }

        /// <summary>
        /// The number of indexed child variables.
        /// The client can use this optional information to present the variables in a paged UI and fetch them in chunks.
        /// </summary>
        public int IndexedVariables
        {
            get; set;
        }
    }


}

