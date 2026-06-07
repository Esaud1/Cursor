#!/usr/bin/env bash
cd "$(dirname "$0")/WComtismc"
echo "تشغيل متجر W Comtismc على http://localhost:5000"
dotnet run --urls "http://localhost:5000"
