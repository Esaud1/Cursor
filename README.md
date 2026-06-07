# Workspace Projects

يوجد مشروعان في هذا المستودع. **كل مشروع له مجلد ومنفذ خاص.**

---

## 🛍️ W Comtismc — متجر التجميل (المتجر الإلكتروني)

```bash
cd WComtismc
dotnet run
```

**أو من جذر المشروع:**

```bash
./run-store.sh
```

### رابط المتجر

**http://localhost:5000**

> إذا فتح لك **نظام الجلسات** بدل المتجر، فأنت تشغّل المشروع الخطأ. أوقف التشغيل الحالي (`Ctrl+C`) ثم نفّذ الأوامر أعلاه من مجلد `WComtismc`.

---

## 📋 Enad Web APP — نظام الجلسات

```bash
cd EnadWebApp
dotnet run --urls "http://localhost:5001"
```

### رابط نظام الجلسات

**http://localhost:5001**

---

## ملخص المنافذ

| المشروع | المجلد | الرابط |
|---------|--------|--------|
| **المتجر** W Comtismc | `WComtismc/` | http://localhost:5000 |
| **نظام الجلسات** Enad Web APP | `EnadWebApp/` | http://localhost:5001 |
