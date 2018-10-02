#!/bin/bash
set -e
set -o allexport
eval $(cat .env | tr -d '\r' | sed 's_%\([^%]*\)%_$\1_g')
set +o allexport
if [ ! -d "packages" ]; then mono .paket/paket.exe restore; fi
mono packages/FAKE/tools/FAKE.exe scripts/build.fsx $@

# there is a problem with sqlproj
# if you need to test a project use ./build.sh test -st to skip DB targets
