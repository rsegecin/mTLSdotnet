# mTLS dotnet core example

This project implements the interaction between two dotnet core services through mTLS.

### Build and Run

This demo requires [dotnet core](https://dotnet.microsoft.com/download/dotnet-core/3.1) v3.1 or Docker. 

- Run `build.sh` to create the certificates necessary for this demo
- Start the Service and Client project through dotnet, Visual Studio or thorugh Docker Compose.
- Make a http call to `http://localhost:5140/api/client/call-service` 
- *(Optional)* Install the ca.crt that was generated from the build on the **Trusted Root Certification Authorities** and client.crt on **Personal Store** so you can make a https call to `https://localhost:5141/api/client/call-service`.

```sh
$ sh build.sh
$ cd Client
$ dotnet bin/Debug/netcoreapp3.1/Client.dll
$ cd Service
$ dotnet bin/Debug/netcoreapp3.1/Service.dll
$ docker-compose up -d
```

### Question

 - The only thing that I didn't nail in this project is figure out why I can't *curl* inside a container to a https endpoint even though CA is apparently correctly installed in there, if you do have an answer for please heat me out.

License
----

MIT

**Free Software, Hell Yeah!**

   [issue]: <https://github.com/dotnet/runtime/issues/41260>
