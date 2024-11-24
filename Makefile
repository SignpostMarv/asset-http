setup:
	git submodule update --init --recursive
	dotnet restore ./CUE4Parse/CUE4Parse/CUE4Parse.csproj
	dotnet restore ./src/AssetHttp.csproj

build: setup deps
	dotnet build ./src/AssetHttp.csproj

deps: deps--cue4parse

deps--cue4parse:
	dotnet build ./CUE4Parse/CUE4Parse/CUE4Parse.csproj
