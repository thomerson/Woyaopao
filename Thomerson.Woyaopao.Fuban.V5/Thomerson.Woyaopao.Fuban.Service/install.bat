echo %~dp0
%~d0
cd %~dp0

%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\installutil.exe Thomerson.Woyaopao.Fuban.Service.exe
Net Start Woyaopao_Fuban
sc config Woyaopao_Fuban start= auto

pause