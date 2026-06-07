#!/usr/bin/env bash
set -e

PORT=5000

echo "إيقاف أي تطبيق يعمل على المنفذ $PORT..."
fuser -k "${PORT}/tcp" 2>/dev/null || true
sleep 1

cd "$(dirname "$0")/WComtismc"
echo ""
echo "✓ تشغيل متجر W Cosmatic"
echo "✓ الرابط: http://localhost:${PORT}"
echo ""

dotnet run --urls "http://localhost:${PORT}"
