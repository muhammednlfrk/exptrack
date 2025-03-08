#!/bin/bash

PROJECT_NAME="ExpenseTracker.CLI"
PROJECT_PATH="./src/$PROJECT_NAME/$PROJECT_NAME.csproj"
OUTPUT_BASE_PATH="./publish"
RUNTIMES=("win-x64" "linux-x64" "osx-x64" "osx-arm64")
for RUNTIME in "${RUNTIMES[@]}"; do
    OUTPUT_PATH="$OUTPUT_BASE_PATH/$RUNTIME"
    echo "Publishing .NET application for $RUNTIME..."
    dotnet publish "$PROJECT_PATH" -c Release -r "$RUNTIME" -o "$OUTPUT_PATH" \
        -p:PublishSingleFile=true \
        -p:IncludeNativeLibraries=true \
        -p:IncludeNativeLibrariesForSelfExtract=true \
        -p:EnableCompressionInSingleFile=true \
        -p:PublishReadyToRun=true \
        -p:DebugType=embedded \
        --self-contained=true
    echo "Publish completed for $RUNTIME! Output directory: $OUTPUT_PATH"
done
