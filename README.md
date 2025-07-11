
# 📚 AaronBlog

A personal and extensible book blog system built with ASP.NET Core MVC.  
This project allows you to create your own personal book blog where you can share your favorite books, write summaries, add author details, and fully manage the content through an admin dashboard.

---

## ✨ Features

- 📝 Public blog to showcase books, summaries, and author information
- 🧑‍💼 Admin panel with login (username/password) to:
  - Manage books, authors, tags, and categories
  - Create, edit, and delete content
  - Change username and password
- 🔖 Tag and categorize books
- 🔎 Search and filter functionality
- ⚡ SEO-friendly slugs and URLs
- 🖼 Upload book cover images

---

## 💻 Tech Stack

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server LocalDB (default, can be customized)
- Bootstrap 5
- C#

---

## 🚀 Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/download)
- Visual Studio 2022 (or newer)

### Setup

```bash
# Clone the repository
git clone https://github.com/yourusername/AaronBlog.git

# Navigate to the project folder
cd AaronBlog/src

# Restore dependencies
dotnet restore

# Apply migrations to LocalDB
dotnet ef database update

# Run the application
dotnet run
```

Once running, open your browser at `https://localhost:5001` (or the port Visual Studio shows).

---

## ⚙️ Configuration

By default, the project uses **LocalDB**, so it works out of the box on most development machines.

You can change the connection string in `appsettings.json` to point to your own LocalDB instance or any SQL Server.

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AaronBlogDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

To see the project live on your machine, **you need to update the connection string with your own LocalDB configuration.**

---

## 🔐 Admin Access

- Admin login URL: `/admin/login`
- Default credentials:  
  - **Username:** `admin`
  - **Password:** `admin`

Once logged in, you can:

- Add, edit, and delete books
- Manage authors, tags, and categories
- Update login credentials

---

## 🪪 License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for more details.

---

## 🌟 Contributing

Pull requests are welcome! Feel free to fork the repository and propose changes or improvements.

---

## 📬 Contact

If you'd like to ask questions, suggest features, or just connect, feel free to reach out!

---

# 🇮🇷 فارسی

## 💬 درباره پروژه

**AaronBlog** یک سیستم آماده برای وبلاگ کتاب است که با ASP.NET Core MVC ساخته شده.  
شما می‌توانید کتاب‌هایی که خوانده‌اید را معرفی کنید، خلاصه و توضیحات بنویسید، اطلاعات نویسنده اضافه کنید و همه‌ی این موارد را از طریق یک بخش مدیریت (Admin) کامل کنترل کنید.

---

## ✨ ویژگی‌ها

- 📝 وبلاگ عمومی برای نمایش کتاب‌ها، خلاصه‌ها و اطلاعات نویسنده‌ها
- 🧑‍💼 بخش مدیریت با ورود (username/password) برای:
  - مدیریت کتاب‌ها، نویسنده‌ها، تگ‌ها و دسته‌بندی‌ها
  - افزودن، ویرایش و حذف محتوا
  - تغییر نام کاربری و رمز عبور
- 🔖 دسته‌بندی و برچسب‌گذاری کتاب‌ها
- 🔎 امکان جستجو و فیلتر کردن کتاب‌ها
- ⚡ لینک‌های SEO-friendly (اسلاگ)
- 🖼 امکان آپلود عکس جلد کتاب

---

## 🚀 شروع به کار

### پیش‌نیازها

- [.NET 7 SDK](https://dotnet.microsoft.com/download)
- ویژوال استودیو 2022 یا جدیدتر

### راه‌اندازی

```bash
# کلون کردن ریپازیتوری
git clone https://github.com/yourusername/AaronBlog.git

# رفتن به پوشه پروژه
cd AaronBlog/src

# بازیابی پکیج‌ها
dotnet restore

# اعمال مایگریشن روی LocalDB
dotnet ef database update

# اجرای پروژه
dotnet run
```

پس از اجرا، می‌توانید مرورگر را باز کرده و به آدرس `https://localhost:5001` (یا پورتی که Visual Studio نمایش می‌دهد) بروید.

---

## ⚙️ تنظیمات اتصال

به صورت پیش‌فرض، پروژه از **LocalDB** استفاده می‌کند.  
برای این که پروژه را به صورت زنده (Live) روی سیستم خودتان ببینید، **باید connection string مخصوص LocalDB خودتان را در فایل `appsettings.json` وارد کنید.**

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AaronBlogDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```

همچنین می‌توانید به جای LocalDB، به هر دیتابیس SQL Server دلخواه خود وصل شوید.

---

## 🔐 ورود به بخش مدیریت

- آدرس ورود به بخش مدیریت: `/admin/login`
- اطلاعات پیش‌فرض:
  - **نام کاربری:** `admin`
  - **رمز عبور:** `admin`

بعد از ورود، می‌توانید:

- کتاب‌ها، نویسنده‌ها، تگ‌ها و دسته‌بندی‌ها را مدیریت کنید
- محتوا اضافه یا ویرایش یا حذف کنید
- نام کاربری و رمز عبور را تغییر دهید

---

## 🪪 مجوز

این پروژه تحت مجوز MIT منتشر شده است. برای اطلاعات بیشتر فایل [LICENSE](LICENSE) را ببینید.

---

## 💬 ارتباط

اگر سوال یا پیشنهادی دارید یا می‌خواهید همکاری کنید، خوشحال می‌شوم در ارتباط باشید!

---
