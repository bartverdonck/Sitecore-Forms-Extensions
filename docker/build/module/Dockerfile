# escape=`

ARG BASE_IMAGE
ARG TOOLING_IMAGE

FROM ${TOOLING_IMAGE} as tooling
FROM ${BASE_IMAGE}

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

# Copy development tools and entrypoint
COPY --from=tooling \tools\ \module\tools\

COPY .\cm \module\cm
COPY .\cd \module\cd
COPY .\db \module\db