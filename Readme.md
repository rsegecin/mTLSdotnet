# mTLS dotnet core Demo

This project implements the interaction between two dotnet core services through mTLS.

### Build and Run

This demo requires [dotnet core](https://dotnet.microsoft.com/download/dotnet-core/3.1) v3.1 to run.

- Run `build.sh` to create the certificates necessary for this demo
- Start the Service and Client project through dotnet or Visual Studio
- Make a http call to `http://localhost:5010/api/client` or if you install all the ca.crt created on the **Trusted Root Certification Authorities** and client.crt on **Personal Store** call to `https://localhost:5011/api/client`.

```sh
$ sh build.sh
$ cd Client
$ dotnet bin/Debug/netcoreapp3.1/Client.dll
$ cd Service
$ dotnet bin/Debug/netcoreapp3.1/Service.dll
```

License
----

MIT


**Free Software, Hell Yeah!**

   [issue]: <https://github.com/dotnet/runtime/issues/41260>
