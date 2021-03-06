//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Newtonsoft.Json.Linq;
using System;
using HostingAdapter.Hosting.Contracts;
using HostingAdapter.Hosting.Serializers;

namespace DebuggerProtocolAdapter.Hosting.Protocol.Serializers
{
    /// <summary>
    /// Serializes messages in the V8 format.  Used primarily for debug adapters.
    /// </summary>
    public class V8MessageSerializer : IMessageSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public JObject SerializeMessage(Message message)
        {
            JObject messageObject = new JObject();

            if (message.MessageType == MessageType.Request)
            {
                messageObject.Add("type", JToken.FromObject("request"));
                messageObject.Add("seq", JToken.FromObject(message.Id));
                messageObject.Add("command", message.Method);
                messageObject.Add("arguments", message.Contents);
            }
            else if (message.MessageType == MessageType.Event)
            {
                messageObject.Add("type", JToken.FromObject("event"));
                messageObject.Add("event", message.Method);
                messageObject.Add("body", message.Contents);
            }
            else if (message.MessageType == MessageType.Response)
            {
                messageObject.Add("type", JToken.FromObject("response"));
                messageObject.Add("request_seq", JToken.FromObject(Convert.ToInt32(message.Id)));
                messageObject.Add("command", message.Method);

                if (message.Error != null)
                {
                    // Write error
                    messageObject.Add("success", JToken.FromObject(false));
                    messageObject.Add("message", message.Error);
                }
                else
                {
                    // Write result
                    messageObject.Add("success", JToken.FromObject(true));
                    messageObject.Add("body", message.Contents);
                }
            }

            return messageObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageJson"></param>
        /// <returns></returns>
        public Message DeserializeMessage(JObject messageJson)
        {
            JToken token = null;

            if (messageJson.TryGetValue("type", out token))
            {
                string messageType = token.ToString();

                if (string.Equals("request", messageType, StringComparison.CurrentCultureIgnoreCase))
                {
                    return Message.Request(
                        messageJson.GetValue("seq").ToString(),
                        messageJson.GetValue("command").ToString(),
                        messageJson.GetValue("arguments"));
                }
                else if (string.Equals("response", messageType, StringComparison.CurrentCultureIgnoreCase))
                {
                    if (messageJson.TryGetValue("success", out token))
                    {
                        // Was the response for a successful request?
                        if (token.ToObject<bool>() == true)
                        {
                            return Message.Response(
                                messageJson.GetValue("request_seq").ToString(),
                                messageJson.GetValue("command").ToString(),
                                messageJson.GetValue("body"));
                        }
                        else
                        {
                            return Message.ResponseError(
                                messageJson.GetValue("request_seq").ToString(),
                                messageJson.GetValue("command").ToString(),
                                messageJson.GetValue("message"));
                        }
                    }
                    // else
                    // {
                    //     // TODO: Parse error
                    // }

                }
                else if (string.Equals("event", messageType, StringComparison.CurrentCultureIgnoreCase))
                {
                    return Message.Event(
                        messageJson.GetValue("event").ToString(),
                        messageJson.GetValue("body"));
                }
                else
                {
                    return Message.Unknown();
                }
            }

            return Message.Unknown();
        }
   }
}

