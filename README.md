# Using the Debugger Protocol Adapter C#

The Debugger Protocol Adapter C# implements the [VS Code Debug Protocol](https://github.com/Microsoft/vscode-debugadapter-node/tree/master/protocol) for C#.
All the messages described on the protocol 1.17.x are implemented.

This project uses the [HostingAdapater](https://github.com/gfrancischini/HostingAdapter)

## How to Use

To implement your own debugger for any language you must subscribe for events on the @DebuggerProtocolAdapter.Hosting.Protocol.DebuggerServiceHost([src link](https://github.com/gfrancischini/DebuggerProtocolAdapter/blob/master/src/DebuggerProtocolAdapter/Hosting/Protocol/DebuggerServiceHost.cs))  class.

To subscribe for an event you can:

```C#
serviceHost.SetRequestHandler([TYPE], [HANDLER]);
serviceHost.SetEventHandler([TYPE], [HANDLER]);
```
You can also send Requests and wait for response using:

```C#
await this.serviceHost.SendRequest([TYPE], [PARAMS]);
```

Also there are 4 basic messages that are already consumed by the DebuggerServiceHost. To consume thoses messages you should use:

```C#
serviceHost.RegisterInitializeTask([CALLBACK]);
serviceHost.RegisterLaunchTask([CALLBACK]);
serviceHost.RegisterConfigurationDoneTask([CALLBACK]);
serviceHost.RegisterDisconnectTask([CALLBACK]);
```