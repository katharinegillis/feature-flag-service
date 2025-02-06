#!/bin/bash

if [ ! -f /data/featureFlagContext.db ]; then
  touch /data/featureFlagContext.db
  /app/efbundle
fi

dotnet WebAPI.dll