# برنامه سیستم استخدام
این برنامه به زبان </br>
ASP .NET Core 3.0</br>
و با تکنولوژی</br>
Razor Page</br>
نوشته شده است.</br>
به هر مرحله از ثبت نام یک</br>
Level</br>
گفته میشود. مثلا مرحله دریافت اطلاعات اولیه کاربر </br>
Level1</br>
میباشد و مرحله تغییر پسورد </br>
Level2</br>
میباشد و ... . ترتیب نمایش این مراحل نیز قابل شخصی سازی است که در پایین توضیح داده شده است.

# اضافه کردن مرحله جدید
برای اضافه کردن مرحله جدید باید مراحل زیر را انجام دهید</br>
1.create a new field in PageSequence table that its name is english number in letters like Ten, Eleven, Twelve and ... .</br>
2.make new PageSequence field value null in previous rows.</br>
3.scaffold database in visual studio.</br>
4.in root Index.cs file in the "OnGet() > if(id==null)" section introduce new level.</br>
5.in Utilities > utility.cs introduce new level in two section of file.</br>
6.create new table for new level in database and scaffold it.</br>
7.create new folder named "LevelN" that N is last number + 1 and should contain Index.cs file.</br>
8.change partial view of steps for new level.</br>
9.in Level folder (that has no number end of its name) you should enter last number + 1 for partial view model.</br>

# ورود اولین بار
اولین بار که کاربر روی لینکی که براش ارسال شده کلیک میکنه در اصل یک لینک به فرمت زیر رو باز کرده:</br>
https://EmploymentSystemDomain/ID</br>
که آی دی تولید شده بر اساس شماره کاربر تولید شده و یکتا است. و هر آی دی 24 ساعت فرصت دارد وارد شود. ابتدا چک میکند ای دی منقضی نشده باشد و موجود باشد اگر اوکی بود و دفعه اول بود مستقیما به مرحله دریافت اطلاعات اولیه میرود و اگر این مرحله را پشت سر گذاشته باشد به صفحه لاگین میرود.

# ترتیب نمایش مراحل
در جدول </br>
PageSequence</br>
هر سطر معرف یک ترتیب نمایش است. و هرکدام که فیلد</br> 
Active</br>
آن فعال باشد در نمایش ترتیب صفحات اعمال خواهد شد. آخرین مرحله ای که در این ترتیب وجود دارد همیشه باید</br> 
NULL</br>
باشد.

# رکورد هر ترتیب
هر ترتیبی که انتخاب شود یا اکتیو باشد در پایان مراحل ثبت نام کاربر با توجه به اطلاعاتی که در جدول </br>
PageTimeLog</br>
برای هر صفحه ذخیره شده است مدت زمان ثبت نام را محاسبه میکند و اگر از مقدار قبلی که در جدول موجود است کمتر بود آن را به مقدار جدید تغییر میدهد و رکورد صفحه تغییر میکند.

# بانک اطلاعاتی
در زیر لیستی از جداول استفاده شده در برنامه توضیح داده شده است:</br>
TblPagesSequence => ترتیب نمایش مراحل ثبت نام برای کاربر</br>
TblPageTimeLog => ذخیره مدت زمانی که طول کشیده کاربر مرحله را تکمیل کند</br>
TblIpLog => آی پی کاربران</br>
TblLink => لینک هایی که برای کاربران ارسال میشود در این جدول ذخیره میشوند</br>
TblUser => ذخیره یوزر و پسورد کاربران برای ورود مجدد به سایت</br>
(TblSmsReceived, TblSmsSent) => مربوط به برنامه مدیریت اس ام اس برای ارسال لینک ثبت نام به کاربران</br>
</br>
سایر جداول برای صفحات ثبت نام میباشد:</br>
(PayDiploma, PayEducation, TblCustomerDegree)</br>
(TblMilitary, TblMilitaryOrganization, TblUserMilitary)</br>
(TblLeaveJob, TblJobTamin, TblWorkExperience, TblWorkExperienceLeaveJobDtl)</br>
(TblJob, TblUserJob)</br>
(TblSkill, TblUserSkill)</br>
(TblEmergencyCall)</br>
(TblGeneralRecord)</br>
(TblHowFind)</br>
(TblMedicalRecord)</br>
(TblPrimaryInformation)</br>

# مراحل کار
این سیستم شامل 4 برنامه مجزاست که این برنامه یک جزء از آن است. برنامه ها شامل</br>
1. برنامه مدیریت اس ام اس</br>
2. برنامه کوتاه کننده لینک</br>
3. برنامه ثبت نام</br>
4. برنامه پشتیبانی اس ام اس</br>
می باشند. که ابتدا کاربر عدد یک را به شماره آگهی شده پیامک میکند و یک لینک ثبت نام که به وسیله برنامه کوتاه کننده لینک کوتاه شده به او فرستاده میشود و پس از باز شدن لینک توسط کاربر به برنامه ثبت نام هدایت میشود و مراحل ثبت نام خود را انجام میدهد.

# نکات
1. هر لینک ثبت نام 24 ساعت فعال است</br>
2. وقتی کاربر یک رو میفرسته بر اساس شماره تلفنش یک ای دی میگیره و با آن ای دی ثبت نام میکنه</br>
3. به محض این که مرحله ورود اطلاعات اولیه رو انجام بده یوزر و پسورد میگیره و میتونه بقیه مراحل رو بعدا انجام بده</br>
