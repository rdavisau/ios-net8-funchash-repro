rm -rf bin obj && dotnet restore FuncHash-net7.csproj && dotnet run --project FuncHash-net7.csproj -f net7.0-ios -c Release -p:ArchiveOnBuild=true -r:ios-arm64 /p:_DeviceName=$1 /p:EnableAssemblyILStripping=false -v:n
