#!/bin/bash

export ASPNETCORE_ENVIRONMENT=Docker

set -e
run_cmd="dotnet run --launch-profile Docker"

until dotnet ef database update; do
	>&2 echo "SQL Server is starting up"
	sleep 1
done

>&2 echo "SQL Server is up - executing command"
exec $run_cmd

