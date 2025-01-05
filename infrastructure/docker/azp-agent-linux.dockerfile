FROM alpine:3.20

ENV TARGETARCH="linux-musl-x64"

RUN apk add --upgrade --no-cache \
    bash \
    curl \
    git \
    ca-certificates-bundle \
    icu-libs \
    icu-data-full \
    libgcc \
    libssl3 \
    libstdc++ \
    zlib \
    dotnet8-sdk

RUN dotnet help

WORKDIR /azp/

COPY ./start.sh ./
RUN chmod +x ./start.sh

RUN adduser -D agent \
    && chown agent ./
USER agent

ENV AGENT_ALLOW_RUNASROOT="true"
ENV AZP_URL="https://dev.azure.com/geerinckxseppe"
ENV AZP_POOL="Proxmox agent"
ENV AZP_AGENT_NAME="Docker Agent - Linux"

ENTRYPOINT [ "./start.sh" ]
