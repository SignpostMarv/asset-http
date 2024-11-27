setup:
	git submodule update --init --recursive
	dotnet restore ./CUE4Parse/CUE4Parse/CUE4Parse.csproj
	dotnet restore ./src/AssetHttp.csproj
	make UnrealEngine UnrealEngine--copy-Oodle

.PHONY: UnrealEngine
UnrealEngine:
	cd UnrealEngine && ./setup.sh && ./GenerateProjectFiles.sh && make UnrealPak

UnrealEngine--copy-Oodle:
	cp ./UnrealEngine/Engine/Source/Programs/Shared/EpicGames.Oodle/Sdk/2.9.10/linux/lib/liboo2corelinux64.so.9 ./src/oo2core_9_win64.dll

build: setup deps
	dotnet build ./src/AssetHttp.csproj

deps: deps--cue4parse

deps--cue4parse:
	dotnet build ./CUE4Parse/CUE4Parse/CUE4Parse.csproj

publish: setup deps
	dotnet publish ./src/AssetHttp.csproj
