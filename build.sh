#!/bin/bash
basedir=$(cd -P -- "$(dirname -- "$0")" && pwd -P)

echo
echo "########### Start Build ###########"

cd Certificates
(exec sh generate_ca.sh)
(exec sh generate_certificates.sh Service/)
(exec sh generate_certificates.sh Client/)
cd ..

cd Service
ln -s ../Certificates/Service/service.pfx
ln -s ../Certificates/Client/client.crt
dotnet build
cd ..

cd Client
ln -s ../Certificates/Client/client.pfx
ln -s ../Certificates/Service/service.crt
dotnet build
cd ..

echo
echo "########### End Build ###########"