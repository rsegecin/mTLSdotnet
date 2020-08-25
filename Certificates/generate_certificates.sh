#!/bin/bash
basedir=$(cd -P -- "$(dirname -- "$0")" && pwd -P)

if [ "$1" == "" ]; then
	echo "Type the directory where the configuration file (cnf) is located."
	set certdir
	read certdir
else
	certdir=$1
fi

echo 
echo "########### Creating Local Certificate Request ###########"
echo
cd ${certdir}
certname=$(basename $(ls *.cnf| head -1) .cnf)

openssl req -config ${certname}.cnf -keyout ${certname}.key -newkey rsa:4096 -sha256 -out ${certname}.csr -outform PEM \
			-passout pass:${certname}_password -batch
cd ${basedir}

pwd 

echo
echo "########### Certifying Local (Certified by CA) ###########"
echo
cd CA
openssl ca -batch -config ca2.cnf -policy signing_policy -extensions signing_req -out ../${certdir}/${certname}.pem -infiles ../${certdir}/${certname}.csr
cd ${basedir}

echo
echo "########### Generating 'Installers' for Local Certificate ###########"
echo
cd ${certdir}
openssl pkcs12 -inkey ${certname}.key -in ${certname}.pem -export -out ${certname}.pfx \
		-passin pass:${certname}_password -passout pass:${certname}_password
openssl x509 -outform der -in ${certname}.pem -out ${certname}.crt
cd ${basedir}