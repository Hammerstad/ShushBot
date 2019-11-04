#!/usr/bin/env bash
set -e

# Ensure we are running from the same directory as the script, due to relative paths
cd "$(dirname "$0")" 

docker build -t hammerstad/shushbot:latest -t hammerstad/shushbot:$(date +%Y%m%d)-$(date +%H%M%S) ../ShushBot/ -f ../ShushBot/DockerFile