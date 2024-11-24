FROM mcr.microsoft.com/devcontainers/dotnet:8.0-bookworm

USER root

RUN \
	apt-get install -y libfontconfig1 \
    && dotnet workload update \
	&& echo "done setting up deps"

USER vscode
