#!/bin/bash
pkill dotnet
cd /home/zxw/dotnet
git pull
nohup dotnet run
