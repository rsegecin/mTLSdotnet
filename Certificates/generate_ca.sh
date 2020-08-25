#!/bin/bash
echo
echo "########### Generating CA (Self Signed Certificate) ###########"
echo
cd CA
openssl req -x509 -days 3000 -config ca.cnf -newkey rsa:4096 -sha256 -out ca.pem  -outform PEM -nodes -batch

echo
echo "########### Generating 'Installers' for CA ###########"
echo
openssl pkcs12 -inkey ca.key -in ca.pem -export -out ca.pfx -passout pass:''
openssl x509 -outform der -in ca.pem -out ca.crt
cd ..

echo "########### Generating CA Completed ###########"