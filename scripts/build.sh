#!/usr/bin/env bash
set -e
docker build -t hammerstad/shushbot:latest -t hammerstad/shushbot:$(date +%Y%m%d)-$(date +%H%M%S) ShushBot -f ShushBot/Dockerfile