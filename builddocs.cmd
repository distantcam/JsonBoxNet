rd docs\api /s /q

dotnet build src\JsonboxNet.sln --configuration Release

InheritDoc -b src -o

xmldocmd src\JsonBoxNet\bin\Release\net462\JsonBoxNet.dll docs/api
xmldocmd src\JsonBoxNet.Newtonsoft\bin\Release\net462\JsonBoxNet.Newtonsoft.dll docs/api
xmldocmd src\JsonBoxNet.TextJson\bin\Release\net462\JsonBoxNet.TextJson.dll docs/api
