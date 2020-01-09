# Ping Monitor

A simple dotnet core 3.0 tool to test network communication throught ping requests

## Configuration

The only configuration needed is the IP list to send ping request against, so edit the key "hosts"
to _appsettings.json_ putting your hosts IPs addresses.

## Build

To generate the executable file just run the command

` dotnet publish -c Release `