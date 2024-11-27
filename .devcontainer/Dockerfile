FROM mcr.microsoft.com/devcontainers/dotnet:8.0-bookworm

USER root

RUN \
	dotnet workload update \
	&& echo "done setting up deps"

USER vscode
