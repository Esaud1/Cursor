#!/usr/bin/env bash
set -e

PORT=5001

cd "$(dirname "$0")/EnadWebApp"

if [ ! -f "Program.cs" ]; then
  echo "خطأ: مجلد EnadWebApp غير مكتمل. استخدم فرع cursor/enad-login-page-6a3d"
  exit 1
fi

echo "تشغيل نظام الجلسات Enad Web APP"
echo "الرابط: http://localhost:${PORT}"
dotnet run --urls "http://localhost:${PORT}"
