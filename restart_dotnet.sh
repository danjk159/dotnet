#!/bin/bash
cd /home/zxw/dotnet
while [ true ]
do
        echo "start sync"
	if [ -z  "`git pull | grep -e 'Already up to date.'`" ]
	  then
	    echo "is not newest,pkill dotnet"
	    pkill dotnet
            ehco "nohup dotnet run"
	    nohup dotnet run
          else
             echo "is newest"
	fi
	sleep 30
done
