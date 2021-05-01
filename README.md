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

#جداول
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
