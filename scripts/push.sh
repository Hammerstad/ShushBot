#!/usr/bin/env bash
set -e
docker login -u $1 -p $2

push_image(){
    image_name=$1
    docker push $image_name
}

push_image $(docker images | awk '{print $1":"$2}' | awk 'NR==2')
push_image $(docker images | awk '{print $1":"$2}' | awk 'NR==3')