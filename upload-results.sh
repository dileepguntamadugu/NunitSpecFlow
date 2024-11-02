#!/bin/bash

cd bin/Debug/net8.0 && allure generate

cd ../../../
pwd
#Delete previous files from blob storage
./azcopy remove "https://cosmosreports.blob.core.windows.net/automationreports/hybridtests?$BLOB_STORAGE_DELETE_SAS_TOKEN" --delete-snapshots=include --from-to=BlobTrash --recursive --log-level=INFO;

#Copy the reports to the blob storage
./azcopy cp "/app/bin/Debug/net8.0/allure-report/" "https://cosmosreports.blob.core.windows.net/automationreports/hybridtests?$BLOB_STORAGE_ADD_SAS_TOKEN" --recursive=true

